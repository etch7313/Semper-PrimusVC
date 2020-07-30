using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class TurnVrOff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(VrOff("None"));
    }

    public IEnumerator VrOff(string _VrOff)
    {
       XRSettings.LoadDeviceByName(_VrOff);
        yield return null;
        XRSettings.enabled = false;
    }
    
}
