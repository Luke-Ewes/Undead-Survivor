using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : WeaponBehaviour
{
    private bool canDamage;
    private float damageCooldown;

    private void OnTriggerStay2D(Collider2D collision)
    {
        damageCooldown -= Time.deltaTime;
        if (damageCooldown < 0)
        {
            canDamage = true;
        }

        if (canDamage)
        {
            base.OnTriggerEnter2D(collision);
            damageCooldown = weapon.AttackSpeed;
            canDamage = false;
        }
    }
        public override void Spawn()
        {
            base.Spawn();
        }
}
