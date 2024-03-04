using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TowerManager : MonoBehaviour
{

    static int MAX_WIDTH = 101;
    static int MAX_HEIGHT = 101;
    private static GameObject[,] buildings = new GameObject[MAX_WIDTH, MAX_HEIGHT];

    // Start is called before the first frame update
    void Start()
    {
        //buildings = new bool[MAX_WIDTH,MAX_HEIGHT];
        for(int y=0;y<MAX_HEIGHT;y++) 
        {
            for(int x=0;x<MAX_WIDTH;x++)
            {
                buildings[x,y] = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameObject getBuilding(Vector2 pos)
    {
        return getBuilding((int) Mathf.Round(pos.x), (int) Mathf.Round(pos.y));
    }

    public static GameObject getBuilding(int x, int y)
    {
        int finX = x + MAX_WIDTH / 2;
        int finY = y + MAX_HEIGHT / 2;
        if (finX < 0 || finX >= MAX_WIDTH || finY < 0 || finY >= MAX_HEIGHT)
        {
            return null;
        }
        return buildings[finX, finY];
    }

    public static bool setBuilding(Vector2 pos, GameObject building)
    {
        return setBuilding((int)Mathf.Round(pos.x), (int)Mathf.Round(pos.y), building);
    }

    public static bool setBuilding(int x, int y, GameObject building)
    {
        int finX = x + MAX_WIDTH / 2;
        int finY = y + MAX_HEIGHT / 2;
        if (finX < 0 || finX >= MAX_WIDTH || finY < 0 || finY >= MAX_HEIGHT)
        {
            return false;
        }
        buildings[finX,finY] = building;

        return true;
    }
}
