using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D Player;
    public Animator animator;
    public GameObject appat_prefab;
    public GameObject minigame_prefab;
    public GameObject message_prefab;
    public int speed;
    public TMPro.TMP_Text myUsername;
    PhotonView view;
    public SpriteRenderer myrenderer;
    public LineRenderer lineRenderer;
    bool typingtext = false;    
    private bool is_send = false;
    private GameObject appat;
    private bool in_mini_game = false;
    private GameObject mini_game;



    public float fire_strength;
    public float maxFireStrength;
    public float chargeTime;

    void Start()
    {

        view = GetComponent<PhotonView>();
        myUsername.text = GetComponent<PhotonView>().Controller.NickName;
    }
    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (mini_game == null)
            {
                if (Input.GetKeyUp(KeyCode.KeypadEnter) == true)
                {
                    typingtext = (typingtext == false) ? true : false;
                }
                if (typingtext == false)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        is_send = !is_send;
                        if (is_send == true)
                        {
                            if (Input.GetMouseButtonDown(0))
                            {
                                StartCoroutine(ChargeAndLaunch());
                            }
                        }
                        else
                        {
                            recuperer();
                            if (transform.parent.Find("eclam_mark").gameObject.activeSelf)
                            {
                                PhotonNetwork.SetInterestGroups(1, true);
                                in_mini_game = true;
                                GameObject playersContainer = transform.parent.parent.transform.gameObject;
                                if (playersContainer != null)
                                {
                                    Debug.Log(playersContainer.name);
                                    foreach (Transform child in playersContainer.transform)
                                    {
                                        if (child.Find("Player").transform != transform) {
                                            float distance = Vector3.Distance(transform.position, child.Find("Player").transform.position);
                                            Debug.Log("distance: " + distance);
                                            if (distance < 3f)
                                            {
                                                PhotonView childPhotonView = child.Find("Player").GetComponent<PhotonView>();
                                                Debug.Log("group avant: " + childPhotonView.Group);
                                                
                                                if (childPhotonView != null)
                                                {
                                                    childPhotonView.RPC("ChangeInterestGroupRPC", RpcTarget.AllBuffered, childPhotonView.Owner.ActorNumber);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                mini_game = PhotonNetwork.Instantiate(minigame_prefab.name, transform.position, Quaternion.identity, 2);

                            }
                        }
                    }
                    float speed_now;
                    if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                    {
                        speed_now = 1;
                    }
                    else
                    {
                        speed_now = 0;
                    }
                    animator.SetFloat("Speed", speed_now);
                    Player.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
                    flipSprite();
                }
            } else {
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    in_mini_game = false;
                    PhotonNetwork.Destroy(mini_game);

                }
            }
        } else
        {
        }
        if(appat != null)
        {
            //Debug.Log(appat.transform.position.x + " " + appat.transform.position.y + " " + appat.transform.position.z);
        }

    }

    [PunRPC]
    private void ChangeInterestGroupRPC(int playerId)
    {
        PhotonNetwork.SetInterestGroups(2, true);
    }
    void recuperer()
    {
        PhotonNetwork.Destroy(appat);
    }
    IEnumerator ChargeAndLaunch()
    {
        float chargeTimer = 0f;

        while (Input.GetMouseButton(0) && chargeTimer < chargeTime)
        {
            chargeTimer += Time.deltaTime;
            yield return null;
        }


        chargeTimer = Mathf.Min(chargeTimer, chargeTime);
        Transform pointDeLancer = transform.Find("Object/top_fishing_line");


        //Vector3 worldPosition = new Vector3(pointDeLancer.transform.position.x, pointDeLancer.transform.position.y, pointDeLancer.transform.position.z); // Remplacez x, y, z par les coordonnées mondiales souhaitées
        //Quaternion worldRotation = Quaternion.Euler(pointDeLancer.transform.rotation.x, pointDeLancer.transform.rotation.y, pointDeLancer.transform.rotation.z); // Remplacez xRot, yRot, zRot par les valeurs de rotation souhaitées

        appat = PhotonNetwork.Instantiate(appat_prefab.name, pointDeLancer.position, pointDeLancer.rotation);
        //view.RPC("SetObjectName", RpcTarget.AllBuffered, "bait");
        //int parentViewID = view.ViewID;
        //view.RPC("SetObjectParent", RpcTarget.AllBuffered, parentViewID);

        //appat = PhotonNetwork.Instantiate(appat_prefab.name, pointDeLancer.position, pointDeLancer.rotation);

        //GameObject playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, randomPos, Quaternion.identity);
        //playerInstance.transform.parent = Players_folder.transform;

        appat.transform.parent = transform.parent.transform;
        appat.transform.name = "bait";
        Rigidbody appatRigidbody = appat.GetComponent<Rigidbody>();

        // Calcule la direction vers la souris
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Ajoute une force vers le haut (axe Z) et dans la direction du rayon
        float launchForce = chargeTimer / chargeTime * maxFireStrength;
        Vector3 launchDirection = new Vector3(targetPosition.x - pointDeLancer.position.x, targetPosition.y - pointDeLancer.position.y, 2).normalized;
        appatRigidbody.AddForce(new Vector3(launchDirection.x, launchDirection.y, 1) * launchForce, ForceMode.Impulse);
    }


    void flipSprite()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.localScale = new Vector3(10, 10, 10);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-10, 10, 10);
        }
    }
}
