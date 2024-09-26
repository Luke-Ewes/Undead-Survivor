using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offSet;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.position = target.position + offSet;
    }
}
