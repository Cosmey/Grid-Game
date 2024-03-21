using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveController : MonoBehaviour
{

    [SerializeField] double tickLength = 0.5;
    [SerializeField] private int waveCount = 0;
    public List<GameObject> enemies = new List<GameObject>();
    [SerializeField] GameObject enemy;
    [SerializeField] WaveDisplayScript waveDisplay;

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
        //Make it so you can't place stuff on top of home base
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                TowerManager.setBuilding(x, y, gameObject);
            }
        }
    }

    double time = 0;
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

        for(int i=enemies.Count-1; i>=0; i--)
        {
            GameObject enemy = enemies[i];
            if (enemy != null)
            {
                EnemyController em = enemy.GetComponent<EnemyController>();
                if (em != null)
                {
                    em.Lerp(percentThroughTick);
                }
                else
                {
                    enemies.Remove(enemy);
                }
            }
            else
            {
                enemies.Remove(enemy);
            }
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    private void OnDestroy()
    {
        GameOver();
    }

    private void Tick()
    {
        if(enemies.Count == 0)
        {
            //Wave();
        }
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

    
    private void Wave()
    {
        SetTickLength(waveCount);
        int enemyCount = EnemyCountForWave(waveCount);
        float radius = Mathf.Sqrt(enemyCount / Mathf.PI);
        float dist = waveCount/2 + 10 + radius;
        Vector2 pos = Random.insideUnitCircle.normalized * dist;
        for(int i=0;i<enemyCount; i++)
        {
            Vector2 spawnPos = Random.insideUnitCircle * radius + pos;
            CreateEnemy((int) spawnPos.x, (int) spawnPos.y);
        }
        waveCount++;
        waveDisplay.UpdateWaveText(waveCount);
    }

    private void SetTickLength(int wave)
    {
        tickLength = 0.5 * (Mathf.Pow(0.99f, wave));
    }

    private int EnemyCountForWave(int wave)
    {
        return (int)Mathf.Round(25.0f * (Mathf.Pow(wave/10.0f, 2)) + 5.0f);
        //Paste this into desmos to modify:
        //y=25\left(\frac{x}{10}\right)^{2}+5
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
