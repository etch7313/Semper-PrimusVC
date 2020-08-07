using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendTreeMovement : MonoBehaviour
{
   public Animator MoveANIM;
    public GameObject camera;
    private float Speed = 10.0f;
    public GameObject head;



    void Update()
    {
       float X = Input.GetAxis("Horizontal");
       float Y = Input.GetAxis("Vertical");
        MoveANIM.SetFloat("x", X);
        MoveANIM.SetFloat("y", Y);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //SetRotate(camera);
        SetRotate(head, camera);
    }

    void SetRotate(GameObject toRotate,GameObject camera)
    {
        Quaternion rotY = Quaternion.Euler(0, camera.transform.rotation.y, 0);
         head.transform.rotation = Quaternion.Slerp(toRotate.transform.rotation, camera.transform.rotation, Speed * Time.deltaTime);

        float Y = camera.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, Y, 0);
    }
}
