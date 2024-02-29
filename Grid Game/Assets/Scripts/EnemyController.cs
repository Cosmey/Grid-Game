using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector2Int oldPos;
    [SerializeField] private Vector2Int newPos;
    [SerializeField] private Entity targetEntity;
    [SerializeField] private Vector2Int goalPos;
    void Start()
    {
        
    }

    public void Init()
    {
        oldPos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        newPos = oldPos;
        targetEntity = WaveController.baseEntity;
        goalPos = targetEntity.GetTargetFromPoint(oldPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tick()
    {
        transform.position.Set(newPos.x, newPos.y, transform.position.z);
        if(newPos == goalPos)
        {
            oldPos = goalPos;
            Entity entity = GetComponent<Entity>();
            if(entity != null)
            {
                entity.DealDamageTo(targetEntity);
            } else
            {
                Debug.Log("Enemy does not have entity component!");
            }
            
            return;
        }
        int distX = (int) transform.position.x - goalPos.x;
        int distY = (int) transform.position.y - goalPos.y;
        oldPos = newPos;
        if (Mathf.Abs(distX) > Mathf.Abs(distY))
        {
            int change = distX == 0 ? 0 : (-distX / Mathf.Abs(distX));
            if(change >= 0)
            {
                newPos.Set(oldPos.x + 1, oldPos.y);
                //degrees = 180;
            } else
            {
                newPos.Set(oldPos.x - 1, oldPos.y);
            }
        }
        else
        {
            int change = distY == 0 ? 0 : (-distY / Mathf.Abs(distY));
            if(change >= 0)
            {
                newPos.Set(oldPos.x, oldPos.y + 1);
            } else
            {
                newPos.Set(oldPos.x, oldPos.y - 1);
            }
        }

        

        //make sure enemy actually got to new position
        //transform.position.Set(newPos.x, newPos.y, transform.position.z);
        //set the old position to the old new position
        
        //Make the enemy face towards where it wants to go
        //transform.rotation = Quaternion.Euler(0, 0, degrees);
        //Move the enemey forward
        //transform.position += -transform.right;
        //Set the new goal position of the enemy 
        //newPos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        //Reset the visual position of the enemy back to old position so interpolation can begin
        //transform.position += transform.right;





        //check if that spot was a wall
        /*bool isWall = TowerManager.isBuilding((int) transform.position.x, (int) transform.position.y);
        int maxChecks = 4;
        int checks = 0;
        //rotate 90 degrees until no longer facing a wall or is surrounded by walls
        while(isWall && checks < maxChecks)
        {
            //reset back to old position so it can rotate and go forward again
            transform.position += transform.right;
            degrees += 90;
            transform.rotation = Quaternion.Euler(0, 0, degrees);
            transform.position += -transform.right;
            //check if that spot was a wall
            isWall = TowerManager.isBuilding((int) transform.position.x, (int)transform.position.y);
            checks++;
        }
        //Reset enemy back to old position and make it stop, put a warning in the console
        if(checks >= maxChecks)
        {
            transform.position += transform.right;
            Debug.Log("LOCKED ENEMY");
            return;
        }*/
        

        

    }

    public void Lerp(float percent)
    {
        //transition enemy between oldPos and newPos based on how far into the current tick we are
        transform.position = Vector2.Lerp(oldPos, newPos, percent);
    }
}
