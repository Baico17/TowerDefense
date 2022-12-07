using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float movementSpeed;

    float moveSpeed;

    [Header("Movement settings")]
    private int currentPathPoint = 0;
    private WayPoint wayPoint;

    public static Action<EnemyController>onPathFinished;

    private void Awake()
    {
        wayPoint = GameObject.FindGameObjectWithTag("WayPoint").GetComponent<WayPoint>();
        moveSpeed = movementSpeed;
        Flip();
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoint.GetPointPosition(currentPathPoint).position, moveSpeed * Time.deltaTime);

    }
    

    // Update is called once per frame
    private void Update()
    {
        if(Vector2.Distance(transform.position, wayPoint.GetPointPosition(currentPathPoint).position) < 0.1f)
        {
            currentPathPoint++;

            if (currentPathPoint >= wayPoint.waypointList.Count)
            {
                ResetCurrentPathPoint();

                onPathFinished?.Invoke(this); // if(onPathFinished != null)

                gameObject.SetActive(false);
                return;
            }

            Flip();

        }

        Move();
    }

    public void ResetCurrentPathPoint()
    {
        currentPathPoint = 0;
    }

    public void Flip()
    {
        if (wayPoint.GetPointPosition(currentPathPoint).position.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void StopMovement()
    {
        moveSpeed = 0;
    }

    public void ResumeMovement()
    {
        if(!GetComponent<HealthManager>().GetEnemyisDead())
        moveSpeed = movementSpeed;
    }
}
