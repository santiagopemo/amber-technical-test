using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public int credits = 100000;

    private TowerSelectionButton[] _towerSelectionButtons;
    private TowerPlacementGrid[] _towerPlacementGrids;

    public delegate void OnBuy(int remainigCredits);
    public event OnBuy TowerBuyed;

    public static Store Instance { get; private set; }

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
        _towerSelectionButtons = FindObjectsOfType<TowerSelectionButton>();
        _towerPlacementGrids = Resources.FindObjectsOfTypeAll<TowerPlacementGrid>();
    }

    private void Start()
    {
        CheckAffordability();
        SubscribeToGrids();
    }

    private void CheckAffordability()
    {
        for (int i = 0; i < _towerSelectionButtons.Length; i++)
        {
            Button button = _towerSelectionButtons[i].GetComponent<Button>();
            if (_towerSelectionButtons[i].itemCost <= credits)
            {                
                button.interactable = true;
            }
            else
                button.interactable = false;
        }
    }

    private void SubscribeToGrids()
    {
        for (int i = 0; i < _towerPlacementGrids.Length; i++)
        {
            _towerPlacementGrids[i].TowerHasBeenPlaced += Buy;
        }
    }

    private void Buy(int cost)
    {
        credits -= cost;
        credits = credits < 0 ? 0 : credits;
        if (credits < cost)
        {
            _towerPlacementGrids[0].DisableAllGrids();
        }
        CheckAffordability();
        TowerBuyed?.Invoke(credits);
    }
}
