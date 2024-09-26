using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponManager : MonoBehaviour
{
    public int MaxUpgrades;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody2D playerRb;

    public WeaponObject[] Weapons;

    private ShockWave shockWaveScript;

    private void Awake()
    {
        shockWaveScript = FindAnyObjectByType<ShockWave>();
    }

    private void Start()
    {
        foreach (WeaponObject weapon in Weapons){
            if (!weapon.StartWeapon)
            {
                weapon.Level = 0;
            }
            else
            {
                weapon.Level = 1;
            }
        }
    }

    void Update()
    {
        if (!GameManager.PlayerDead)
        {
            foreach (WeaponObject weapon in Weapons)
            {
                if (weapon.Level > 0) {
                    weapon.Cooldown -= Time.deltaTime;

                    if (weapon.Cooldown <= 0)
                    {
                        switch (weapon.Type)
                        {
                            case WeaponType.Arrow:
                                Arrow(weapon);
                                break;
                            case WeaponType.Axe:
                                Axe(weapon);
                                break;
                            case WeaponType.Shockwave:
                                ShockWave(); 
                                break;
                            default: break;
                        }
                        weapon.Cooldown = weapon.AttackSpeed * GameManager.AttackSpeedMultiplier;
                    } 
                }
            }
        }
    }

    private void Axe(WeaponObject weapon)
    {
        for (int i = 0; i < weapon.Level; i++)
        {
            var throwingWeapon = Instantiate(weapon.Model, playerTransform.transform.position, Quaternion.identity); 
            throwingWeapon.GetComponent<Rigidbody2D>().AddForce( new Vector2(playerTransform.right.x, 2) * weapon.Speed * (i+1) + playerRb.velocity, ForceMode2D.Impulse);
            Destroy(throwingWeapon, 4);

        }
    }

    private void ShockWave()
    {
        shockWaveScript.ActivateShockWave();
    }

    private void Arrow(WeaponObject weapon)
    {
        for (int i = 0; i < weapon.Level; i++)
        {
            Instantiate(weapon.Model, new Vector2(playerTransform.position.x + i * 2, playerTransform.position.y), Quaternion.identity);
        }
    }
    
}
