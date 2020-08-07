using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fire1 : MonoBehaviour
{
    
    public AudioSource m4Sound;
    public int Profile = 0;
    float Nextfire=0;
    bool switcher = true;
    [SerializeField] private TextMeshProUGUI NameOnUI;
    [SerializeField] private TextMeshProUGUI BulletsOnUI;
    [SerializeField] private float ReloadTime =0;
    [SerializeField] private string[] Names = { "deagle", "m4"};
    [SerializeField] private int[] NoOfBullets = {7, 30};
    [SerializeField] private int[] NoOfMags = {2,3};
    [SerializeField] private float M4FireRateValue = 00.25f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject cubeFireM4;
    [SerializeField] private GameObject cubeFireDeagle;
    [SerializeField] private GameObject M4;
    [SerializeField] private GameObject Deagle;
    public Animator anim;
    [HideInInspector] public int TempInfo=0;
    // Start is called before the first frame update
    void Start()
    {
          TempInfo = NoOfMags[Profile] * NoOfBullets[Profile];
          GetInfo();
        anim.SetBool("Pistol", false);
        anim.SetBool("M4", false);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Profile = (Profile + 1) % 2;
            GetInfo();         
            anim.SetBool("M4", switcher);
            anim.SetBool("Pistol", !switcher);
            switcher = !switcher;
            Debug.Log(switcher);
        }
        
        reload();
        switch (Profile)
        {
            case 0:
            {              
              if (Input.GetKeyDown(KeyCode.K))
                {
                    Fire();
                }
              break;
            }
            case 1:
                {
                    if (Input.GetKey(KeyCode.K))
                    {
                        if (Time.time >= Nextfire)
                        {
                            Nextfire = Time.time + M4FireRateValue;
                            Fire();
                        }
                    }
                    break;
                }
        }           
    }
    public void Fire()
    {
        Debug.Log("Fire");
        switch (Profile)
        {
            case 0:
            {
                if (NoOfBullets[Profile] > 0)
                {
                    Instantiate(bulletPrefab, cubeFireDeagle.transform.position, cubeFireDeagle.transform.rotation);
                    NoOfBullets[Profile]--;
                    GetInfo();
                    Debug.Log("Deagle");
                }
                break;
            }           
            case 1:
            {
                if (NoOfBullets[Profile] > 0)
                {
                    Instantiate(bulletPrefab, cubeFireM4.transform.position, cubeFireM4.transform.rotation);
                    NoOfBullets[Profile]--;
                   // m4Sound.Play();
                    GetInfo();
                    Debug.Log("m4");
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
                                NoOfBullets[Profile] = 7;
                                NoOfMags[Profile]--;
                                TempInfo = NoOfMags[Profile] * NoOfBullets[Profile];
                                GetInfo();
                                ReloadTime = 0;
                                Debug.Log("Info");
                                break;
                            case 1:
                                NoOfBullets[Profile] = 30;
                                NoOfMags[Profile]--;
                                TempInfo = NoOfMags[Profile] * NoOfBullets[Profile];
                                GetInfo();
                                ReloadTime = 0;
                                Debug.Log("Info");
                                break;                          
                        }
                    }
                }
            }
        } 
    public void GetInfo()
    {
        NameOnUI.text = Names[Profile];
       
        BulletsOnUI.text = NoOfBullets[Profile].ToString() + "/" + TempInfo.ToString();
    }
}
