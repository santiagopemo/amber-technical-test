using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Tower tower;
    public float matchDuration;
    public bool matchStart = true;
    public Store store;
    public int credits = 100000;
    public int allowedIntruders = 5;
    public Text timerText;
    public Text creditText;

    private float _currentDuration;

    public static GameManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        store.credits = credits;
        tower.allowedIntruders = allowedIntruders;
    }

    void Start()
    {
        tower.TowerHasFallenEvent += GameLost;
        store.TowerBuyed += SuccessfulPurchase;
        matchStart = true;
        _currentDuration = matchDuration;

        creditText.text = $"${store.credits}";
    }

    void Update()
    {
        Utilities.UpdateTimer(matchStart, ref _currentDuration, matchDuration, GameWon);
        if (matchStart) UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        string minutes = ((int) _currentDuration / 60).ToString();
        string seconds = ((int) _currentDuration % 60).ToString("D2");

        timerText.text = $"{minutes}:{seconds}";
    }

    private void SuccessfulPurchase(int remainingCredits)
    {
        creditText.text = $"${remainingCredits}";
    }

    private void GameWon()
    {
        SceneManager.LoadScene("WinScene");
    }

    private void GameLost()
    {
        SceneManager.LoadScene("LoseScene");
    }
}
