using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spowner : MonoBehaviour
{

    public Wave[] waves;
    public Enemy enemy;
    float nextspontiem;

    Wave currentwave;
    int currentwavenumber;
    int enemyreminigalive;
    int enemisRemainingTospawn;
    float nextSpawnTime;
    void Start()
    {
        nextwave();
        Enemy spownedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity) as Enemy;

    }
    void Update()
    {
        if ( enemisRemainingTospawn > 0 && Time.time > nextSpawnTime)
        {
           
            enemisRemainingTospawn--;
            nextSpawnTime = Time.time + currentwave.timeBetweenSpowns;

            Enemy spownedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity) as Enemy;
            spownedEnemy.OnDeath += OnEnemyDeath;
        }
    }

    void OnEnemyDeath()
    {
        enemyreminigalive--;
        if (enemyreminigalive == 0 )
        {
            nextwave();
        }
    }

    void nextwave()
    { 
        currentwavenumber++;
        Debug.Log("wave:" + currentwavenumber);
        if (currentwavenumber - 1 < waves.Length)
        {
            currentwave = waves[currentwavenumber - 1];


            enemisRemainingTospawn = currentwave.enemyCount;
            enemyreminigalive = enemisRemainingTospawn;
        }
    }
    [System.Serializable]   
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpowns;
    }



}







