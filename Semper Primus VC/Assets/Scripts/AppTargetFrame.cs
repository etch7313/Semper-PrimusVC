using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppTargetFrame : MonoBehaviour
{
    
    private void Awake()
    {
        Application.targetFrameRate=100;
    }
}
