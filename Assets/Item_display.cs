using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Item_display : MonoBehaviour
{
    // Start is called before the first frame update

    public Item item;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI quantityText;
    public Image artworkImage;
    void Start()
    {
        if (item != null)
        {
            artworkImage.sprite = item.artwork;
            nameText.text = item.name;
            quantityText.enabled = true;
            quantityText.text = item.quantity.ToString();
        }
        else
        {
            quantityText.enabled = false;
            nameText.text = "";
            quantityText.text = 0.ToString();
        }
    }

    void Update()
    {
        if (item != null)
        {
            artworkImage.sprite = item.artwork;
            nameText.text = item.name;
            quantityText.enabled = true;
            quantityText.text = item.quantity.ToString();
        }
        else
        {
            nameText.text = "";
            quantityText.enabled = false;
            quantityText.text = 0.ToString();
        }
    }
}
