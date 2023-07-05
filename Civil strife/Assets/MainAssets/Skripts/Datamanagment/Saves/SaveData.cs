using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{
    #region PlayerData
    public static void SavePlayerData(PlayerInventory player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/playerData";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Save");
    }

    public static PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/playerData";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
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
    public static PlayerData DestroyPlayerData()
    {
        string path = Application.persistentDataPath + "/playerData";
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

    #region Scene's Data
    public static void SaveSceneData(SceneData sceneData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/scene" + sceneData.idOfScene;
        FileStream stream = new FileStream(path, FileMode.Create);

        SceneDataContainer data = new SceneDataContainer(sceneData);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Save " + sceneData.idOfScene + " scene's data");
    }

    public static SceneDataContainer LoadScenesData(int id)
    {
        string path = Application.persistentDataPath + "/scene" + id;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SceneDataContainer data = formatter.Deserialize(stream) as SceneDataContainer;
            stream.Close();
            Debug.Log("Load " + id + " scene's data Complete");

            return data;
        }
        else
        {
            Debug.LogError("NotFind" + path);
            return null;
        }

    }
    public static SceneData DestroySceneData(int id)
    {
        string path = Application.persistentDataPath + "/scene" + id;

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Destroy " + id + " scene Complete");

            return null;
        }
        else
        {
            Debug.LogError("NotFind" + path);
            return null;
        }
    }

    public static bool CheckSceneData(int id)
    {
        string path = Application.persistentDataPath + "/SceneSaves/scene" + id;

        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region SettingsData
    public static void SaveSettingsData(Settings settingsData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        

        string path = Application.persistentDataPath + "/settings";
        FileStream stream = new FileStream(path, FileMode.Create);

        SettingsData data = new SettingsData(settingsData);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Save settings data");
    }

    public static SettingsData LoadSettingsData()
    {
        string path = Application.persistentDataPath + "/settings";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SettingsData data = formatter.Deserialize(stream) as SettingsData;
            stream.Close();
            Debug.Log("Load settings data");

            return data;
        }
        else
        {
            Debug.LogError("NotFind" + path);
            return null;
        }

    }
    public static SettingsData DestroySettingsData()
    {
        string path = Application.persistentDataPath + "/settings";

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Destroy settings data Complete");

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