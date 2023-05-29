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
    void Awake(){
        btnBuy.onClick.AddListener(()=>{
            buy();
        });
    }
    void buy(){
        if(this.gameObject.GetComponent<PlayerGameManager>().myStats.coins >= item.cost){
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    if(this.gameObject.GetComponent<PlayerInventory>().inventoryItems[r,c] != null){
                        this.gameObject.GetComponent<PlayerInventory>().inventoryItems[r,c] = new PlayerInventory.Item();
                        this.gameObject.GetComponent<PlayerInventory>().inventoryItems[r,c]._name = item.name;
                        break;
                    }
                }
            }
        }
    }
}
