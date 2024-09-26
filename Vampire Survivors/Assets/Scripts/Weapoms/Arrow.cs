using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : WeaponBehaviour
{
    [SerializeField]private Transform target;


    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemies;

    // Start is called before the first frame update
    private void Start()
    {
        Spawn();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(target != null){
            transform.right = target.position - transform.position;
            rb.velocity = transform.right * speed * Time.deltaTime;
        }
        else
        {
            target = FindTarget();
        }
    }

    public override void Spawn()
    {
        base.Spawn();
        target = FindTarget(); 
    }

    protected override void DamageHandle()
    {
        base.DamageHandle();
        Destroy(gameObject);
    }

    private Transform FindTarget()
    {
        Transform bestTarget = null;
        float closestTarget = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(currentPosition, radius, enemies);
        Debug.DrawRay(currentPosition, transform.forward * (radius /2), Color.yellow, 1.5f);

        if(enemiesInRange == null)
        {
            return null;
        }
        else
        {
            foreach(Collider2D coll in enemiesInRange)
            {
                Transform EnemyPosition = coll.gameObject.GetComponent<Transform>();

                Vector3 DirectionToTarget = EnemyPosition.position - currentPosition;
                float DistanceToTarget = DirectionToTarget.sqrMagnitude;
                if(DistanceToTarget < closestTarget)
                {
                    closestTarget = DistanceToTarget;
                    bestTarget = EnemyPosition;
                }
            }
            return bestTarget;
        }
    }
}
