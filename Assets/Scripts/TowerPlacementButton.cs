using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPlacementButton : MonoBehaviour
{
    private TowerPlacementGrid _towerPlacementGrid;
    private GameObject _tower;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlaceTower);
    }

    private void Update()
    {
        if ((Input.GetButtonDown("Cancel") || Input.GetButtonDown("Fire2")) 
            && _towerPlacementGrid)
        {
            _towerPlacementGrid.DisableAllGrids();
            _towerPlacementGrid.towerToPlace = null;
        }
    }

    public void PlaceTower()
    {
        _towerPlacementGrid = SearchForGridInParent();
        _tower = _towerPlacementGrid.towerToPlace;
        Instantiate(_tower, transform.position, _tower.transform.rotation);
        _towerPlacementGrid.TowerPlaced();
    }

    private TowerPlacementGrid SearchForGridInParent()
    {
        for (Transform parent = transform.parent; parent != null; parent = parent.parent)
        {
            TowerPlacementGrid grid = parent.GetComponent<TowerPlacementGrid>();
            if (grid) return grid;
        }
        return null;
    }
}
