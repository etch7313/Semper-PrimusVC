using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 150;
    public Animator Anim;
    public Collider HeadRef;

    private void Update()
    {
        if(health<=0)
        {
            Debug.Log("DEAD");
            //Play dead Animation Anim.SetBool("Dead",True);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="M4Bullet")
        {
            health -= 45;
            //HeadShot();
            HitIndication();
        }
        if (other.tag == "DeagleBullet")
        {
            health -= 30;
           /*if( HeadShot())
            {
                //DEAD Animation
                return;
            }*/
            HitIndication();

        }
    }

    public void HitIndication()
    {
        if(transform.tag=="Player")
        {
            //play ther hit Animation for Player
            //lerp camera Color To red And fade out 
        }
        else if(transform.tag=="Enemy")
        {
            //play Hit animation for Bots
        }
    }

   /* public bool HeadShot()
    {
        if()


        return true;
    }*/
}
