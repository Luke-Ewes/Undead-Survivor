using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Playermovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private PickUpManager pickUpManager;

    private Rigidbody2D rb;
    private PlayerInput playerActions;
    private InputAction playerMove;
    private Animator anim;

    public bool CanMove = true;
    public float Health;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerActions = new PlayerInput();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        Health = GameManager.PlayerMaxHealth;
    }
    private void OnEnable()
    {
        playerMove = playerActions.Player.Move;
        playerMove.Enable();
    }

    private void OnDisable()
    {
        playerMove.Disable();
    }

    private void Update()
    {
        if (rb.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (rb.velocity.x > 0.1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (CanMove)
        {
            Move();
        }
        
        if (rb.velocity != Vector2.zero) {
            anim.SetBool("Walking", true);
                }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    private void Move()
    {
        Debug.Log("Move");
        Vector2 direction = playerMove.ReadValue<Vector2>();
        rb.velocity = direction * speed * Time.deltaTime;
        Debug.Log(direction);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {
            pickUpManager.ItemPickedUp(collision.gameObject.GetComponent<PickUp>().type);
            Destroy(collision.gameObject);
        }
    }

    private void Death()
    {
        gameObject.SetActive(false);
        GameManager.PlayerDead = true;
    } 

    public void UpdateHealth(float HealthIncrease)
    {
        Health += HealthIncrease;
        if (Health > GameManager.PlayerMaxHealth)
        {
            Health = GameManager.PlayerMaxHealth;
        }
    }

    public void LevelUp()
    {
        DamagePopUps.Create(transform.position, "Level Up");

    }
}
