using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exclam_mark : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject bait;
    void Start()
    {
        Vector3 temp = transform.parent.Find("Player").gameObject.transform.position;
        temp.y += 2;
        transform.position = temp;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.parent.Find("Player").gameObject.transform.position;
        temp.y += 2;
        transform.position = temp;
        if (bait == null)
        {
            transform.gameObject.SetActive(false);
            try
            {
                bait = transform.parent.Find("bait").gameObject;

            } catch
            {

            }
        }
    }
}
