using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    private GameObject target;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] float bulletSpeed;
    float count = 0;
    bool attackable=true;
    float storedCount;
    [SerializeField] float attackCooldown;
    [SerializeField] int maxHealth;
    int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            target = collision.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && attackable)
        {
            attackable = false;
            attack();
            storedCount = count;
        }
        count+=Time.deltaTime;
        
        if(count >= storedCount + attackCooldown)
        {
            attackable=true;
        }

        if (currentHealth <= 0)
        {
            death();
        }
    }
    
    void attack()
    {
        
        GameObject e =  Instantiate(bullet, spawnPoint.transform.position, Quaternion.identity);
        e.GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * bulletSpeed;
        Destroy(e, 2);
        bulletMove(e);
        
        
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
