using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyType", menuName = "ScriptableObjects/EnemyType", order = 0)]
public class EnemyObject : ScriptableObject
{
    public float MaxHealth;
    public float Speed;
    public float Damage;

    public Sprite Sprite;
    public RuntimeAnimatorController Controller;
} 
