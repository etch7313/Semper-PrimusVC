/*
	--------------------------------
	VRDOC Dynamic Occlusion Culling
	Copyright (C) 2017 Viversion Ltd
	--------------------------------
*/

using UnityEngine;
using System.Collections.Generic;

public class VRDOC_Camera : MonoBehaviour {

	public static VRDOC_Camera Instance;

	[HeaderAttribute("VRDOC_Object will be auto-added to gameobjects on these layers.")]
	public LayerMask VRDOC_Layers;
	[HeaderAttribute("NOTE: Do not put any gameobjects on this layer. Transparent gameobjects will use this layer.")]
	[SerializeField, VRDOC_Layer] int transparencyLayer;

	[HeaderAttribute("Raycast FOV - set it to about ten units larger than your camera FOV.")]
	[RangeAttribute(30, 200)]
	public int raycastFieldOfView = 90;
	[RangeAttribute(1, 30)]
	public int raycastEveryNthFrame = 5;

	[HeaderAttribute("The maximum range of rays. Objects further than this are considered culled.")]
	public float maxRaycastRange = 500f;

	[HeaderAttribute("How many columns of rays there are?")]
	[Range(4, 24)]
	public int rayColumnCount = 12;

	[HeaderAttribute("How many frames an object will be visible if not hit by a ray?")]
	[RangeAttribute(30, 200)]
	public int objectVisibleFrameTime = 90;

	[HeaderAttribute("Modifies the dynamic sphere size.")]
	[Range(0.1f,1f)]
	public float dynamicSphereSizeDistanceModifier = 0.4f;

	[HeaderAttribute("Used only when USE_DYNAMIC_SPHERE_SIZE is false.")]
	[Range(1, 32)]
	public float staticSphereCastRadius = 8f;

	[HeaderAttribute("Additional settings")]
	public bool USE_REALTIME_SHADOWS = false;
	public bool USE_DYNAMIC_SPHERE_SIZE = true;
	[HeaderAttribute("Debug settings")]
	public bool VISUALIZE_CULLING = false;
	public bool ENABLE_LOGGING = false;
	
	private Ray VRDOC_Ray;
	private Camera VR_CAMERA;
	private Camera VRDOC_RaycastCamera;
	private Vector2[] raySourcePoints;
	private Vector2[] raySourceMPoints;
	private int raySourceCountW = 0;
	private int raySourceCountH = 0;
	private RaycastHit[] rayHits;
	public bool objectsInitialized = false;
	private float castSphereSize;
	private RaycastHit hit;
	private List<Vector3> visualHitPoints = new List<Vector3>();
	
	public int GetTransparentLayer()
	{
		return transparencyLayer;
	}

	private void Awake () 
	{
		if(Instance == null)
			Instance = this;
		else
			VRDOC_API.Log("There are more than one VRDOC_Camera scripts in the scene.", true);
		
		VR_CAMERA = GetComponent<Camera>();

		if(VR_CAMERA == null)
			VRDOC_API.Log("The VRDOC_Camera component should be attached to your main camera", true);
			
		VRDOC_API._InitializeMainCamera();

		if(raycastEveryNthFrame >= objectVisibleFrameTime)
		{
			VRDOC_API.Log("raycastEveryNthFrame should be smaller than objectVisibleFrameTime", true);
			raycastEveryNthFrame = objectVisibleFrameTime - 10;
		}

		if(maxRaycastRange <= 0)
			maxRaycastRange = VR_CAMERA.farClipPlane;
	}

	public void GenerateRaycastPoints()
	{
		float rayRadius;
		float x = 0;
		float y = 0;

		raySourceCountW = Mathf.FloorToInt(Screen.width / rayColumnCount);
		raySourceCountH = Mathf.FloorToInt(Screen.height / rayColumnCount);
		rayRadius = Mathf.CeilToInt(Screen.width / rayColumnCount);
		raySourcePoints = new Vector2[(rayColumnCount*rayColumnCount)*2];

		VRDOC_API.Log("Ray source points: " + raySourcePoints.Length);
		
		for(int i=0;i<raySourcePoints.Length;i++)
		{	
			if(i < raySourcePoints.Length/2)
			{
				if(i > 0 && i % rayColumnCount == 0)
				{
					x = 0;
					y += rayRadius / Screen.height;
				}
				
				raySourcePoints[i] = new Vector2(x, y);
				x += rayRadius / Screen.width;
			}
			else
			{
				if(i == raySourcePoints.Length/2)
				{
					x = (rayRadius/2) / Screen.width;
					y = (rayRadius/2) / Screen.height;
				}

				if(i % rayColumnCount == 0)
				{
					x = (rayRadius/2) / Screen.width;
					y += rayRadius / Screen.height;
				}
			
				raySourcePoints[i] = new Vector2(x, y);
				x += rayRadius / Screen.width;
			}
		}
	}
	
	private void InitializeRaycastCamera()
	{
		GameObject VRDOC_RayCastCamera_GameObject = new GameObject("VRDOC_RaycastCamera");
		VRDOC_RayCastCamera_GameObject.transform.position = this.transform.position;
		VRDOC_RayCastCamera_GameObject.transform.rotation = this.transform.rotation;
		VRDOC_RayCastCamera_GameObject.transform.parent = this.transform;

		VRDOC_RaycastCamera = VRDOC_RayCastCamera_GameObject.AddComponent<Camera>();
		VRDOC_RaycastCamera.nearClipPlane = VR_CAMERA.nearClipPlane;
		VRDOC_RaycastCamera.farClipPlane = VR_CAMERA.farClipPlane;
		VRDOC_RaycastCamera.cullingMask = 0;
		VRDOC_RaycastCamera.fieldOfView = raycastFieldOfView;
		VRDOC_RaycastCamera.aspect = VR_CAMERA.aspect;
		VRDOC_RaycastCamera.clearFlags = CameraClearFlags.Nothing;
		VRDOC_RaycastCamera.enabled = false;
	}
	
	private void Start () 
	{
		GenerateRaycastPoints();
		InitializeRaycastCamera();
		VRDOC_API.InitializeColliders();
		objectsInitialized = true;
		
		VRDOC_API.Log("Number of objects: " + VRDOC_API.GetPoolSize());

		if(VISUALIZE_CULLING)
			VRDOC_API.Log("Culling visualization ON.");
			
		VRDOC_API.Log("Ready.");
	}
	
	
	void OnDrawGizmos()
	{
		if(VISUALIZE_CULLING)
		{
			Gizmos.color = Color.green;
			
			if(rayHits != null)
			{
				for(int r=0;r<visualHitPoints.Count;r++)
				{
					Gizmos.DrawWireSphere(visualHitPoints[r], (USE_DYNAMIC_SPHERE_SIZE ?  castSphereSize : staticSphereCastRadius));
					visualHitPoints.Remove(visualHitPoints[r]);
				}
			}
		}
	}

	public void SphereCastAll(int index)
	{
		VRDOC_Ray = VRDOC_RaycastCamera.ViewportPointToRay(new Vector3(raySourcePoints[index].x, raySourcePoints[index].y, 0f));
		
		if(VISUALIZE_CULLING)
			Debug.DrawLine(VRDOC_Ray.origin, VRDOC_Ray.direction + VRDOC_Ray.GetPoint(100f), (secondSet ? Color.yellow : Color.red));
		
		if(Physics.Raycast(VRDOC_Ray, out hit, maxRaycastRange, VRDOC_Layers.value, QueryTriggerInteraction.Collide))
		{
			if(VISUALIZE_CULLING)
			{
				Debug.DrawLine(VRDOC_Ray.origin, hit.point, Color.green);
				visualHitPoints.Add(hit.point);
			}
			
			castSphereSize = (hit.distance*dynamicSphereSizeDistanceModifier);
			
			rayHits = Physics.SphereCastAll(VRDOC_Ray, (USE_DYNAMIC_SPHERE_SIZE ?  castSphereSize : staticSphereCastRadius), hit.distance, VRDOC_Layers, QueryTriggerInteraction.Collide);

			for(int r=0;r<rayHits.Length;r++)
			{
				VRDOC_API.GetObjectFromPool(rayHits[r].collider).VRDOC_EnableRenderer();
			}
		}
	}
	
	private bool secondSet = false;

	private void Update()
	{
		if(objectsInitialized)
		{
			if(Time.frameCount % raycastEveryNthFrame == 0)
			{
				if(!secondSet)
				{
					for(int i=0;i<raySourcePoints.Length/2;i++)
						SphereCastAll(i);
				}
				else
				{
					for(int i=raySourcePoints.Length/2;i<raySourcePoints.Length;i++)
						SphereCastAll(i);
				}

				secondSet = !secondSet;
			}
		}
	}
}