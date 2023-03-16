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
    [SerializeField] private Inventory2 inv;

    [SerializeField] private Znacharka ZN;
    [SerializeField] private Girl GL;


    private void Start()
    {
       Load();
    }
    private void Update()
    {
        idOfCamp = health.campID;
        healthPoint = health.healthPoint;
    }
    public void NewGameStart()
    {
        SaveData.DestroyQuestsInfo();
        SaveData.DestroyInventory();

    }
    public void Save()
    {
        SaveData.SavePlayerData(this);
        inv.SaveInvent();

        List<bool> fl = new List<bool>();

        for(int i = 0; i < ZN.flag.Length; i++)
        {
            fl.Add(ZN.flag[i]);
        }
        bool temp = ZN.isQuest;

        List<int> DV = new List<int>();
        for(int i = 0; i < ZN.dialogs.Length; i++)
        {
            DV.Add(ZN.dialogs[i].dialogValue);
        }

        bool kakashka = ZN.isStart;

        QuestsInfo QI = new QuestsInfo(fl, temp, DV, kakashka);

        QuestsSave.SavePlayerQuests(QI);

        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SaveEnemy();
        }


        List<bool> fl1 = new List<bool>();

        for (int i = 0; i < GL.flag.Length; i++)
        {
            fl1.Add(GL.flag[i]);
        }
        bool temp2 = GL.isQuest;

        List<int> DV1 = new List<int>();
        for (int i = 0; i < GL.dialogs.Length; i++)
        {
            DV1.Add(GL.dialogs[i].dialogValue);
        }

        bool kakashka2 = GL.isStart;

        QuestsInfo2 QI2 = new QuestsInfo2(fl1, temp2, DV1, kakashka2);

        QuestsSave.SavePlayerQuests2(QI2);


    }

    public void Load()
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].Load();
        }
        inv.RemoveItemAllByID(1);
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
        inv.RemoveItemAllByID(1);
        SaveData.SavePlayerData(this);

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
