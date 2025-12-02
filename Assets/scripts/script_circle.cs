using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_circle : MonoBehaviour
{
    int life_fish;
    public  fish_script fish_script;
    private RectTransform rectTransform; 
    //private SpriteRenderer spriteRenderer;

    void Start()
    {
        life_fish = fish_script.life;
        rectTransform  = GetComponent<RectTransform>();
    }

    private void Update()
    {
        life_fish = fish_script.life;
        //transform.GetComponent<RectTransform>().transform.height = life_fish * 200 / 100;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, life_fish * 200 / 100);
    }
}
