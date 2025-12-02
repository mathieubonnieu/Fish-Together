using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class xp_script : MonoBehaviour
{
    // Start is called before the first frame update
    public int lvl = 1;
    public int Xp = 0;
    public int baseXp = 10;
    public float xpMultiplier = 1.5f;
    public GameObject xp_bar;
    private RectTransform Bar_trans;
    private int RequiredXP;
    public TextMeshProUGUI lvlText;
    public float CalculateRequiredXP(int level)
    {
        // Utilisation de la fonction logarithmique pour calculer l'XP nécessaire
        float requiredXP = baseXp * Mathf.Pow(xpMultiplier, level - 1);
        return requiredXP;
    }
    void Start()
    {
        RequiredXP = Mathf.FloorToInt(CalculateRequiredXP(lvl));
        Bar_trans = xp_bar.transform.GetComponent<RectTransform>();
        lvlText.text = lvl.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Xp >= RequiredXP) {
            lvl = lvl + 1;
            Xp = Xp - RequiredXP;
            RequiredXP = Mathf.FloorToInt(CalculateRequiredXP(lvl));
            lvlText.text = lvl.ToString();
        }
        float newWidth = (340 * ( Xp * 100 / RequiredXP) / 100);
        Bar_trans.sizeDelta = new Vector2(newWidth, Bar_trans.sizeDelta.y);
    }
}
