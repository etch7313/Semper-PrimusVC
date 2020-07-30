using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRDOC_RuntimeColliderExample : MonoBehaviour {

	void Start()
    {
        // Instantiates a new BoxCollider for this gameobject.
        BoxCollider collider = this.gameObject.AddComponent<BoxCollider>();
            
        // Creates a new VRDOC_Object component and the collider to the VRDOC pool.
        VRDOC_API.ManageRuntimeGeneratedObject(this.gameObject, collider);

        /*
            If you want, you can override the layer name of the object:
            VRDOC_API.ManageRuntimeGeneratedObject(this.gameObject, collider, LayerMask.NameToLayer("VRDOC_ObjectLayer"));
            
            Bear in mind that if you input a layer name that is not managed by VRDOC, it will be forced to the first
            layer that is managed by VRDOC.
        */
    }

}
