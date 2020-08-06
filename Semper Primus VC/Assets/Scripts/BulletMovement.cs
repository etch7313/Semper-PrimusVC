using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Rigidbody RB;
    public GameObject RP;
    
    [SerializeField]
    private float Speed = 5000f;
    
    public void Start()
    {
        Mathf.Lerp(transform.eulerAngles.y, RP.transform.eulerAngles.y, 0.1f);
         
            Destroy(this.gameObject,2f);
            RB.AddForce(transform.forward*Speed);
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
