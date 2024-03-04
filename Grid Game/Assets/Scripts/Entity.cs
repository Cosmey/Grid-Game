using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] Vector2Int pos;
    [SerializeField] public double health;
    [SerializeField] public float damage;
    [SerializeField] private int radius;

    void Start()
    {
        radius = (int)Mathf.Ceil(transform.localScale.x / 2f);
        pos = new Vector2Int((int) transform.position.x, (int) transform.position.y);
    }

    //This function allows things to get 
    private int GetEdgeOffset()
    {
        return radius - 1;
    }

    public Vector2Int GetTargetFromPoint(Vector2Int point)
    {
        Vector2Int upperLeft = new Vector2Int(pos.x - GetEdgeOffset(), pos.y - GetEdgeOffset());
        int width = (GetEdgeOffset()*2)+1;
        float lowestDist = float.MaxValue;
        Vector2Int closestPoint = pos;
        for (int x = 0; x < width; x++) 
        {
            for (int y = 0; y < width; y++)
            {
                if (x == 0 || y == 0 || x == width - 1 || y == width - 1)
                {
                    Vector2Int cur = new Vector2Int(upperLeft.x + x, upperLeft.y + y);
                    float dist = Vector2.Distance(cur, point);
                    if (dist < lowestDist)
                    {
                        lowestDist = dist;
                        closestPoint = cur;
                    }
                }
            }
        }

        return closestPoint;
    }

    //This should be called from entity that is dealing the damage, parameter is taking damage
    public void DealDamageTo(Entity entity)
    {
        entity.TakeDamage(this);
        Destroy(gameObject);
    }

    //This should be called from the entity that is taking damage, parameter is dealing damage
    public void TakeDamage(Entity entity)
    {
        health -= entity.damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
