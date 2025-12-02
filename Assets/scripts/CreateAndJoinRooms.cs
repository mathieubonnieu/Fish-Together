using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public TMPro.TMP_InputField CreateInput;
    public TMPro.TMP_InputField JoinInput;
    public TMPro.TMP_InputField Username;

    void Start()
    {

    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(CreateInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(JoinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.NickName = Username.text;
        PlayerPrefs.SetString("Username", Username.text);
        PhotonNetwork.LoadLevel("Game");
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
