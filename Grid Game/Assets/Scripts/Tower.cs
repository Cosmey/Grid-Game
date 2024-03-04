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
        if(target != null)
        {
            GameObject e = Instantiate(bullet, spawnPoint.transform.position, Quaternion.identity);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit");
        if (other.tag == "Enemy")
        {
            targetList.Add(other.gameObject);
            target = targetList[0];
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            targetList.Remove(other.gameObject);
            if(targetList.Count <= 0)
            {
                target = null;
            }
        }
    }
}
