using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class occlusionCulling : MonoBehaviour
{
     private Renderer[] _renderers;
     
     void Start () { 
         _renderers = FindObjectsOfType<Renderer>();
    }

    void Update()
    {
         foreach( Renderer _renderer in _renderers){
        	Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main); 
		_renderer.enabled = GeometryUtility.TestPlanesAABB(frustumPlanes, _renderer.bounds);;
		//_renderer.gameObject.SetActive(_renderer.enabled);
         }
    }
}
