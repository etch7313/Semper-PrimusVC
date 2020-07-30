using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Intro : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(iNtro());
    }
    IEnumerator iNtro()
    {
        
        yield return new WaitForSeconds(9.8f);
        SceneManager.LoadSceneAsync(1);
    }

    

}
