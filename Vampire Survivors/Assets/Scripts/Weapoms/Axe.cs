using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : WeaponBehaviour
{
    int AmountOfHits = 0;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    protected override void DamageHandle()
    {
        base.DamageHandle();
        AmountOfHits++;
        if(AmountOfHits >= 5)
        {
            Destroy(gameObject);
        }
    }
}
