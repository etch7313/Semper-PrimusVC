using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire1 : MonoBehaviour
{
    public AudioSource akSound;
    public AudioSource m4Sound;
    public float offsetX, offsetY, offsetZ;
     public static int Profile = 5;
   // [SerializeField]  private Text NameOnUI;
    //[SerializeField]  private Text BulletsOnUI;
    [SerializeField] private float ReloadTime =0;
    [SerializeField] private string[] Names = { "Glock", "ak47", "m4","deagle" };
    [SerializeField] private int[] NoOfBullets = { 12, 30, 30 };
    [SerializeField] private int[] NoOfMags = {2,3,3 };
    [SerializeField] private float AKFireRateValue = 00.5f;
    [SerializeField] private float M4FireRateValue = 00.25f;
    float Nextfire=0;
    [HideInInspector] public int TempInfo=0;



    public GameObject bulletPrefab;
    public GameObject cubeFire;

    // Start is called before the first frame update
    void Start()
    {
          TempInfo = NoOfMags[Profile] * NoOfBullets[Profile];
            // GetInfo();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
       
        
        reload();
        switch (Profile)
        {
            //weapon switch in game
            /*if (Input.GetKeyDown(KeyCode.Space))
            Profile = (Profile + 1) % 3;
        */
            case 0:
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    Fire();
                }

                break;
            }
            case 4:
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    Fire();
                }

                break;
            }
        }
        


        if (Input.GetKey(KeyCode.K))
        {
            if(Profile == 1 && Time.time >=Nextfire) 
            {
                         Nextfire = Time.time + AKFireRateValue;
                         Fire();
                         akSound.Play();
                
            }
            else if (Profile == 2 && Time.time >= Nextfire)
            {
                         Nextfire = Time.time + M4FireRateValue;
                         Fire();
                         
            }
        }
        
    }
  
   

    public void Fire()
    {
        switch (Profile)
        {
            case 0:
            {
                if (NoOfBullets[Profile] > 0)
                {
                    Instantiate(bulletPrefab, cubeFire.transform.position, cubeFire.transform.rotation);
                    NoOfBullets[Profile]--;
                    
                    //GetInfo();
                }

                break;
            }
            case 1:
            {
                if (NoOfBullets[Profile] > 0)
                {
                        Instantiate(bulletPrefab, cubeFire.transform.position, cubeFire.transform.rotation);
                        NoOfBullets[Profile]--;
                    akSound.Play();
                    // GetInfo();
                }

                break;
            }
            case 2:
            {
                if (NoOfBullets[Profile] > 0)
                {
                        Instantiate(bulletPrefab, cubeFire.transform.position, cubeFire.transform.rotation);
                        NoOfBullets[Profile]--;
                    m4Sound.Play();
                    //GetInfo();
                }

                break;
            }
            case 3:
            {
                if (NoOfBullets[Profile] > 0)
                {
                        Instantiate(bulletPrefab, cubeFire.transform.position, cubeFire.transform.rotation);
                        NoOfBullets[Profile]--;
                    //GetInfo();
                }

                break;
            }
        }
    }
       public void reload() 
        {
            if (NoOfBullets[Profile] <= 0)
            {

                if (ReloadTime < 3f)
                {
                    ReloadTime += Time.deltaTime;
                
                }
                else
                {
                
                    if (NoOfMags[Profile] > 0)
                    {
                        switch (Profile)
                        {
                            case 0:
                                NoOfBullets[0] = 12;
                                NoOfMags[0]--;
                                TempInfo = NoOfMags[Profile] * NoOfBullets[Profile];
                                // GetInfo();
                                ReloadTime = 0;
                                break;
                            case 1:
                                NoOfBullets[1] = 30;
                                NoOfMags[1]--;
                                TempInfo = NoOfMags[Profile] * NoOfBullets[Profile];
                                // GetInfo();
                                ReloadTime = 0;
                                break;
                            case 2:
                                NoOfBullets[2] = 30;
                                NoOfMags[2]--;
                                TempInfo = NoOfMags[Profile] * NoOfBullets[Profile];
                                //GetInfo();
                                ReloadTime = 0;
                                break;
                            case 4:
                                NoOfBullets[0] = 12;
                                NoOfMags[0]--;
                                TempInfo = NoOfMags[Profile] * NoOfBullets[Profile];
                                //GetInfo();
                                ReloadTime = 0;
                                break;
                        }
                    }
                }
            }
        }


       
   /* public void GetInfo()
    {
        NameOnUI.text = Names[Profile];
       
        BulletsOnUI.text = NoOfBullets[Profile].ToString() + "/" + TempInfo.ToString();
    }*/
}
