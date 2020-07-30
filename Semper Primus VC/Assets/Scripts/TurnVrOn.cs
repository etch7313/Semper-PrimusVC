using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.XR.XRSettings;

public class TurnVrOn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(VrOn("Cardboard")); 
    }

    public IEnumerator VrOn(string _vrON)
    {
        LoadDeviceByName(_vrON);
        yield return null;
        XRSettings.enabled = true;

    }
}
