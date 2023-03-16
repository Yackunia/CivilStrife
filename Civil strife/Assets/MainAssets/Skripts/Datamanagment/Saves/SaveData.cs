using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{
    #region PlayerData
    public static void SavePlayerData(PlayerInventar player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/lol.log";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerStats data = new PlayerStats(player);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Save");
    }

    public static PlayerStats LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/lol.log";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerStats data = formatter.Deserialize(stream) as PlayerStats;
            stream.Close();
            Debug.Log("LoadComplete");

            return data;
        }
        else
        {
            Debug.LogError("NotFind" + path);
            return null;
        }

    }


    public static PlayerStats DestroyPlayerData()
    {
        string path = Application.persistentDataPath + "/lol.log";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("DestroyComplete");

            return null;
        }
        else
        {
            Debug.LogError("NotFind" + path);
            return null;
        }
    }
    #endregion

    #region EnemyData
    public static void SaveEnemyData(EnemySaving enemy, int id)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/enemyScene" + id + ".log";
        FileStream stream = new FileStream(path, FileMode.Create);

        EnemyData data = new EnemyData(enemy);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Save "+id +" scene");
    }

    public static EnemyData LoadEnemyData(int id)
    {
        string path = Application.persistentDataPath + "/enemyScene" + id + ".log";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            EnemyData data = formatter.Deserialize(stream) as EnemyData;
            stream.Close();
            Debug.Log("Load of " + id + " scene Complete");

            return data;
        }
        else
        {
            Debug.LogError("NotFind" + path);
            return null;
        }

    }


    public static EnemySaving DestroyEnemyData(int id)
    {
        string path = Application.persistentDataPath + "/enemyScene" + id + ".log";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Destroy of " + id + " scene Complete");

            return null;
        }
        else
        {
            Debug.LogError("NotFind" + path);
            return null;
        }
    }
    #endregion

   
}