using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DistanceWeapons", menuName = "DistanceWeapons", order = 1)]
public class DistanceWeapon : ScriptableObject
{
    public string nome;
    public Sprite imageWeapon;
    public int dmg;
    public string rarita;
    public int cost;
    public float speedFrequency;
    public float bulletSpeed;
    public Sprite imageBullet;
}