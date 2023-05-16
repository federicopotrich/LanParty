using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public int idShopSeller = 0; // 0 gasperini - 1 Sanni♥ - 2 trentini
    public UnityEngine.UI.Image imageSeller;
    public Sprite gasperini, sannicolo, trentini;
    void Start()
    {

    }

    void Update()
    {
        switch (idShopSeller)
        {
            case 0:
                imageSeller.sprite = gasperini;
                break;
            case 1:
                imageSeller.sprite = sannicolo;
                break;
            case 2:
                imageSeller.sprite = trentini;
                break;
        }        
    }
}
