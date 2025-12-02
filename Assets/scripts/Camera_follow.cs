using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Camera_follow : MonoBehaviour
{
    public float smoothSpeed = 100;
    public GameObject Player_folder;
    private Transform target;
    private GameObject Real_Player;
    private int myActorNumber;
    // Start is called before the first frame update



    public float timeOffset;
    public Vector3 posOffset;
    private Vector3 velocity;

    void Start()
    {
        myActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        /*myActorNumber = GetComponent<PhotonView>().Owner.ActorNumber;*/
        CheckAndFollowPlayers(Player_folder.transform);
    }

    private void CheckAndFollowPlayers(Transform currentTransform)
    {
        foreach (Transform child in currentTransform)
        {
            PhotonView photonView = child.GetComponent<PhotonView>();

            if (photonView != null)
            {
                int actorNumber = photonView.Owner.ActorNumber;

                if (actorNumber == myActorNumber)
                {
                    Real_Player = child.gameObject;
                }
            }
            CheckAndFollowPlayers(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Real_Player == null)
        {
            CheckAndFollowPlayers(Player_folder.transform);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, Real_Player.transform.position + posOffset, ref velocity, timeOffset);
        }
    }
}
