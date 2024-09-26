using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    [SerializeField] private GameObject pickUpPrefab;
    [SerializeField] private PickUpObject[] pickUpObjects;
    [SerializeField] private Playermovement player;
    [SerializeField] private float dropChance;
    [SerializeField] private int maxUpgrades;

    private int KillsSinceLastDrop;
    private int kills;
    private int coins;
    private UIManager uiManager;

    private List<PickUpObject> PossiblePickUps = new List<PickUpObject>();
    private EnergyBallParent energyBall;

    private void Awake()
    {
        energyBall = FindAnyObjectByType<EnergyBallParent>();
        uiManager = GetComponent<UIManager>();
    }

    private void Start()
    {
        foreach (var pickUpObject in pickUpObjects)
        {
            PossiblePickUps.Add(pickUpObject);
        }
        foreach (var pickUpObject in pickUpObjects)
        {
            if (!pickUpObject.Weapon.StartWeapon)
            {
                pickUpObject.dropped = 0;
            }
            else
            {
                pickUpObject.dropped = 1;
            }
        }
    }
    public void ItemPickedUp(PickUpObject pickUp)
    {
        switch (pickUp.Type)
        {
            case PickUpType.weapon:
                UpgradeLevel(pickUp.Weapon);
                break;
            case PickUpType.coin:
                Coin();
                break;
            case PickUpType.health:
                Health();
                break;
            default:
                break;
        }
    }

    public void Drop(Vector3 pos)
    {
        kills++;
        uiManager.UpdateKillCounter(kills);
        KillsSinceLastDrop++;
        if (Random.Range(0, 100) <= dropChance || KillsSinceLastDrop >= 20)
        {
            KillsSinceLastDrop = 0;
            if (PossiblePickUps.Count != 0) {
                PickUpObject currentObject = PossiblePickUps[Random.Range(0, PossiblePickUps.Count)];
                Instantiate(pickUpPrefab, pos, Quaternion.identity).GetComponent<PickUp>().Spawn(currentObject);
                currentObject.dropped++;
                if(currentObject.dropped >= maxUpgrades && currentObject.Type != PickUpType.coin && currentObject.Type != PickUpType.health)
                {
                    PossiblePickUps.Remove(currentObject);
                }
            }
        }
    }

    private void UpgradeLevel(WeaponObject weapon)
    {
        weapon.Level++;
        energyBall.SpawnNewBall();
    }

    private void Coin()
    {
        coins++;
        if (coins >= 15)
        {
            coins = 0;
            GameManager.DamageMultiplier += 0.2f;
            GameManager.AttackSpeedMultiplier -= 0.05f;
            if (GameManager.AttackSpeedMultiplier < 0.6f)
            {
                GameManager.AttackSpeedMultiplier = 0.6f;
            }
            GameManager.PlayerMaxHealth += 100;
            GameManager.PlayerLevel++;
            uiManager.UpdateLevelCounter();
            player.UpdateHealth(100);
            player.LevelUp();
        }
        uiManager.UpdateCoinCounter(coins);
    }

    private void Health()
    {
        player.UpdateHealth(200);
    }
}
