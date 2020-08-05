using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendTreeMovement : MonoBehaviour
{
   public Animator MoveANIM;
    float X;
    float Y;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        X = Input.GetAxis("Horizontal");
        Y = Input.GetAxis("Vertical");
        MoveANIM.SetFloat("x", X);
        MoveANIM.SetFloat("y", Y);
    }
}
