//coded by reece

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerDisplayScript : MonoBehaviour
{
    private GameObject selectedTowerDisplay;
    private GameObject costText;
    private GameObject towerName;
    // Start is called before the first frame update
    void Awake()
    {
        selectedTowerDisplay = transform.Find("SelectedTower").gameObject;
        costText = transform.Find("CostText").gameObject;
        towerName = transform.Find("TowerName").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSelectedTowerImage(GameObject tower)
    {
        selectedTowerDisplay.GetComponent<Image>().sprite = tower.GetComponent<SpriteRenderer>().sprite;
        selectedTowerDisplay.GetComponent<Image>().color = tower.GetComponent<SpriteRenderer>().color;
        costText.GetComponent<TextMeshProUGUI>().text = "$" + tower.GetComponent<Entity>().GetCost();
        towerName.GetComponent<TextMeshProUGUI>().text = tower.name;

    }
}
