using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", order = 1)]
public class ItemsScriptable : ScriptableObject
{
    public string nome;
    public enum Type
    {
        Armor, Weapon
    }
    public Type typeItem;
    public Sprite image_weapon_Armor;
    public Sprite image_bullet;
    public bool DistanceWeapon_MeleeWeapon; /* true = distance / false = melee */ 
    public int valoreArmatura;
    public int damage;
    public double lunghezza, larghezza;
    public int cost;
}