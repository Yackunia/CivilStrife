using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySaving : MonoBehaviour
{
    public float timer;
    [SerializeField] private int idOfScene;

    public bool[] isAliveEnemy;
    public bool isActive;

    public Enemy[] enemyObj;

    private void Start()
    {
        Load();
    }
    private void Update()
    {
        CheckEnemys();
    }

    private void CheckEnemys()
    {
        timer += Time.deltaTime;
        if (isActive && timer > 3)
        {
            timer = 0;
            for (int i = 0; i < enemyObj.Length; i++)
            {
                isAliveEnemy[i] = enemyObj[i].isAlive;
            }
        }
    }

    public void SaveEnemy()
    {
        SaveData.SaveEnemyData(this, idOfScene);
        Load();
    }

    public void Reborn()
    {
        for (int i = 0; i <= enemyObj.Length; i++)
        {
            isAliveEnemy[i] = true;
        }
        SaveEnemy();
    }

    public void Load()
    {

            EnemyData enemy = SaveData.LoadEnemyData(idOfScene);

            isAliveEnemy = enemy.isAliveEnemy;

            for (int i = 0; i < enemyObj.Length; i++)
            {
                if (!isAliveEnemy[i] && isActive) Destroy(enemyObj[i].GetComponent<GameObject>());
            }

    }

    public EnemySaving(EnemySaving enemy)
    {
        isAliveEnemy = enemy.isAliveEnemy;
    }
}
