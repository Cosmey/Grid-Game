using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TowerManager : MonoBehaviour
{

    static int MAX_WIDTH = 101;
    static int MAX_HEIGHT = 101;
    private static GameObject[,] buildings = new GameObject[MAX_WIDTH, MAX_HEIGHT];

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

        int radius = building.GetComponent<Entity>().radius - 1;
        

        //minX, maxX, minY, maxY
        if (finX - radius < 0 || finX + radius >= MAX_WIDTH || finY - radius < 0 || finY + radius >= MAX_HEIGHT)
        {
            return false;
        }
        for (int fx = -radius; fx <= radius; fx++) { 
            for(int fy = -radius; fy <= radius; fy++)
            {
                buildings[finX+fx, finY+fy] = building;
            }
        }
        

        return true;
    }
}
