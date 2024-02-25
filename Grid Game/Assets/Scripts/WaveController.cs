using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{

    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    [SerializeField] GameObject enemy;

    public static Vector2Int basePosition;

    // Start is called before the first frame update
    void Start()
    {
        CreateEnemy(-15, -7);
        basePosition.Set((int) transform.position.x, (int) transform.position.y);
    }

    double time = 0;
    [SerializeField] double tickLength = 0.5;
    double ticks = 0;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        while(ticks < time / tickLength)
        {
            ticks++;
            Tick();
        }
    }

    private void Tick()
    {
        foreach(GameObject enemy in enemies)
        {
            if(enemy != null)
            {
                EnemyController em = enemy.GetComponent<EnemyController>();
                if(em != null)
                {
                    em.Tick();
                }
            }
        }
    }

    public void CreateEnemy(int x, int y)
    {
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = new Vector2(x, y);
        enemies.Add(newEnemy);
    }
}
