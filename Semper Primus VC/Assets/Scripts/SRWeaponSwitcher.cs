using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRWeaponSwitcher : MonoBehaviour
{
    
    /*
     profile numebrs
     glock =0
     ak47 =1
     m4 =2
     deagle=3
     knife=4
     smoke=5
     frag=6
     */
    [SerializeField] private GameObject _ak47;
    [SerializeField] private GameObject _m4;
    [SerializeField] private GameObject _glock;
    [SerializeField] private GameObject _deagle;
    [SerializeField] private GameObject _knife;
    [SerializeField] private GameObject _fragGrenade;
    [SerializeField] private GameObject _smokeGrendae;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask weapons;
    public WeaponHandler WP;

    private void Awake()
    {
        _ak47.SetActive(false);
        _m4.SetActive(false);
        _glock.SetActive(false);
        _deagle.SetActive(false);
        _knife.SetActive(false);
        _fragGrenade.SetActive(false);
        _smokeGrendae.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("R1"))
        {
            weaponChecker();
        }
        
    }

    void weaponChecker()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(mainCamera.transform.position,mainCamera.transform.forward, out hit, 500f,weapons))
        {

            if (hit.transform.name == "ak")
            {
                Ak47();
            }
            else if (hit.transform.name == "M4Final")
            {
                M4();
            }
            else if (hit.transform.name == "pistolNobel")
            {
                Deagle();
            }
            else if (hit.transform.name == "GLOCK")
            {
                Glock();
            }
            else if (hit.transform.name == "knifeFinal")
            {
                Knife();
            }
            else if (hit.transform.name == "smoke grenade")
            {
                SmokeGrenade();
            }
            else if (hit.transform.name == "frag grenade")
            {
                FragGrenade();
            }
            
                
        }
    }

    public void Ak47()
    {
        _ak47.SetActive(true);
        _m4.SetActive(false);
        _glock.SetActive(false);
        _deagle.SetActive(false);
        _knife.SetActive(false);
        _fragGrenade.SetActive(false);
        _smokeGrendae.SetActive(false);
        
        WP.WeaponChoice("ak47");
        
    }

    public void M4()
    {
        _ak47.SetActive(false);
        _m4.SetActive(true);
        _glock.SetActive(false);
        _deagle.SetActive(false);
        _knife.SetActive(false);
        _fragGrenade.SetActive(false);
        _smokeGrendae.SetActive(false);
        WP.WeaponChoice("m4");
    }

    public void Glock()
    {
        _ak47.SetActive(false);
        _m4.SetActive(false);
        _glock.SetActive(true);
        _deagle.SetActive(false);
        _knife.SetActive(false);
        _fragGrenade.SetActive(false);
        _smokeGrendae.SetActive(false);
        WP.WeaponChoice("glock");
    }

    public void Deagle()
    {
        _ak47.SetActive(false);
        _m4.SetActive(false);
        _glock.SetActive(false);
        _deagle.SetActive(true);
        _knife.SetActive(false);
        _fragGrenade.SetActive(false);
        _smokeGrendae.SetActive(false);
        WP.WeaponChoice("deagle");
    }
    
    public void Knife()
    {
        _ak47.SetActive(false);
        _m4.SetActive(false);
        _glock.SetActive(false);
        _deagle.SetActive(false);
        _knife.SetActive(true);
        _fragGrenade.SetActive(false);
        _smokeGrendae.SetActive(false);
        WP.WeaponChoice("knife");
    }
    
    public void SmokeGrenade()
    {
        _ak47.SetActive(false);
        _m4.SetActive(false);
        _glock.SetActive(false);
        _deagle.SetActive(false);
        _knife.SetActive(false);
        _fragGrenade.SetActive(false);
        _smokeGrendae.SetActive(true);
        WP.WeaponChoice("smoke");
    }
    
    public void FragGrenade()
    {
        _ak47.SetActive(false);
        _m4.SetActive(false);
        _glock.SetActive(false);
        _deagle.SetActive(false);
        _knife.SetActive(false);
        _fragGrenade.SetActive(true);
        _smokeGrendae.SetActive(false);
        WP.WeaponChoice("frag");
    }
}
