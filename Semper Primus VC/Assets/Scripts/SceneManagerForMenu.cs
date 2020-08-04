
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.XR;


public class SceneManagerForMenu : MonoBehaviour
{
    
    public GameObject putVrHeadset;
    private AsyncOperation _Xscene;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject multiplayerMenu;
    [SerializeField] GameObject hostMenu;
    [SerializeField] GameObject nameMenu;
    [SerializeField] GameObject joinMenu;
    [SerializeField] GameObject networkSH;
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


    public void NameMenu()
    {
        LeanTween.scale(startMenu, Vector3.zero, 0.25f);
        LeanTween.scale(nameMenu, new Vector3(1, 1, 1), 0.25f).setDelay(0.25f);
      
    }
    public void MultiplayerMenu()
    {
         LeanTween.scale(nameMenu, Vector3.zero, 0.25f);
        LeanTween.scale(multiplayerMenu, new Vector3(1, 1, 1), 0.25f).setDelay(0.25f);
       
    }
    public void HostMenu()
    {
        LeanTween.scale(multiplayerMenu, Vector3.zero, 0.25f);
        LeanTween.scale(hostMenu, new Vector3(1, 1, 1), 0.25f).setDelay(0.25f);
    }

    public void JoinMenu()
    {
        LeanTween.scale(multiplayerMenu, Vector3.zero, 0.25f);
        LeanTween.scale(joinMenu, new Vector3(1, 1, 1), 0.25f).setDelay(0.25f);
    }
    public void BackToMainMenuFromStart()
    {
        LeanTween.scale(startMenu, Vector3.zero, 0.25f);
        LeanTween.scale(mainMenu, new Vector3(1, 1, 1), 0.25f).setDelay(0.25f);
    }
    public void BackTostartFromName()
    {
        LeanTween.scale(nameMenu, Vector3.zero, 0.25f);
        LeanTween.scale(startMenu, new Vector3(1, 1, 1), 0.25f).setDelay(0.25f);
    }
    public void BackToNameMenu()
    {
        LeanTween.scale(multiplayerMenu, Vector3.zero, 0.25f);
        LeanTween.scale(nameMenu, new Vector3(1, 1, 1), 0.25f).setDelay(0.25f);
       
    }
    public void BackToMultiplayerrFromHost()
    {
        LeanTween.scale(hostMenu, Vector3.zero, 0.25f);
        LeanTween.scale(multiplayerMenu, new Vector3(1, 1, 1), 0.25f).setDelay(0.25f);
    }
    public void BackToMultiplayerMeneFromJoinMenu()
    {
        LeanTween.scale(joinMenu, Vector3.zero, 0.25f);
        LeanTween.scale(multiplayerMenu, new Vector3(1, 1, 1), 0.25f).setDelay(0.25f);
    }


    public void ConnectButton()
    {
        //de mo2ktan bs kda l7d ma nzbat l network bs ehan hytktb code l network bta3 l join 3la l local host
        putVrHeadset.SetActive(true);
        _Xscene = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
        _Xscene.allowSceneActivation = false;
        StartCoroutine(MobilePlacer("Cardboard"));
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
        _Xscene = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
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