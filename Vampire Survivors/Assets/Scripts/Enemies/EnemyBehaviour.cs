using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent (typeof(Animator))]
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private PickUpManager pickUpManager;
    private float health; 
    private float speed;
    private float damage;

    private Playermovement player;
    private Animator anim;
    private SpriteRenderer sprite;
    private Transform target;

    private bool canDamage;
    private float damageCooldown;

    [SerializeField] float damageMultiplier;
    [SerializeField] float speedMultiplier;
    [SerializeField] float healthMultiplier;

    private void Start()
    {
        pickUpManager = FindObjectOfType<PickUpManager>();
    }

    void FixedUpdate()
    {
        Vector2 MoveDirection = target.position - transform.position;
        transform.Translate(MoveDirection.normalized * speed);

        if (!canDamage)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0) {
                canDamage = true;
            }
        }

        if (MoveDirection.x < 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }

    public void Spawn(EnemyObject type, Transform targetPos, int totalWaves)
    {
        int multiplier = Mathf.FloorToInt(GameManager.WaveLevel /totalWaves ) + 1;
        if(multiplier < 1)
        {
            multiplier = 1;
        }
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        maxHealth = type.MaxHealth;
        health = maxHealth;
        speed = type.Speed;
        damage = type.Damage;
        for (int i = 1; i < multiplier; i++)
        {
            health *= healthMultiplier;
            speed *= speedMultiplier;
            damage *= damageMultiplier;
        }

        sprite.sprite = type.Sprite;
        if(targetPos != null)
        {
            target = targetPos;
        }
        anim.runtimeAnimatorController = type.Controller;
        this.AddComponent<PolygonCollider2D>();

        sprite.sortingOrder = GameManager.EnemySortingOrder;
        GameManager.EnemySortingOrder++;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        DamagePopUps.Create(transform.position, Mathf.RoundToInt(damage).ToString());
        if (health < 0)
        {
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Playermovement>();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (canDamage)
            {
                player.TakeDamage(damage);
                canDamage = false;
                damageCooldown = 0.1f;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }

    private void Death()
    {
        pickUpManager.Drop(transform.position);
        GameManager.TotalEnemies--;
        Destroy(gameObject);
    }
}
