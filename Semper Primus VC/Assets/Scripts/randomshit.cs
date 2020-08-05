using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomshit : MonoBehaviour
{
    public GameObject VROFFBUTTON;
   

    public void goback()
    {
        VROFFBUTTON.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
