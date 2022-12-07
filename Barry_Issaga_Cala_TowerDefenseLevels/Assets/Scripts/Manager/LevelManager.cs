using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [Header("General settings")]
    [SerializeField] private int lifes = 10;

    private int totalLifes;

    void Start()
    {
        totalLifes = lifes;
    }


    private void RemoveLives(EnemyController enemy)
    {
        totalLifes--;

        if (totalLifes <= 0)
        {
            totalLifes = 0;
            //game over menu
        }
    }

    private void OnEnable()
    {
        EnemyController.onPathFinished += RemoveLives;
    }

    private void OnDisable()
    {
        EnemyController.onPathFinished -= RemoveLives;
    }

    
}
