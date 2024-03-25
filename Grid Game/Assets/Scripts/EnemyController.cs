using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector2 oldPos;
    [SerializeField] private Vector2 newPos;
    [SerializeField] private float goalRatio;
    [SerializeField] private Vector2Int goalPos;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private Entity targetEntity;
    [SerializeField] public int money;
    [SerializeField] private float speedMult = 1.0f;
    void Start()
    {

    }

    public void Init(int x, int y)
    {
        oldPos = new Vector2(x, y);
        newPos = new Vector2(oldPos.x, oldPos.y);
        targetEntity = WaveController.baseEntity;
        goalPos = targetEntity.GetTargetFromPoint(oldPos);
        float distX = Mathf.Abs(oldPos.x - goalPos.x);
        float distY = Mathf.Abs(oldPos.y - goalPos.y);
        goalRatio = distX + distY == 0 ? 0.5f : distX / (distX+distY);
        lastTick = Time.time;
    }

    private void OnDestroy()
    {
        WaveController.instance.RemoveEnemy(gameObject);
    }

    private double lastTick;
    public void Tick()
    {
        if(Time.time - lastTick < WaveController.instance.tickLength - 0.01)
        {
            return;
        }
        lastTick = Time.time;
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        /*if (firstTick) {
            int balance = WaveController.EnemyCountForWave(WaveController.instance.waveCount);
            float radius = Mathf.Sqrt(balance / Mathf.PI);
            float dist = WaveController.instance.waveCount / 2 + 10 + 5 * radius;
            Debug.Log("Goal Distance: " + dist + " (+-" + radius + ")");
            Debug.Log("     Distance: " + Vector3.Distance(Vector3.zero, transform.position)); 
            firstTick = false;
        }*/

        /*if(newPos == goalPos)
        {
            oldPos = goalPos;
            Entity entity = GetComponent<Entity>();
            if(entity != null)
            {
                //entity.DealDamageTo(targetEntity);
            } 
            else
            {
                Debug.Log("Enemy does not have entity component!");
            }
            
            return;
        }*/
        float distX = (int) Mathf.Abs(transform.position.x - goalPos.x);
        float distY = (int) Mathf.Abs(transform.position.y - goalPos.y);
        float ratio = distX + distY == 0 ? goalRatio + 1.0f : distX / (distX + distY);
        oldPos = new Vector2(newPos.x, newPos.y);
        if (ratio >= goalRatio && distX != 0) //move along x
        {
            if(transform.position.x < goalPos.x)
            {
                //newPos.Set(oldPos.x + speedMult, oldPos.y);
                newPos = new Vector2(oldPos.x + speedMult, oldPos.y);
            } 
            else
            {
                //newPos.Set(oldPos.x - speedMult, oldPos.y);
                newPos = new Vector2(oldPos.x - speedMult, oldPos.y);
            }
        }
        else //move along y
        {
            if (transform.position.y < goalPos.y)
            {
                //newPos.Set(oldPos.x, oldPos.y + speedMult);
                newPos = new Vector2(oldPos.x, oldPos.y + speedMult);
            } 
            else
            {
                //newPos.Set(oldPos.x, oldPos.y - speedMult);
                newPos = new Vector2(oldPos.x, oldPos.y - speedMult);
            }
        }
    }

    public void Lerp(float percent)
    {
        //transition enemy between oldPos and newPos based on how far into the current tick we are
        Vector2 lerp = Vector2.Lerp(oldPos, newPos, percent);
        transform.position = new Vector3(lerp.x, lerp.y, transform.position.z);
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Building")
        {
            Entity entity = other.gameObject.GetComponent<Entity>();
            Entity enemyEntity = GetComponent<Entity>();
            DeathParticle();
            enemyEntity.DealDamageTo(entity);
        }
        else if(other.gameObject.tag == "Bullet")
        {
            Entity entity = other.gameObject.GetComponent<Entity>();
            Entity enemyEntity = GetComponent<Entity>();
            entity.DealDamageTo(enemyEntity);
            Destroy(other.gameObject);
        }
        
    }
    private void DeathParticle()
    {
        GameObject particle = Instantiate(deathParticle, GameObject.Find("Particles").transform);
        particle.transform.position = transform.position;
    }
}
