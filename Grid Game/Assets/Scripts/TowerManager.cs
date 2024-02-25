using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{

    static int MAX_WIDTH = 101;
    static int MAX_HEIGHT = 101;
    private static bool[,] buildings;

    // Start is called before the first frame update
    void Start()
    {
        buildings = new bool[MAX_HEIGHT,MAX_WIDTH];
        for(int y=0;y<MAX_HEIGHT;y++) 
        {
            for(int x=0;x<MAX_WIDTH;x++)
            {
                buildings[y,x] = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool isBuilding(int x, int y)
    {
        int finX = x + MAX_WIDTH / 2;
        int finY = y + MAX_HEIGHT / 2;
        //Debug.Log(finX + ", " + finY);
        if(finX < 0 || finX >= MAX_WIDTH || finY <  0 || finY >= MAX_HEIGHT)
        {
            return false;
        }
        return buildings[finY,finX];
    }

    public static bool setBuilding(int x, int y, bool isBuilding)
    {
        int finX = x + MAX_WIDTH / 2;
        int finY = y + MAX_HEIGHT / 2;
        if (finX < 0 || finX >= MAX_WIDTH || finY < 0 || finY >= MAX_HEIGHT)
        {
            return false;
        }
        buildings[finY,finX] = isBuilding;

        return true;
    }
}
