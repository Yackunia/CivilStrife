using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneStopper : MonoBehaviour
{
    [SerializeField] private GameObject StopMenu;

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        InputCheck();
    }
    private void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPauseMenu();
        }
    }

    public void SetPauseMenu()
    {
        StopMenu.SetActive(!StopMenu.activeSelf);
<<<<<<< HEAD
        Cursor.visible = StopMenu.activeSelf;

=======
>>>>>>> 8c535e139b9908e7d4b885de4a0367bbd91b7f85
        if (Time.timeScale == 1) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}
