using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text_scale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Scale = transform.localScale;
        Scale.x = 1;
        Scale.y = 1;
        Scale.z = 1;
        transform.localScale = Scale;
    }
}
