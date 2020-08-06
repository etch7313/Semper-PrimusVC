using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identifier : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tExt;
   // public LayerMask eNemy;
    [SerializeField] private Camera mainCamera;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 500f))
        {

            if (hit.transform.tag == "Enemy")
            {
                tExt.SetActive(true);
                Debug.Log("done");
            }
            else if (hit.transform.tag != "Enemy")
            {
                
                tExt.SetActive(false);

            }

        }
    }
}