using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject playerPrefab;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    public GameObject Players_folder;

    void Start()
    {
        
        Vector2 randomPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        GameObject playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, randomPos, Quaternion.identity);
        playerInstance.transform.SetParent(Players_folder.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
