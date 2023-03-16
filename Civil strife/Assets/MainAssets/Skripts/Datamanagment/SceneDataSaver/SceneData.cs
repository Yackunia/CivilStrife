using System;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public bool isRespawn;

    public bool[] withRespawn;
    public bool[] withoutRespawn;

    public int idOfScene;

    [SerializeField] private int countOfScenes;

    [SerializeField] private GameObject[] objectsWithRespawn;
    [SerializeField] private GameObject[] objectsWithoutRespawn;

    private void Start()
    {
        SetGameObjects();
    }

    private void SetGameObjects()
    {
        SetAllidOdObjonScene();

        Load(idOfScene);
        for (int i = 0; i < withRespawn.Length; i++)
        {
            if (!withRespawn[i] && !isRespawn) Destroy(objectsWithRespawn[i]);
            else if (!withRespawn[i]) withRespawn[i] = true;
        }
        for (int i = 0; i < withoutRespawn.Length; i++)
        {
            if (!withoutRespawn[i]) Destroy(objectsWithoutRespawn[i]);
        }

        isRespawn = false;
    }

    private void SetAllidOdObjonScene()
    {
        for (int i = 0; i < objectsWithRespawn.Length; i++)
        {
            if (objectsWithRespawn[i].transform.tag == "EnemyV") objectsWithRespawn[i].GetComponent<EnemyV>().SetIdOfSceneObj(i, true);
            else if (objectsWithRespawn[i].transform.tag == "EnemyS") objectsWithRespawn[i].GetComponent<EnemyS>().SetIdOfSceneObj(i, true);
            else if (objectsWithRespawn[i].transform.tag == "DestrObj") objectsWithRespawn[i].GetComponent<DestrObj>().SetIdOfSceneObj(i, true);
        }

        for (int i = 0; i < objectsWithoutRespawn.Length; i++)
        {
            if (objectsWithoutRespawn[i].transform.tag == "EnemyV") objectsWithoutRespawn[i].GetComponent<EnemyV>().SetIdOfSceneObj(i, false);
            else if (objectsWithoutRespawn[i].transform.tag == "EnemyS") objectsWithoutRespawn[i].GetComponent<EnemyS>().SetIdOfSceneObj(i, false);
            else if (objectsWithoutRespawn[i].transform.tag == "DestrObj" || objectsWithoutRespawn[i].transform.tag == "Mish") objectsWithoutRespawn[i].GetComponent<DestrObj>().SetIdOfSceneObj(i, false);

        }
    }
    private void DestroySave(int id)
    {
        SaveData.DestroySceneData(id);
    }

    private void Load(int id)
    {
        SceneDataContainer data = SaveData.LoadScenesData(id);
        isRespawn = data.isRespawn;

        withRespawn = data.withRespawn;
        withoutRespawn = data.withoutRespawn;

        idOfScene = data.idOfScene;
    }

    public void Save()
    {
        SaveData.SaveSceneData(this);
    }

    public void Respawn()
    {
        isRespawn = true;
        Save();

        for (int i = 0; i < countOfScenes; i++)
        {
            if (SaveData.CheckSceneData(i))
            {
                Load(i);
                isRespawn = true;
                Save();
            }
        }
    }

    public void NewGame()
    {
        for (int i = 0; i < countOfScenes; i++)
        {
            if (SaveData.CheckSceneData(i)) DestroySave(i);
        }
    }
    public void SetObjDisabled(bool isRespawn, int id)
    {
        if (isRespawn) withRespawn[id] = false;
        else withoutRespawn[id] = false;
    }
}
