using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject target;
    [SerializeField] float speed;



    void playerFound(GameObject target)
    {
        target = target.gameObject;
    }
    void Update()
    {
        if(target != null)
        {
            var step = speed * Time.deltaTime;
            Vector3 targetPosition = target.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }
    }
}
