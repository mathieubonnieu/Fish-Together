using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Object : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Players"))
        {
            transform.name = "Player";
            transform.parent = GameObject.Find("Players").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
