using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
   public Transform pLayer;
  public  Transform cAmera;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        pLayer.rotation = new Quaternion(0f, 10f * Time.deltaTime, 0f,0f);
        Debug.Log("a7a");
    }
}
