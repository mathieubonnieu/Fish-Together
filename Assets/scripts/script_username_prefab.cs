 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_username_prefab : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        playerPosition.y += 1;
        RectTransform canvasRectTransform = GetComponent<RectTransform>();
        canvasRectTransform.position = playerPosition;
    }
}
