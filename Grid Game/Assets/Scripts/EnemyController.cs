using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(goalPos.x == transform.position.x && goalPos.y == transform.position.y)
        {
            return;
        }
        int distX = (int) transform.position.x - goalPos.x;
        int distY = (int) transform.position.y - goalPos.y;
        if(Mathf.Abs(distX) > Mathf.Abs(distY))
        {
            transform.position += new Vector3(-distX / Mathf.Abs(distX), 0, 0);
        } else
        {
            transform.position += new Vector3(0, -distY / Mathf.Abs(distY), 0);
        }

    }
}
