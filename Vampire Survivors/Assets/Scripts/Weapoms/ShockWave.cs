using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ShockWave : WeaponBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float radius1;
    [SerializeField] private float radius2;
    [SerializeField] private float radius3;
    [SerializeField] private LayerMask enemiesMask;
    private ParticleSystem particle;


    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        damage = weapon.Damage * GameManager.DamageMultiplier;
    }

    private void Update()
    {
        transform.position = playerTransform.position;
    }

    public void ActivateShockWave()
    {
        particle.Play();
        StartCoroutine(ShockWaveHitboxes());
    }

    private void DamageDistribution(List<Collider2D> enemies, int instance)
    {
        List<Collider2D> currentEnemies = new List<Collider2D>();
        List<Collider2D> previousEnemies = new List<Collider2D>();

        currentEnemies.AddRange(enemies);

        foreach (var enemy in currentEnemies)
        {
            previousEnemies.RemoveAll(eachTarget => { return eachTarget == null; });
            if (!enemy.IsDestroyed() && !previousEnemies.Contains(enemy)) {
        
                enemy.TryGetComponent(out EnemyBehaviour Enemy);
                previousEnemies.Add(enemy);
                DealDamage(damage * GameManager.DamageMultiplier * weapon.Level /  2  / instance, Enemy);
                Debug.Log("Damnage dealth " + damage / instance);
            }
            else
            {
                continue;
            }
        }

        if(instance == 3)
        {
            previousEnemies.Clear();
            currentEnemies.Clear();
        }
        else
        {
            currentEnemies.Clear();
        }
    }

    IEnumerator ShockWaveHitboxes()
    {
        List<Collider2D> enemies = new List<Collider2D>();

        enemies = Physics2D.OverlapCircleAll(transform.position, radius1, enemiesMask).ToList();
        DamageDistribution(enemies,1);
        enemies.RemoveAll(eachTarget => { return eachTarget == null; });
        yield return new WaitForSeconds(0.5f); 
        enemies = Physics2D.OverlapCircleAll(transform.position, radius2, enemiesMask).ToList();
        DamageDistribution(enemies,2);
        enemies.RemoveAll(eachTarget => { return eachTarget == null; });
        yield return new WaitForSeconds(0.5f);
        enemies = Physics2D.OverlapCircleAll(transform.position, radius3, enemiesMask).ToList();
        DamageDistribution(enemies,3);
        enemies.RemoveAll(eachTarget => { return eachTarget == null; });
        yield return null;
    }
}
