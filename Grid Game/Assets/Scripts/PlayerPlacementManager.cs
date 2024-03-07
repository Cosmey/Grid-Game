using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlacementManager : MonoBehaviour
{
    private bool placingTowers;
    private GameObject selectedTower;
    private GameObject moneyManager;
    [SerializeField] private TowerDisplayScript myTowerDisplayScript;
    [SerializeField] List<GameObject> towerTypes = new List<GameObject>();
    /*[SerializeField] private GameObject basicTower;
    [SerializeField] private GameObject wallTower;*/
    private int currentTowerNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetupList();
    }

    private void SetupList()
    {
        moneyManager = GameObject.Find("MoneyManager");
        selectedTower = towerTypes[currentTowerNum];
        myTowerDisplayScript.SetSelectedTowerImage(selectedTower);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CycleTowers()
    {
        currentTowerNum++;
        if(currentTowerNum >= towerTypes.Count)
        {
            currentTowerNum = 0;
        }
        selectedTower = towerTypes[currentTowerNum];
        myTowerDisplayScript.SetSelectedTowerImage(selectedTower);
    }
    public void SelectTower(string towerName)
    {
        if(towerName == "basicTower")
        {
            selectedTower = towerTypes[0];
        }
        else if(towerName == "wallTower")
        {
            selectedTower = towerTypes[1];
        }

        currentTowerNum = towerTypes.IndexOf(selectedTower);
        myTowerDisplayScript.SetSelectedTowerImage(selectedTower);
    }
    public void TogglePlacingTowers()
    {
        placingTowers = !placingTowers;
    }
    public void PlaceObject()
    {
        Vector2 mousePos = (Vector2)GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        mousePos.Set(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
        int cost = selectedTower.GetComponent<Entity>().GetCost();
        if (TowerManager.getBuilding(mousePos) == null && moneyManager.GetComponent<MoneyManagerScript>().SubtractAndCheckMoney(cost)) {
            GameObject tower = Instantiate(selectedTower, GameObject.Find("Towers").transform);
            tower.transform.localPosition = mousePos;
            if (TowerManager.setBuilding(mousePos, tower))
            {
                
            } else
            {
               //Debug.Log("Invalid Placement!");
            }
        } else
        {
            //Debug.Log("Existing Tower There!");
        }

        
    }
}
