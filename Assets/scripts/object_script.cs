using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


public class object_script : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 5.0f;
    public GameObject player;
    PhotonView view;
    void Start()
    {
        
        view = GetComponentInParent<PhotonView>();
    }

   
    void Update()
    {
        if (view.IsMine)
        {
            Vector3 currentPosition = transform.position;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - currentPosition;
            direction.z = 0;
            Vector3 localScale = player.transform.localScale;
            int rota_temp = 45;
            if (localScale.x < 0)
            {
                rota_temp = 135;
            }
            float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - rota_temp;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
