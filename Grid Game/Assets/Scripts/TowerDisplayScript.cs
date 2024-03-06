using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerDisplayScript : MonoBehaviour
{
    private GameObject selectedTowerDisplay;
    private GameObject costText;
    // Start is called before the first frame update
    void Awake()
    {
        selectedTowerDisplay = transform.Find("SelectedTower").gameObject;
        costText = transform.Find("CostText").gameObject;
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
    }
}
