using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIInteraction : MonoBehaviour
{
    [Header("GameObject Refrence")]

            public GameObject VROFFBUTTON;

    [Header("Camera Refrence")]

            [SerializeField] private Camera mainCamera;

    public LayerMask floatinUI;
    
    void Update()
    {
        if (Input.GetButtonDown("R1"))
        {
            ClickUI();
        }  
    }

    void ClickUI()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(mainCamera.transform.position,mainCamera.transform.forward, out hit, 500f,floatinUI))
        {
            Debug.Log(hit.transform.name);
            if(hit.transform.name=="back button")
                Back();
        }
    }

    private void Back()
    {
        VROFFBUTTON.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
