using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class result_script : MonoBehaviourPun
{
    public Item[] ItemTab = new Item[5];
    void Start()
    {
        PhotonNetwork.SetInterestGroups(1, true);
        PhotonNetwork.SetInterestGroups(2, false);


        GameObject inventoryObject = GameObject.Find("Inventory");
        GameObject Xp_bar = GameObject.Find("xp_bar");
        if (inventoryObject != null)
        {
            inventory childScript = inventoryObject.GetComponent<inventory>();
            Item RandItem = ItemTab[Random.Range(0, 5)];
            xp_script xpScript = Xp_bar.GetComponent<xp_script>();
            if (childScript.inventoryTab.Find(item => item.name == RandItem.name))
            {
                //childScript.inventoryTab.Find
                Item foundItem = childScript.inventoryTab.Find(item => item.name == RandItem.name);
                foundItem.quantity += RandItem.quantity;
                xpScript.Xp += RandItem.xp;

            }
            else
            {
                //comment ajouter une copie
                childScript.inventoryTab.Add(RandItem.Copy());
                xpScript.Xp += RandItem.xp;
            }
            
            
            //Xp_bar.Xp
        }
        else
        {
            Debug.LogError("GameObject avec le nom 'Inventory' non trouvé.");
        }


        Destroy(gameObject, 3);
    }

    void Update()
    {
        
    }
}
