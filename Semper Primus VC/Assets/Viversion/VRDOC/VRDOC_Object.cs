using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;
using System.Linq;

public class VRDOC_Object : MonoBehaviour {

	[HeaderAttribute("Automatically set if one of the materials is transparent.")]
	public bool isTransparent = false;
	

	[HeaderAttribute("If this gameobject contains only a collider, set this to true.")]
	public bool colliderOnly = false;

	[HeaderAttribute("If the renderers aren't on this gameobject, specify them here.")]
    public List<Renderer> objectRenderers;

    private bool objectHidden;
	private int frameCountOnVisible;
	private int startingLayer;
	private ShadowCastingMode[] SCModes;
	
    public void AddToRenderers(Renderer r)
    {
        if (objectRenderers == null)
            objectRenderers = new List<Renderer>();

        if (!objectRenderers.Contains(r))
            objectRenderers.Add(r);
    }
	private void Start () 
	{
		startingLayer = gameObject.layer;

		List<Renderer> childRenderers = new List<Renderer>();
		List<Renderer> parentRendereres = new List<Renderer>();

		if(objectRenderers == null)
		{
			objectRenderers = new List<Renderer>();
			GetComponentsInChildren<Renderer>(true, childRenderers);
			GetComponentsInParent<Renderer>(true, parentRendereres);
			objectRenderers = childRenderers.Union<Renderer>(parentRendereres).ToList<Renderer>();
		}
		
		if(objectRenderers.Count == 0)
		{
			VRDOC_API.Log("No renderers found on '" + this.name + "'.");
			Destroy(this);
		}
		
		SCModes = new ShadowCastingMode[objectRenderers.Count];
		
		for(int r=0;r<objectRenderers.Count;r++)
			SCModes[r] = objectRenderers[r].shadowCastingMode;		

		if(!VRDOC_Camera.Instance.enabled)
			this.enabled = false;

		if(!isTransparent)
			CheckTransparency();
	}
    
	private void CheckTransparency()
	{
		for(int r=0;r<objectRenderers.Count;r++)
		{
			for(int m=0;m<objectRenderers[r].materials.Length;m++)
			{
				if (objectRenderers[r].materials[m].HasProperty("_Mode"))
				{
					if(objectRenderers[r].materials[m].GetFloat("_Mode") == 3f)
						isTransparent = true;
				}
			}
		}
	}

	public void VRDOC_DisableRenderer()
	{
		if(!isTransparent)
		{
			objectHidden = true;
			
			if(VRDOC_Camera.Instance.USE_REALTIME_SHADOWS)
			{
				for(int r=0;r<objectRenderers.Count;r++)
					objectRenderers[r].shadowCastingMode = ShadowCastingMode.ShadowsOnly;
			}
			else
			{
				for(int r=0;r<objectRenderers.Count;r++)
					objectRenderers[r].enabled = false;
			}
		}
		else
		{
			if(gameObject.layer == VRDOC_Camera.Instance.GetTransparentLayer())
			{
				frameCountOnVisible = Time.frameCount + VRDOC_Camera.Instance.objectVisibleFrameTime;
				gameObject.layer = startingLayer;
			}
			else
			{
				objectHidden = true;
				if(VRDOC_Camera.Instance.USE_REALTIME_SHADOWS)
				{
					for(int r=0;r<objectRenderers.Count;r++)
						objectRenderers[r].shadowCastingMode = ShadowCastingMode.ShadowsOnly;
				}
				else
				{
					for(int r=0;r<objectRenderers.Count;r++)
						objectRenderers[r].enabled = false;
				}
			}
		}

	}

	public void VRDOC_EnableRenderer()
	{
		frameCountOnVisible = Time.frameCount + VRDOC_Camera.Instance.objectVisibleFrameTime;
		
		if(objectHidden)
		{
			for(int r=0;r<objectRenderers.Count;r++)
			{
				objectRenderers[r].shadowCastingMode = SCModes[r];
				objectRenderers[r].enabled = true;
				objectHidden = false;
			}
		}

		if(isTransparent)
			gameObject.layer = VRDOC_Camera.Instance.GetTransparentLayer();
	}

	private void Update () 
	{
		if(!objectHidden)
		{
			if(Time.frameCount > frameCountOnVisible)
				VRDOC_DisableRenderer();
		}

	}
}
