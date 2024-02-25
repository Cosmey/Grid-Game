using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tick()
    {
        Vector2Int goalPos = WaveController.basePosition;
        if(goalPos.x == (int) transform.position.x && goalPos.y == (int) transform.position.y)
        {
            return;
        }
        int distX = (int) transform.position.x - goalPos.x;
        int distY = (int) transform.position.y - goalPos.y;
        
        int degrees;
        if (Mathf.Abs(distX) > Mathf.Abs(distY))
        {
            int change = distX == 0 ? 0 : (-distX / Mathf.Abs(distX));
            if(change >= 0)
            {
                degrees = 180;
            } else
            {
                degrees = 0;
            }
        }
        else
        {
            int change = distY == 0 ? 0 : (-distY / Mathf.Abs(distY));
            if(change >= 0)
            {
                degrees = 270;
            } else
            {
                degrees = 90;
            }
        }
        transform.rotation = Quaternion.Euler(0, 0, degrees);
        transform.position += -transform.right;
        bool isWall = TowerManager.isBuilding((int) transform.position.x, (int) transform.position.y);
        while(isWall)
        {
            transform.position += transform.right;
            degrees += 90;
            transform.rotation = Quaternion.Euler(0, 0, degrees);
            transform.position += -transform.right;
        }

    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }
}
