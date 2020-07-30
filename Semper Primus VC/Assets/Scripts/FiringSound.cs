using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringSound : MonoBehaviour
{
    [SerializeField] private AudioSource akShootSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            shoot();
        }
    }

    void shoot()
    {
        akShootSound.Play();
    }
}
