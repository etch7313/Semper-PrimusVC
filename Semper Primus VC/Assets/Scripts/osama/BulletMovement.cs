using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Rigidbody RB;

    
    [SerializeField]
    private float Speed = 5000f;
    
    public void Start()
    {
            
            Destroy(this.gameObject,2f);
            RB.AddForce(transform.forward*Speed);
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
