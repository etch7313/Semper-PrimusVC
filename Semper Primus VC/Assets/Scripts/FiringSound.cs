using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringSound : MonoBehaviour
{
    [Header("Audio Refrence")]
    [SerializeField] private AudioSource akShootSound;
  
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
