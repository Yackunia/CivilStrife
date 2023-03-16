using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{
    #region Folder Create

    public static void CreateSaveFolders()
    {
        CreateDataFolder("/SceneData");
        CreateDataFolder("/PlayerData");
        CreateDataFolder("/SettingsData");
    }

    private static void CreateDataFolder(string name)
    {
        string folderPath = Application.persistentDataPath + name;
        Directory.CreateDirectory(folderPath);
        Debug.Log(name + " Folder was creating");
    }

    #endregion



    #region PlayerData
    public static void SavePlayerData(PlayerInventory player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

<<<<<<< HEAD
        string path = Application.persistentDataPath + "/PlayerData/playerData";
=======
        string path = Application.persistentDataPath + "/playerData";
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Save");
    }

    public static PlayerData LoadPlayerData()
    {
<<<<<<< HEAD
        string path = Application.persistentDataPath + "/PlayerData/playerData";
=======
        string path = Application.persistentDataPath + "/playerData";
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85
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
<<<<<<< HEAD
        string path = Application.persistentDataPath + "/PlayerData/playerData";
=======
        string path = Application.persistentDataPath + "/playerData";
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85
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

<<<<<<< HEAD
        string path = Application.persistentDataPath + "/SceneData/scene" + sceneData.idOfScene;
=======
        string path = Application.persistentDataPath + "/scene" + sceneData.idOfScene;
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85
        FileStream stream = new FileStream(path, FileMode.Create);

        SceneDataContainer data = new SceneDataContainer(sceneData);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Save " + sceneData.idOfScene + " scene's data");
    }

    public static SceneDataContainer LoadScenesData(int id)
    {
<<<<<<< HEAD
        string path = Application.persistentDataPath + "/SceneData/scene" + id;
=======
        string path = Application.persistentDataPath + "/scene" + id;
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85

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
<<<<<<< HEAD
        string path = Application.persistentDataPath + "/SceneData/scene" + id;
=======
        string path = Application.persistentDataPath + "/scene" + id;
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85

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
<<<<<<< HEAD
        string path = Application.persistentDataPath + "/SceneData/scene" + id;
=======
        string path = Application.persistentDataPath + "/SceneSaves/scene" + id;
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85

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

        

<<<<<<< HEAD
        string path = Application.persistentDataPath + "/SettingsData/settings";
=======
        string path = Application.persistentDataPath + "/settings";
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85
        FileStream stream = new FileStream(path, FileMode.Create);

        SettingsData data = new SettingsData(settingsData);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Save settings data");
    }

    public static SettingsData LoadSettingsData()
    {
<<<<<<< HEAD
        string path = Application.persistentDataPath + "/SettingsData/settings";
=======
        string path = Application.persistentDataPath + "/settings";
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85

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
<<<<<<< HEAD
        string path = Application.persistentDataPath + "/SettingsData/settings";
=======
        string path = Application.persistentDataPath + "/settings";
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85

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