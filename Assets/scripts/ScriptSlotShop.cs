using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScriptSlotShop : MonoBehaviour
{
    public ItemsScriptable item;
    public TextMeshProUGUI txtName, txtCost, txtDmg;
    public Button btnBuy;
    public bool isArmor;


    void Awake()
    {
        txtName.text = item.nome;
        txtCost.text = "Cost: " + item.cost;
        if (isArmor)
        {
            txtDmg.text = "Armor: " + item.valoreArmatura;
        }
        else
        {
            txtDmg.text = "Damage: " + item.damage;
        }

        btnBuy.onClick.AddListener(Buy);
    }

    public void Buy()
    {
        ItemsScriptable tmpItem = new ItemsScriptable();
        tmpItem.nome = item.nome;
        if (GameObject.Find("Player").GetComponent<PlayerGameManager.Stats>().coins >= item.cost)
        {
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    if (!GameObject.Find("Player").GetComponent<PlayerInventory>().ContainsItem(tmpItem))
                    {
                        GameObject.Find("Player").GetComponent<PlayerInventory>().AddItem(tmpItem);
                        break;
                    }
                }
            }
        }
    }
}
