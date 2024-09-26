using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnergyBallParent : MonoBehaviour
{
    [SerializeField] List<GameObject> EnergyBalls = new List<GameObject>();
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float radiusOfset;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private WeaponObject EnergySCROBJ;
    private float speed;

    [SerializeField] List<float> angles = new List<float>();
    private Vector2 playerPosition;

    private void Start()
    {
        speed = rotationSpeed;
        EnergySCROBJ.Level = 0;
    }

    void FixedUpdate()
    {
        playerPosition = playerTransform.position;

        for (int i = 0; i < EnergyBalls.Count; i++) {
            EnergyBalls[i].transform.localPosition = Quaternion.Euler(0, 0, angles[i]) * Vector3.right * radiusOfset;
        }
        transform.Rotate(0, 0, rotationSpeed);
        transform.position = playerPosition;

        if (GameManager.PlayerDead)
        {
            Destroy(gameObject);
        }   
    }
    
    public void SpawnNewBall()
    {
        if(EnergyBalls.Count < EnergySCROBJ.Level)
        {
            var TempBall = Instantiate(EnergySCROBJ.Model, transform);
            TempBall.GetComponent<EnergyBall>().Spawn();
            EnergyBalls.Add(TempBall);
            angles.Add(0);
            for (int i = 0; i < EnergyBalls.Count; i++)
            {
                angles[i] = (360 * i) / EnergyBalls.Count;
            }
        }
    }
}
