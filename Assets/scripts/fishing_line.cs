using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishing_line : MonoBehaviour
{
    public GameObject top_fishing_line;
    private LineRenderer lineRenderer;
    private GameObject appat;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        /*lineRenderer.positionCount = 2;*/
    }

    // Update is called once per frame
    void Update()
    {
        if(appat == null)
        {
            try
            {
                appat = transform.parent.transform.parent.Find("bait").gameObject;
            } catch
            {
                lineRenderer.positionCount = 0;
                lineRenderer.enabled = false;
            }
            
        } else
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            if (top_fishing_line != null && appat != null)
            {
                Vector3 pos1 = top_fishing_line.transform.position;
                lineRenderer.SetPosition(0, pos1); // Position de départ
                lineRenderer.SetPosition(1, appat.transform.position);   // Position d'arrêt
            }

        }
    }
}
