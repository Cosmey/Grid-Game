using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlacementManager : MonoBehaviour
{
    private bool placingTowers;
    private GameObject selectedTower;
    private TowerDisplayScript myTowerDisplayScript;
    [SerializeField] private GameObject basicTower;
    [SerializeField] private GameObject wallTower;
    private List<GameObject> towerList;
    private int currentTowerNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetupList();
    }

    private void SetupList()
    {
        myTowerDisplayScript = GameObject.Find("CurrentTowerDisplay").GetComponent<TowerDisplayScript>();
        towerList = new List<GameObject>();
        towerList.Add(basicTower);
        towerList.Add(wallTower);
        selectedTower = towerList[currentTowerNum];
        myTowerDisplayScript.SetSelectedTowerImage(selectedTower);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CycleTowers()
    {
        currentTowerNum++;
        if(currentTowerNum >= towerList.Count)
        {
            currentTowerNum = 0;
        }
        selectedTower = towerList[currentTowerNum];
        myTowerDisplayScript.SetSelectedTowerImage(selectedTower);
    }
    public void SelectTower(string towerName)
    {
        if(towerName == "basicTower")
        {
            selectedTower = basicTower;
        }
        else if(towerName == "wallTower")
        {
            selectedTower = wallTower;
        }

        currentTowerNum = towerList.IndexOf(selectedTower);
        myTowerDisplayScript.SetSelectedTowerImage(selectedTower);
    }
    public void TogglePlacingTowers()
    {
        placingTowers = !placingTowers;
    }
    public void PlaceObject()
    {
        GameObject tower = Instantiate(selectedTower, GameObject.Find("Towers").transform);
        Vector2 mousePos = (Vector2)GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        tower.transform.localPosition = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
        TowerManager.setBuilding((int) Mathf.Round(mousePos.x), (int) Mathf.Round(mousePos.y), true);
    }
}
