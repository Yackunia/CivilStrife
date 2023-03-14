
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoader : MonoBehaviour
{
    [SerializeField] private Transform[] nextLvl;
    [SerializeField] private Transform[] lvlSpawnPoint;
    [SerializeField] private Transform plTransform;

    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private PlayerInventar invent;

    private bool menu;

    [SerializeField] private int[] scenes;

    [SerializeField] private float[] distance;
    private void LoadScene(int id)
    {
        plTransform.position = lvlSpawnPoint[id].position;
        
        invent.Save();
        
        SceneManager.LoadScene(scenes[id]);
        Time.timeScale = 1f;  
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu = !menu;
            StopMenu(menu);
        }

        for (int i = 0; i < lvlSpawnPoint.Length; i++)
        {
            distance[i] = Vector2.Distance(transform.position, nextLvl[i].position);
            if (distance[i] < 2)
            {
                LoadScene(i);
            }
        }
        
    }

    private void StopMenu(bool v)
    {
        if (!v) Time.timeScale = 1f;
        else Time.timeScale = 0f;

        pauseMenu.SetActive(v);
        Cursor.visible = v;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadScene1(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
