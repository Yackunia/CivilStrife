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
        Cursor.visible = StopMenu.activeSelf;

        if (Time.timeScale == 1) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}
