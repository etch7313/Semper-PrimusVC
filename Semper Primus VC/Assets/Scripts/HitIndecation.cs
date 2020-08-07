using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class HitIndecation : MonoBehaviour
{
    [Range(0,0.220f)]
    public float Health;
    public RawImage Blood;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Blood.color = new Color(1, 1, 1, Health);
    }
}
