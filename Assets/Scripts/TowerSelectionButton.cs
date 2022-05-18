using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelectionButton : MonoBehaviour
{
    public TowerPlacementGrid[] towerPlacementGrids;
    public GameObject towerPrefab;
    public int itemCost = 150;

    private Text _costText;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ActivateGrids);
        _costText = transform.GetChild(0).GetComponent<Text>();
        _costText.text = $"${itemCost}";
    }

    public void ActivateGrids()
    {
        towerPlacementGrids[0]?.DisableAllGrids();
        for (int i = 0; i < towerPlacementGrids.Length; i++)
        {
            towerPlacementGrids[i].gameObject.SetActive(true);
            towerPlacementGrids[i].towerToPlace = towerPrefab;
            towerPlacementGrids[i].towerToPlaceCost = itemCost;
        }
    }
}
