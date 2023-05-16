using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    public WeponsManager id;

    public void select(){

        int coins = GameObject.Find("Player").GetComponent<PlayerController>().coin;

        if(id.cost < coins){
            GameObject.Find("GameManager").GetComponent<GameManager>().weaponSelected = id;
            
            GameObject.Find("Player").GetComponent<PlayerController>().coin -= id.cost;
        }

    }
}
