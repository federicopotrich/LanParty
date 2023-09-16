using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScriptSlotShopNet : MonoBehaviour
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
            if (GameObject.Find("Player").GetComponent<PlayerInventory>().ContainsItem(tmpItem))
            {
                // Se l'oggetto è già presente nell'inventario, lo equipaggia
                EquipItem(tmpItem);
            }
            else
            {
                // Altrimenti, lo compra e lo aggiunge all'inventario
                BuyItem(tmpItem);
            }
        }
    }

    private void EquipItem(ItemsScriptable item)
    {
        if (isArmor)
        {
            GameObject.Find("Player").GetComponent<Stats>().setArmor(item.valoreArmatura);
        }
        else
        {
            GameObject.Find("Player").GetComponent<Stats>().setDamageDealth(item.damage);
        }

        // Cambia il testo del pulsante in "Equip"
        btnBuy.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
    }

    private void BuyItem(ItemsScriptable item)
    {
        // Effettua l'acquisto e aggiunge l'oggetto all'inventario
        GameObject.Find("Player").GetComponent<PlayerGameManager.Stats>().coins -= item.cost;
        GameObject.Find("Player").GetComponent<PlayerInventory>().AddItem(item);

        // Disabilita il pulsante dopo l'acquisto
        btnBuy.interactable = false;
    }
}
