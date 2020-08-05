using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIInteractionTest : MonoBehaviour
{public GameObject VROFFBUTTON;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Back();
        }
    }

    private void Back()
    {
        VROFFBUTTON.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
