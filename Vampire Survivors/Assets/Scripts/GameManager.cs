using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    public static int WaveLevel;
    public static float DamageMultiplier;
    public static float AttackSpeedMultiplier;
    public static int TotalEnemies;
    public static float PlayerMaxHealth;
    public static int PlayerLevel;
    public static bool PlayerDead;
    public static int EnemySortingOrder;


    [SerializeField] private InputAction pauseAction;
    [SerializeField] private float playerHealth;

    private UIManager uiManager;
    public static bool started;
    public static bool paused = true;

    private void Awake()
    {
        DamageMultiplier = 1;
        AttackSpeedMultiplier = 1;
        PlayerMaxHealth = playerHealth;
        PlayerDead = false;
        started = false;
        Time.timeScale = 0;
        uiManager = GetComponent<UIManager>();
    }

    private void OnEnable()
    {
        pauseAction.Enable();
        pauseAction.performed += Pause;
    }

    private void Update()
    {
        playerHealth = PlayerMaxHealth;
        if(EnemySortingOrder > 300)
        {
            EnemySortingOrder = 0;
        }
    }

    private void Pause(InputAction.CallbackContext context)
    {
        if(started)
        {
            if (!paused)
            {
                Time.timeScale = 0;
                uiManager.PausePanel.SetActive(true);
                paused = true;
            }
            else
            {
                uiManager.Continue();
            }
        }
    }
}
