
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.XR;


public class SceneManagerForMenu : MonoBehaviour
{
    [Header("Misc.")]
    public GameObject putVrHeadset;
    private AsyncOperation _Xscene;
    [Header("Menu Refrence")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject chooseMap;
    
   [Header("Controls Image")]
    [SerializeField] private GameObject controlsImage;
    int x = 0;


    public void ControlsON()
    {
      
       LeanTween.scale(controlsImage, new Vector3(1,1,1), 0.25f);
        
    }
    public void ControlsOFF()
    {
        LeanTween.scale(controlsImage, Vector3.zero, 0.25f);
    }

    public void StartMenu()
    {
        LeanTween.scale(mainMenu, Vector3.zero, 0.25f);
        LeanTween.scale(startMenu, new Vector3(1, 1, 1), 0.25f).setDelay(0.25f);
    }

    public void MultiPlayer()
    {
        LeanTween.scale(startMenu, Vector3.zero, 0.25f);
        LeanTween.scale(chooseMap, new Vector3(1, 1, 1), 0.25f).setDelay(0.25f);
    }





    public void BackToMainMenuFromStart()
    {
        LeanTween.scale(startMenu, Vector3.zero, 0.25f);
        LeanTween.scale(mainMenu, new Vector3(1, 1, 1), 0.25f).setDelay(0.25f);
    }
    public void BackToStartMenuFromChooseMap()
    {
        LeanTween.scale(chooseMap, Vector3.zero, 0.25f);
        LeanTween.scale(startMenu, new Vector3(1, 1, 1), 0.25f).setDelay(0.25f);
    }





   


    public void Factory()
    {
        putVrHeadset.SetActive(true);
        _Xscene = SceneManager.LoadSceneAsync(4, LoadSceneMode.Single);
        _Xscene.allowSceneActivation = false;
        StartCoroutine(MobilePlacer("Cardboard"));
    }

    public void SkyScrapper()
    {
        putVrHeadset.SetActive(true);
        _Xscene = SceneManager.LoadSceneAsync(5, LoadSceneMode.Single);
        _Xscene.allowSceneActivation = false;
        StartCoroutine(MobilePlacer("Cardboard"));
    }
    public void Collage()
    {
        putVrHeadset.SetActive(true);
        _Xscene = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
        _Xscene.allowSceneActivation = false;
        StartCoroutine(MobilePlacer("Cardboard"));
        
    }



    public void TrainingButton()
    {
        putVrHeadset.SetActive(true);
        _Xscene = SceneManager.LoadSceneAsync(3, LoadSceneMode.Single);
        _Xscene.allowSceneActivation = false;
        StartCoroutine(MobilePlacer("Cardboard"));
    }

    IEnumerator MobilePlacer(string vrON)
    {
        
        XRSettings.LoadDeviceByName(vrON);
        yield return new WaitForSeconds(2);
        
        
        XRSettings.enabled = true;
        _Xscene.allowSceneActivation = true;
    }

    public void Exit()
    {
        Application.Quit();
    }

}