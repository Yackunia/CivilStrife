using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPoint : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private Loader sceneLoader;
    [SerializeField] private SceneData sceneData;

    [SerializeField] private Transform pl_Transform;

    [SerializeField] private int idOfScene;
    [SerializeField] private int idOfPoint;

    [SerializeField] private bool needToSaveSceneData = true;
    private void Start()
    {
        pl_Transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        inventory = GameObject.Find("pl_INVENTOTY").GetComponent<PlayerInventory>();
        sceneLoader = GameObject.Find("pl_SAVELOADER").GetComponent<Loader>();
        if (needToSaveSceneData) sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
    }
    private void Update()
    {
        CheckDistToPlayer();
    }

    private void CheckDistToPlayer()
    {
        if (Vector2.Distance(transform.position, pl_Transform.position) <= 3f) SetScene();
    }

    public void SetScene()
    {
        inventory.isRespawn = false;
        for (int i = 0; i < inventory.isScene.Length;i++)
        {
            inventory.isScene[i] = false;
        }
        inventory.isScene[idOfScene] = true;
        inventory.sceneId = idOfPoint;
        inventory.UpdHealth();

        if (needToSaveSceneData) sceneData.Save();
        inventory.SaveAll();
        sceneLoader.LoadScene(idOfScene);
    }

    public void FirstLoad()
    {
        inventory.isRespawn = false;
        for (int i = 0; i < inventory.isScene.Length; i++)
        {
            inventory.isScene[i] = false;
        }
        inventory.isScene[idOfScene] = true;
        inventory.sceneId = idOfPoint;
        inventory.UpdHealth();

        inventory.SaveAll();
    }
}
