using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class mini_game : MonoBehaviour
{
    // Start is called before the first frame update
    int life_fish;
    public fish_script fish_script;
    public GameObject prefabWin;
    public GameObject prefabLoose;
    public GameObject prefabCursor;
    PhotonView view;

    void Start()
    {
        transform.name = "mini_game";
        life_fish = fish_script.life;
        view = GetComponent<PhotonView>();
        GameObject instantiatedPrefab = PhotonNetwork.Instantiate(prefabCursor.name, Vector3.zero, Quaternion.identity, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(fish_script.life >= 100)
        {
            GameObject instantiatedPrefab = PhotonNetwork.Instantiate(prefabWin.name, Vector3.zero, Quaternion.identity, 2);
            PhotonNetwork.Destroy(gameObject);
        } else if (fish_script.life <= 0)
        {
            GameObject instantiatedPrefab = PhotonNetwork.Instantiate(prefabLoose.name, Vector3.zero, Quaternion.identity, 2);
            PhotonNetwork.Destroy(gameObject);
        }  
    }
}
