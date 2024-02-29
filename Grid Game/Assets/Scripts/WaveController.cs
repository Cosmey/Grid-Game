using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveController : MonoBehaviour
{

    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    [SerializeField] GameObject enemy;

    public static WaveController instance;

    public static Entity baseEntity;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        baseEntity = GetComponent<Entity>();
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
        float percentThroughTick = (float) ((time - (ticks * tickLength)) / tickLength);
        percentThroughTick = 1.0f - Mathf.Abs(percentThroughTick);
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                EnemyController em = enemy.GetComponent<EnemyController>();
                if (em != null)
                {
                    em.Lerp(percentThroughTick);
                } else
                {
                    enemies.Remove(enemy);
                }
            } else
            {
                enemies.Remove(enemy);
            }
        }


        if(baseEntity.health <= 0)
        {
            GameOver();
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
        EnemyController eC = newEnemy.GetComponent<EnemyController>();
        eC.Init();
        enemies.Add(newEnemy);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("EndScreen", LoadSceneMode.Single);
    }
}
