using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

using UnityEngine.Serialization;
using UnityEngine.Audio;

public class WeaponHandler : MonoBehaviour
{
    #region Serialized Variables



      #region Profiles

    //_____________________________________[profiles]___________________________________________________________________
    public enum WeaponsProfile{None,M4,Ak47,Glock,Deagle,Knife,Frag,Smoke}
    [FormerlySerializedAs("wp")] public  WeaponsProfile WeaponProfiles;
    [Space (20)]
    #endregion
    
      #region Bullet Settings
    //_____________________________________[Bullet Settings]____________________________________________________________

    [Header("Bullet Settings")]
    [SerializeField] private GameObject bulletPreFab;
    
    [SerializeField] private float bulletLifeTime=2f;
     private float bulletMag;
    private float bulletMagHolder;
     private float numOfMags;
     private float timeToReload;
    [SerializeField] private TMP_Text bulletHUD;
    private  float totalBullets,currentBullet;
    
    #endregion
    
      #region Flags
    //______________________________________[Flags]_____________________________________________________________________
    [HideInInspector]
    public bool isVisited;

    private bool isupdated = false;
    private bool isPressed = false;
    private bool isReloading = false;
    
    #endregion

      #region Firing Settings
    //______________________________________[Firing Settings]___________________________________________________________
   
    [Header("Firing Settings")] 
    [Range(0, 1)] 
    [SerializeField] private float fireRate=0.1f;

    [SerializeField] private float nextFire = 0.0f;
    [SerializeField] private float reloadingTime = 3;
    [SerializeField] private GameObject FragGrenade;
    [SerializeField] private GameObject SmokeGrenade;
    public float range = 10f;
    #endregion

      #region Sounds

    //______________________________________[Sound]_____________________________________________________________________
    [Header("Sounds")]
    [SerializeField] private AudioSource ak47Sound;
    [SerializeField] private AudioSource m4Sound;
    [SerializeField] private AudioSource glockSound;
    [SerializeField] private AudioSource deagleSound;
    [SerializeField] private AudioSource fragSound;
    [SerializeField] private AudioSource smokeSound;
    
    #endregion

      #region Spwan Points
    //______________________________________[Spwan Points]______________________________________________________________
    
    [Header("Spwan Points")]
    [SerializeField] private GameObject ak47SpwanPoint;
    [SerializeField] private GameObject m4SpwanPoint;
    [SerializeField] private GameObject glockSpwanPoint;
    [SerializeField] private GameObject deagleSpwanPoint;
    [SerializeField] private GameObject fragSpwanPoint;
    [SerializeField] private GameObject smokeSpwanPoint;
    
    
    #endregion
    
      #region Muzzle Flash
    
    //______________________________________[Muzzle Flash]______________________________________________________________
    
    [Header("Muzzle Flash")]
    [SerializeField] private ParticleSystem ak47MF;
    [SerializeField] private ParticleSystem m4MF;
    [SerializeField] private ParticleSystem glockMF;
    [SerializeField] private ParticleSystem deagleMFt;
    #endregion

      #region HUDs

    [Header("HUDs")] 
    [SerializeField] private TMP_Text AK47Hud;
    [SerializeField] private TMP_Text M4Hud;
    [SerializeField] private TMP_Text GlockHud;
    [SerializeField] private TMP_Text DeagleHud;

    #endregion

    #region Reloading Animation
    [Header("Reloading Animations")]
    [SerializeField] private Animator akReloadAnimation;
    [SerializeField] private Animator m4ReloadAnimation;
    [SerializeField] private Animator glockReloadAnimation;
    [SerializeField] private Animator deagleReloadAnimation;
    #endregion
    
    
    #endregion
    
   
    
    void Update()
    {
        if (!isupdated)
        {
            UpdatingHud(AK47Hud,30,2*30);
            UpdatingHud(M4Hud,30,2*30);
            UpdatingHud(GlockHud,12,3*12);
            UpdatingHud(DeagleHud,9,3*9);
            bulletMagHolder = bulletMag;
            isupdated = true;
        }
        if (WeaponProfiles == WeaponsProfile.Ak47 || WeaponProfiles == WeaponsProfile.M4)
        {
            if (Input.GetButton("R2") && Time.time > nextFire)
            {
                shoot();
                
            }
           
            return;   
        }
        if(WeaponProfiles==WeaponsProfile.Deagle||WeaponProfiles==WeaponsProfile.Glock||
           WeaponProfiles==WeaponsProfile.Frag ||WeaponProfiles==WeaponsProfile.Smoke||
           WeaponProfiles==WeaponsProfile.Knife)
        {
            if ( Input.GetButtonDown("R2"))
            {

                shoot();
            }
            
        }
    }

   

    IEnumerator RifleReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadingTime);
        numOfMags--;
        bulletMag = bulletMagHolder;
        isReloading = false;

    }

    public  void shoot()
    {
        if (WeaponProfiles==WeaponsProfile.M4)
        {
            M4();
            bulletMagHolder = 30f;
            
        }
        else if (WeaponProfiles==WeaponsProfile.Ak47)
        { 
            AK47();
            bulletMagHolder = 30f;
        }
        else if (WeaponProfiles==WeaponsProfile.Glock)
        {
           Glock();
            bulletMagHolder = 12f;
        }
        else if (WeaponProfiles==WeaponsProfile.Deagle)
        {
            Deagle();
            bulletMagHolder = 9f;
        }
        else if (WeaponProfiles==WeaponsProfile.Frag)
        {
           //fragSound.Play();
           GameObject FragInstance = Instantiate(FragGrenade, fragSpwanPoint.transform.position,
               fragSpwanPoint.transform.rotation);
           FragInstance.GetComponent<Rigidbody>().AddForce(fragSpwanPoint.transform.forward*range,ForceMode.Impulse);
        }
        else if (WeaponProfiles==WeaponsProfile.Smoke)
        {
           smokeSound.Play();
        }
        else if (WeaponProfiles==WeaponsProfile.Knife)
        {
           
           
        }
    }

    private  void M4()
    {
        if (!isReloading)
        {


            if (bulletMag > -1 && numOfMags > -1)
            {
                m4ReloadAnimation.SetBool("reload",false);
                m4MF.Play();
                m4Sound.Play();
                Instantiate(bulletPreFab, m4SpwanPoint.transform.position, m4SpwanPoint.transform.rotation);
                nextFire = Time.time + fireRate;
                UpdatingHud(M4Hud, bulletMag, numOfMags*30);
                bulletMag--;
            }
            else if (numOfMags  >0 )
            {
               
                m4ReloadAnimation.SetBool("reload",true);
                StartCoroutine(RifleReload());
                

            }
            else
            {
                UpdatingHud(M4Hud);
                Debug.Log("lee da5lt hena ");
            }
        }
    }

    private void AK47()
    {
        if (!isReloading)
        {
            if (bulletMag > -1 && numOfMags > -1)
            {
                akReloadAnimation.SetBool("reload",false);
                ak47MF.Play();
                ak47Sound.Play();
                Instantiate(bulletPreFab, ak47SpwanPoint.transform.position, ak47SpwanPoint.transform.rotation);
                nextFire = Time.time + fireRate;
                UpdatingHud(AK47Hud, bulletMag, numOfMags*30);
                bulletMag--;
            }
            else if (numOfMags > 0)
            {
                akReloadAnimation.SetBool("reload",true);
                StartCoroutine(RifleReload());
            }
            else
            {
                UpdatingHud(AK47Hud);
            }
        }
    }

    private void Glock()
    {
        if (!isReloading)
        {
            if (bulletMag > -1 && numOfMags > -1)
            {
                Debug.Log(bulletMag);
                glockReloadAnimation.SetBool("reload",false);           
                glockMF.Play();
                glockSound.Play();
                Instantiate(bulletPreFab, glockSpwanPoint.transform.position,
                    glockSpwanPoint.transform.rotation.normalized);
                nextFire = Time.time + fireRate;
                UpdatingHud(GlockHud, bulletMag, numOfMags*12);
                bulletMag--;
            }
            else if (numOfMags > 0)
            {
                glockReloadAnimation.SetBool("reload",true);
                StartCoroutine(RifleReload());
            }
            else
            {
                UpdatingHud(GlockHud);
            }
        }
    }

    private  void Deagle()
    {
        if (!isReloading)
        {
            if (bulletMag > -1 && numOfMags > -1)
            {
                deagleReloadAnimation.SetBool("reload",false);
                deagleMFt.Play();
                deagleSound.Play();
                Instantiate(bulletPreFab, deagleSpwanPoint.transform.position,
                    deagleSpwanPoint.transform.rotation.normalized);
                nextFire = Time.time + fireRate;
                UpdatingHud(DeagleHud, bulletMag, numOfMags*9);
                bulletMag--;
            }
            else if (numOfMags > 0)
            {
                deagleReloadAnimation.SetBool("reload",true);
                StartCoroutine(RifleReload());
            }
            else
            {
                UpdatingHud(DeagleHud);
            }
        }
    }

    private void UpdatingHud(TMP_Text txt,float numberOfBullets,float CurrentBullet)
    {
        txt.text = numberOfBullets.ToString()+" / "+(CurrentBullet).ToString()/*+" MAGs"*/;
    }
    
    private void UpdatingHud(TMP_Text txt)
    {
        txt.text = "Out OF Ammo";
    }


    #region Applying stats

    public void WeaponChoice(string weaponName)
    {
        if (weaponName == "ak47")
        {
            WeaponProfiles = WeaponsProfile.Ak47;
            bulletMag = 30f;
            numOfMags = 2f;
            timeToReload = 0f;
            return;
        }
        else if (weaponName=="m4")
        { 
            WeaponProfiles = WeaponsProfile.M4;
            bulletMag = 30f;
            numOfMags = 2f;
            timeToReload = 0f;
            return;
        }
        else if (weaponName=="glock")
        {
            WeaponProfiles = WeaponsProfile.Glock;
            bulletMag = 12f;
            numOfMags = 3f;
            timeToReload = 0f;
            return;
        }
        else if (weaponName=="deagle")
        {
            WeaponProfiles = WeaponsProfile.Deagle;
            bulletMag = 9f;
            numOfMags = 3f;
            timeToReload = 0f;
            return;
        }
        else if (weaponName=="frag")
        {
            WeaponProfiles = WeaponsProfile.Frag;
            bulletMag = 1f;
            numOfMags = 2f;
            timeToReload = 2f;
            return;
        }
        else if (weaponName=="smoke")
        {
            WeaponProfiles = WeaponsProfile.Smoke;
            bulletMag = 1f;
            numOfMags = 2f;
            timeToReload = 2f;
            return;
        }
        else if (weaponName=="knife")
        {
            WeaponProfiles = WeaponsProfile.Knife;
            //apply knife stats here
        }
        
    }
    #endregion
}
