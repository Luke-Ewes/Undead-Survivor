using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickUpType { weapon, coin, health }

[CreateAssetMenu(fileName ="New PickUp", menuName = "ScriptableObjects/PickUpObject", order = 2)]
public class PickUpObject : ScriptableObject
{
    public PickUpType Type;
    public Sprite Sprite;
    public int dropped;

    public WeaponObject Weapon;
}
