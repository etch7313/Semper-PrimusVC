using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class fire : MonoBehaviour
{
    public GameObject bulletPreFab;
    private float bulletMag = 30f;
    public float numOfMags = 5;
    public float timeToReload = 4;
    public GameObject enemy;
    public Transform tr;

    // Update is called once per frame
    void Update()
    {
        shoot();
    }
  

    void reload()
    {
        timeToReload = timeToReload - Time.deltaTime;
        if (timeToReload <= 0)
        {
            timeToReload = 4;
            numOfMags--;
            bulletMag = 30f;
        }
    }

    public  void shoot()
    {
        if (numOfMags > 0)
        {


            if (bulletMag > 0f)
            {
                if (Input.GetButton("R1"))
                {
                    Instantiate(bulletPreFab, tr.position, Camera.main.transform.rotation);
                    bulletMag--;
                }
            }
            else
            {
                reload();
            }
        }
    }
}
