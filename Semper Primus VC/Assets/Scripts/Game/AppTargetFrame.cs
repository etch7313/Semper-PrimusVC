using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppTargetFrame : MonoBehaviour
{
   // Setting the Application Target Frame =100 frame per second
    private void Awake()
    {
        Application.targetFrameRate=100;
    }
}
