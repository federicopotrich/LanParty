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

    }
}
