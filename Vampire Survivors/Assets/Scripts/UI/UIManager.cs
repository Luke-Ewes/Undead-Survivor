using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerLbl;
    [SerializeField] private TextMeshProUGUI killCounterLbl;
    [SerializeField] private TextMeshProUGUI levelCounterLbl;
    [SerializeField] private TextMeshProUGUI coinCounterLbl;
    [SerializeField] private Image healthBar;

    [SerializeField] private Playermovement player;

    private float timer;

    public GameObject PausePanel;
    [SerializeField] private GameObject StartPanel;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCoinCounter(0);
        UpdateLevelCounter();
        UpdateKillCounter(0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        System.TimeSpan t = System.TimeSpan.FromSeconds(timer);
        timerLbl.text = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);

        healthBar.fillAmount = player.Health / GameManager.PlayerMaxHealth;
    }

    public void UpdateKillCounter(int totalKills)
    {
        killCounterLbl.text = totalKills.ToString();
    }

    public void UpdateLevelCounter()
    {
        levelCounterLbl.text = "Level: " + GameManager.PlayerLevel.ToString();
    }

    public void UpdateCoinCounter(int coins)
    {
        coinCounterLbl.text = coins.ToString();
    }


    //Button functions

    public void Continue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1.0f;
        GameManager.paused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        StartPanel.SetActive(false);
        GameManager.started = true;
        Time.timeScale = 1;
        GameManager.paused = false;
    }
}
