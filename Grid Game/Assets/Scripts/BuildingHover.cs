using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.Set(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));

        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        GameObject activeBuilding = PlayerPlacementManager.instance.selectedTower;
        if(activeBuilding != null) transform.localScale = new Vector2(activeBuilding.transform.localScale.x, activeBuilding.transform.localScale.y);
    }
}
