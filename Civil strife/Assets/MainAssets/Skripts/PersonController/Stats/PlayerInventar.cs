using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerInventar : MonoBehaviour
{
    [SerializeField] private Transform[] campsCoordinate;
    public Transform pos;

    public bool isAlive;

    public float healthPoint;

    public int idOfWeapon;
    public int idOfCamp;

    public PlayerHealth health;

    [SerializeField] private EnemySaving[] enemys;

    private void Start()
    {
       Load();
    }
    private void Update()
    {
        idOfCamp = health.campID;
        healthPoint = health.healthPoint;
    }
    public void Save()
    {
        SaveData.SavePlayerData(this);

        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SaveEnemy();
        }
    }

    public void Load()
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].Load();
        }

        PlayerStats pl = SaveData.LoadPlayerData();

        health.healthPoint = pl.health;

        idOfWeapon = pl.weaponID;
        health.campID = pl.campID;
        isAlive = pl.isAlive;
        pos.position = new Vector2(pl.posX, pl.posY);

        if (!isAlive)
        {
            isAlive = true;
        }
    }

    public void Respawn()
    {

        health.healthPoint = health.maxHP;
        healthPoint = health.healthPoint;
        pos.position = campsCoordinate[idOfCamp].position;

        Save();
        SceneManager.LoadScene(idOfCamp);
        Load();
        Time.timeScale = 1F;
    }

    public void NewGame()
    {
        Cursor.visible = false;
        SaveData.DestroyPlayerData();
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
}
