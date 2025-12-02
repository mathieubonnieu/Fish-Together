using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;


public class bait_script : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 initialScale;
    public Animator animator;
    private Tilemap Eaux;
    private Tilemap Terrain;
    private PhotonView photonView;
    private GameObject eclam_mark;


    public int min_time;
    public int max_time;
    private float waitTime ;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = Random.Range(min_time, max_time);
        photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        initialScale = transform.localScale;
        Tilemap[] tilemaps = FindObjectsOfType<Tilemap>();
        gameObject.name = "bait";
        int id = photonView.OwnerActorNr;

        GameObject playersContainer = GameObject.Find("Players");

        foreach (Transform child in playersContainer.transform)
        {
            PhotonView photonView_player = child.GetComponent<PhotonView>();
            if (photonView_player.OwnerActorNr == id)
            {
                    transform.parent = child;
                    break;
            }
        }
        eclam_mark = transform.parent.Find("eclam_mark").gameObject;

        foreach (Tilemap tilemap in tilemaps)
        {
            if (tilemap.name == "Eaux")
            {
                Eaux = tilemap;
            }
            if (tilemap.name == "Terrain")
            {
                Terrain = tilemap;
            }
        }
        
    }
    void Update()
    {
        float currentZ = transform.position.z;
        float scaleFactor = Mathf.Max(1.0f, 1.0f + (currentZ + 10) * currentZ / 6);
        transform.localScale = initialScale * scaleFactor;

        
        Vector3Int gridPosition = Eaux.WorldToCell(transform.position); // Convertir la position en coordonnées de grille
        TileBase waterTile = Eaux.GetTile(gridPosition);
        TileBase terrainTile = Terrain.GetTile(gridPosition);
        if (Eaux == null)
        {
            Debug.LogError("Les Tilemaps n'ont pas été trouvées dans la scène.");
            return;
        }
            
        if (currentZ <= 0)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            GetComponent<SpriteRenderer>().sortingOrder = 5;
            if (waterTile != null)
            {
                animator.SetBool("is_in_water", true);
            } else if (terrainTile != null)
            {
                animator.SetBool("is_in_water", false);
            } else {
                animator.SetBool("is_in_water", true);
            }

        } else
        {
            animator.SetBool("is_in_water", false);
            GetComponent<SpriteRenderer>().sortingOrder = 10;
        } 
        
        if(animator.GetBool("is_in_water") == true)
        {
            waitTime -= Time.deltaTime;
        }
        if (waitTime <= 0 && !eclam_mark.activeSelf)
        {
            eclam_mark.SetActive(true);
            waitTime = 1;
        }
        if (waitTime <= 0 && eclam_mark.activeSelf)
        {
            eclam_mark.SetActive(false);
            waitTime = Random.Range(min_time, max_time);
        }

    }
}