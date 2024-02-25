using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDisplayScript : MonoBehaviour
{
    private GameObject selectedTowerDisplay;
    // Start is called before the first frame update
    void Start()
    {
        selectedTowerDisplay = transform.Find("SelectedTower").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSelectedTowerImage(GameObject tower)
    {
        selectedTowerDisplay.GetComponent<Image>().sprite = tower.GetComponent<SpriteRenderer>().sprite;
        selectedTowerDisplay.GetComponent<Image>().color = tower.GetComponent<SpriteRenderer>().color;
    }
}
