using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    public GameObject scoreBoard;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreBoard.transform.LookAt(Player.transform.position);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            scoreBoard.SetActive(false);
        }
    }
}
