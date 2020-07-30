using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 5f;
    public GameObject camerarig;
    public Transform cameraTransform;
    
    // Update is called once per frame
    void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = ((cameraTransform.forward * vertical) + (cameraTransform.right * horizontal));
        transform.position += moveDirection.normalized * speed * Time.deltaTime;

    }

  
    
}
