using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMainMenu : MonoBehaviour
{
    public GameObject VROFFBUTTON;
   

    public void goback()
    {
        VROFFBUTTON.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
