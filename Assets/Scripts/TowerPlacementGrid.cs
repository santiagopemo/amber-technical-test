using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementGrid : MonoBehaviour
{
    public GameObject towerToPlace;
    public int towerToPlaceCost;

    private TowerPlacementGrid[] _otherGrids;

    public delegate void OnTowerPlaced(int cost);
    public event OnTowerPlaced TowerHasBeenPlaced;

    private void Awake()
    {
        _otherGrids = Resources.FindObjectsOfTypeAll<TowerPlacementGrid>();
    }

    public void DisableAllGrids()
    {
        if (_otherGrids == null) return;

        for (int i = 0; i < _otherGrids.Length; i++)
        {
            _otherGrids[i].gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public void TowerPlaced()
    {
        TowerHasBeenPlaced?.Invoke(towerToPlaceCost);
    }
}
