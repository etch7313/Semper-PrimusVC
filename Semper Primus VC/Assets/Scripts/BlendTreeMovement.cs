using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendTreeMovement : MonoBehaviour
{
   public Animator MoveANIM;
    public GameObject camera;
    private float Speed = 10.0f;




    void LateUpdate()
    {
       float X = Input.GetAxis("Horizontal");
       float Y = Input.GetAxis("Vertical");
        MoveANIM.SetFloat("x", X);
        MoveANIM.SetFloat("y", Y);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        SetRotate(camera);
    }

    void SetRotate( GameObject camera)
    {
        Quaternion rotY = Quaternion.Euler(0, camera.transform.rotation.y, 0);
       
      
        transform.rotation = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
    }
}
