using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum WeaponType { Arrow, Axe, Shockwave, Energy}

[CreateAssetMenu(fileName = "New Weapon", menuName ="ScriptableObjects/WeaponObject", order = 1)]
public class WeaponObject : ScriptableObject
{
    public float Damage;
    public float Cooldown;
    public float AttackSpeed;
    public float Speed;

    public int Level;

    public bool StartWeapon;

    public WeaponType Type;

    public GameObject Model;

}
