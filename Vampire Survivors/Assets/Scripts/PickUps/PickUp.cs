using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public PickUpObject type;

    public void Spawn(PickUpObject pickUp)
    {
        type = pickUp;
        GetComponent<SpriteRenderer>().sprite = type.Sprite;
    }
}
