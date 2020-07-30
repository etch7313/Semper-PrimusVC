using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRDOC_Layer : PropertyAttribute
{
}

public static class VRDOC_API {

	public static List<Collider> VRDOC_colliderPool = new List<Collider>();
	public static Dictionary<Collider,VRDOC_Object> VRDOC_objectPool = new Dictionary<Collider,VRDOC_Object>();
	
	private static VRDOC_Camera VRDOC_Cam;
    private static List<int> VRDOC_LayerMaskIndices;

    public static VRDOC_Object ManageRuntimeGeneratedObject(GameObject go, Collider c, int overrideLayer = -1)
    {
        // Add a VRDOC_Object component to the passed gameobject.
        VRDOC_Object vrdoc_object = go.gameObject.AddComponent<VRDOC_Object>();

        if ((VRDOC_Camera.Instance.VRDOC_Layers == (VRDOC_Camera.Instance.VRDOC_Layers | (1 << go.layer))) == false)
        {
            
            if(overrideLayer > -1)
            {
                // Override layer if user wants it.
                go.layer = overrideLayer;

            }
            else
            {
                // Change the layer of the gameobject to first one of the VRDOC managed layers.
                go.layer = VRDOC_LayerMaskIndices[0];
            }
        }

        // Add the instantiated collider along with the VRDOC_Object script to the VRDOC pool.
        VRDOC_API.AddToPool(c, vrdoc_object);

        return vrdoc_object;
    }

	public static void InitializeColliders()
	{
        /*
			Pools all colliders as a Collider,VRDOC_Object dictionary.
			If you have procedurally generated objects/colliedrs,
			you can call this on runtime.

            Alternative method is to call VRDOC_API.ManageRuntimeGeneratedObject manually.
            See the example file VRDOC_RuntimeColliderExample.cs.
		*/

        GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();
		VRDOC_Object obj;

		for(int i=0;i<gameObjects.Length;i++)
		{
			if(((1 << gameObjects[i].layer) & VRDOC_Camera.Instance.VRDOC_Layers) != 0)
			{
				obj = gameObjects[i].GetComponent<VRDOC_Object>();

				if(obj == null)
					obj = gameObjects[i].AddComponent<VRDOC_Object>() as VRDOC_Object;

        		Collider[] colliders = obj.GetComponents<Collider>();
				foreach (Collider c in colliders)
				{
					VRDOC_API.AddToPool(c, obj);
				}
			}
		}
	}
	public static VRDOC_Object GetObjectFromPool(Collider obj)
	{
		return VRDOC_objectPool[obj];
	}
	
	public static void AddToPool(Collider col, VRDOC_Object obj)
	{
		if(!VRDOC_objectPool.ContainsKey(col))
		{
			VRDOC_objectPool.Add(col, obj);
		}
	}

	public static int GetPoolSize()
	{
		return VRDOC_objectPool.Count;
	}
	
	public static void GenerateRaycastPoints()
	{
		VRDOC_Camera.Instance.GenerateRaycastPoints();
	}

	public static void _InitializeMainCamera()
	{
		VRDOC_Cam = Object.FindObjectOfType<VRDOC_Camera>();

		if(!VRDOC_Cam)
			VRDOC_API.Log("No VRDOC_Camera script found attached to the main camera!");

        VRDOC_LayerMaskIndices = new List<int>(32);
        for (int i = 0; i < 32; i++)
        {
            if ((VRDOC_Cam.VRDOC_Layers.value & (1 << i)) > 0)
                VRDOC_LayerMaskIndices.Add(i);
        }
    }

	public static void Log(string str, bool isError = false)
	{
		if(VRDOC_Camera.Instance.ENABLE_LOGGING)
			Debug.Log((!isError ? "<Color=#00FFFF>VRDOC </Color>" : "<Color=#FF0000>VRDOC Warning: </Color>") + "<Color=#FFFFFF>" + str + " </Color>");
	}

}
