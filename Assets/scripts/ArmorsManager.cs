using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armors", menuName = "Armor", order = 1)]
public class ArmorsManager : ScriptableObject
{
    public string nome;
    public Sprite imageArmor;
    public int defence;
    public string rarita;
    public int cost;
    public string nomeAnimazione;
}