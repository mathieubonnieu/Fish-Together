using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    public List<Item> inventoryTab = new List<Item>();
    int x = 0;
    void Start()
    {
        x = inventoryTab.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(x != inventoryTab.Count) {
            x = x + 1;
            Transform parentTransform = transform;
            /*Transform child = parentTransform.GetChild(0);
            Item_display childScript = child.GetComponent<Item_display>();
            childScript.item = inventoryTab[x - 1];*/
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                if (i == x - 1)
                {
                    Transform child = parentTransform.GetChild(i);
                    Item_display childScript = child.GetComponent<Item_display>();

                    // Vérifiez si le script existe
                    if (childScript != null)
                    {
                        // Modifiez la valeur publique du script
                        childScript.item = inventoryTab[x - 1];
                    }
                    // Faites quelque chose avec l'enfant, par exemple, imprimez le nom
                    //Debug.Log("Nom de l'enfant : " + child.name);
                }
            }
        }
            //Debug.Log("inventory" + inventoryTab.Count);
       
        
        
    }
}
