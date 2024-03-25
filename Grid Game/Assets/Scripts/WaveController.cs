using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveController : MonoBehaviour
{

    [SerializeField] private bool isRunning;
    [SerializeField] public double tickLength = 0.5;
    [SerializeField] public int waveCount = 0;
    [SerializeField] int lastBalance;
    public List<GameObject> enemyTypes;
    public List<GameObject> enemies = new List<GameObject>();
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
        isRunning = false;
    }

    double time = 0;
    double ticks = 0;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(ticks < time / tickLength)
        {
            ticks++;
            if(isRunning)
            {
                Tick();
            }
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

    public static void StartWaves()
    {
        instance.isRunning = true;
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
        if (enemies.Count == 0 && isRunning)
        {
            Wave();
        }
    }

    
    public void Wave()
    {
        SetTickLength(waveCount);
        int balance = EnemyCountForWave(waveCount);
        float radius = Mathf.Sqrt(balance / Mathf.PI);
        float dist = waveCount/2 + 10 + 2*radius;
        //Debug.Log(waveCount+": "+dist);
        Vector2 pos = Random.insideUnitCircle.normalized * dist;
        while(balance > 0)
        {
            Vector2 spawnPos = Random.insideUnitCircle * radius + pos;
            //Debug.Log(spawnPos/*waveCount+" | ["+dist+"] ("+radius+") "+(Vector2.Distance(spawnPos, Vector2.zero))*/);
            balance = CreateEnemy((int)Mathf.Round(spawnPos.x), (int)Mathf.Round(spawnPos.y), balance);
        }
        waveCount++;
        waveDisplay.UpdateWaveText(waveCount);
    }

    private void SetTickLength(int wave)
    {
        tickLength = 0.25 * (Mathf.Pow(0.95f, wave));
    }

    public static int EnemyCountForWave(int wave)
    {
        instance.lastBalance = (int)Mathf.Round(25.0f * (Mathf.Pow(wave / 10.0f, 2)) + 5.0f);
        return instance.lastBalance;
        //Paste this into desmos to modify:
        //y=25\left(\frac{x}{10}\right)^{2}+5
    }


    //returns the remaining balance
    public int CreateEnemy(int x, int y, int balance)
    {
        
        List<float> enemyWeights = new List<float>();
        float totalWeight = 0.0f;
        foreach (GameObject enemyType in enemyTypes)
        {
            float weight = 1.0f / enemyType.GetComponent<EnemyController>().money;
            if (balance >= enemyType.GetComponent<EnemyController>().money)
            {
                totalWeight += weight;
                enemyWeights.Add(totalWeight);
            }
                
        }
        float rand = Random.Range(0, totalWeight);
        for(int i=0;i< enemyWeights.Count;i++)
        {
            if(rand <= enemyWeights[i])
            {
                CreateEnemyType(x, y, i);
                return balance - enemyTypes[i].GetComponent<EnemyController>().money;
            }
        }
        Debug.Log("Total Weight: " + totalWeight + ", Rand: " + rand);
        Debug.Log("Does the enemy types list contain the enemies?");
        isRunning = false;
        return 0;
    }

    public void CreateEnemyType(int x, int y, int enemyType)
    {
        GameObject newEnemy = Instantiate(enemyTypes[enemyType], new Vector3(x, y, 0), Quaternion.identity);
        EnemyController eC = newEnemy.GetComponent<EnemyController>();
        eC.Init(x, y);
        enemies.Add(newEnemy);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("EndScreen", LoadSceneMode.Single);
    }
}
