//coded by someone

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private GameObject target;
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletScale = 1.0f;
    [SerializeField] int bulletDamage;
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
        if(enemyList == null) return;
        float closestDistance = maxDistance;
        float distance;
        for(int i = 0;i < enemyList.Count; i++)
        {
            if(enemyList[i] == null) 
            {
                continue;

            }
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
            e.transform.localScale = e.transform.localScale * bulletScale;
            e.GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * bulletSpeed;
            e.GetComponent<Entity>().SetDamage(bulletDamage);
            Destroy(e, 5);
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
