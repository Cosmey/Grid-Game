using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private List<GameObject> targetList;
    private GameObject target;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] float bulletSpeed;
    float count = 0;
    bool attackable=true;
    float storedCount;
    [SerializeField] float attackCooldown;
    [SerializeField] int maxHealth;
    [SerializeField] private float maxDistance;
    int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    private void Start()
    {
        targetList = new List<GameObject>();
    }


    // Update is called once per frame
    void Update()
    {
        CheckClosestEnemy();
        UpdateThings();
    }
    private void UpdateThings()
    {
        if (target != null && attackable)
        {
            attackable = false;
            attack();
            storedCount = count;
        }
        count += Time.deltaTime;

        if (count >= storedCount + attackCooldown)
        {
            attackable = true;
        }

        if (currentHealth <= 0)
        {
            death();
        }
    }
    private void CheckClosestEnemy()
    {
        CheckTargetInRadius();
        List<GameObject> enemyList = WaveController.instance.enemies;
        float closestDistance = maxDistance;
        float distance;
        for(int i = 0;i < enemyList.Count; i++)
        {
            distance = Vector2.Distance(enemyList[i].transform.position, transform.position);
            if(distance < closestDistance)
            {
                closestDistance = distance;
                target = enemyList[i];
            }
        }
    }
    private void CheckTargetInRadius()
    {
        if(target != null)
        {
            float distance = Vector2.Distance(target.transform.position, transform.position);
            if (distance > maxDistance)
            {
                target = null;
            }
        }
        
    }
    
    void attack()
    {
        if(target != null)
        {
            GameObject e = Instantiate(bullet);
            e.transform.position = transform.position;
            e.GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * bulletSpeed;
            Destroy(e, 2);
            bulletMove(e);
        }
        
        
        
    }

    void bulletMove(GameObject bullet)
    {
        //bullet.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, bulletSpeed);
    }

    void takeDamage(int damage)
    {
        currentHealth -= damage;
    }

    void death()
    {
        Destroy(this);
    }

    
}
