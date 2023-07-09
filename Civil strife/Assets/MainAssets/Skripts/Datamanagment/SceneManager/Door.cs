using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool canLoad;

    [SerializeField] GameObject helpObj;

    [SerializeField] private LoadingPoint loader;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            helpObj.SetActive(true);
            canLoad = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            helpObj.SetActive(false);
            canLoad = false;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canLoad)
        {
            loader.SetScene();
        }
    }
}
