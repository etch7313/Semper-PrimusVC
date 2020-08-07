using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    #region Variables
    [Header("Variables")]
    public float speed = 5f;
    #endregion
    #region
    [Header("Refrence")]
    public Rigidbody rb;
    public GameObject camerarig;
    public Transform cameraTransform;
    #endregion
    
    // Update is called once per frame
    void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = ((cameraTransform.forward * vertical) + (cameraTransform.right * horizontal));
        transform.position += moveDirection.normalized * speed * Time.deltaTime;

    }

  
    
}
