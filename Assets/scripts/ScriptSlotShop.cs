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
    void Awake(){

        txtName.text = item.nome;
        txtCost.text = "Cost: "+item.cost ;
        if(isArmor){
            txtDmg.text = "Armor: "+item.valoreArmatura;
        }else{
            txtDmg.text = "Armor: "+item.damage;
        }

        btnBuy.onClick.AddListener(()=>{
            buy();
        });
    }
    public void buy(){
        for (int row = 0; row < 3; row++)
        {
            string rowString = "";
            for (int col = 0; col < 4; col++)
            {
                rowString += this.gameObject.GetComponent<PlayerInventory>().inventoryItems[row,col] + "  |  ";
            }
            Debug.Log(rowString);
        }
        if(this.gameObject.GetComponent<PlayerGameManager>().myStats.coins >= item.cost){
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    if(this.gameObject.GetComponent<PlayerInventory>().inventoryItems[r,c] != null){
                        this.gameObject.GetComponent<PlayerInventory>().inventoryItems[r,c] = new PlayerInventory.Item();
                        this.gameObject.GetComponent<PlayerInventory>().inventoryItems[r,c]._name = item.nome;
                        break;
                    }
                }
            }
        }
    }
}
