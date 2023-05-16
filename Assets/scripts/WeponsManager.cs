using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Weapons", order = 1)]
public class WeponsManager : ScriptableObject
{
    public string nome;
    public Sprite imageWeapon;
    public int dmg;
    public string rarita;
    public int cost;
    public float speedFrequency;

}
