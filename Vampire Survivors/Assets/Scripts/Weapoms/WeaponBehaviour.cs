using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class WeaponBehaviour : MonoBehaviour
{
    protected float damage;
    protected float speed;
    protected Rigidbody2D rb;
    [SerializeField] protected WeaponObject weapon;

    public virtual void Spawn()
    {
        damage = weapon.Damage;
        speed = weapon.Speed;
        gameObject.TryGetComponent(out Rigidbody2D tempRb);
        rb=tempRb;
    }

    protected virtual void DealDamage(float damage, EnemyBehaviour enemy)
    {
        enemy.TakeDamage(damage);
        DamageHandle();
    }

    protected virtual void DamageHandle() { }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            DealDamage(damage * GameManager.DamageMultiplier, collision.gameObject.GetComponent<EnemyBehaviour>());
        }
    }
}
