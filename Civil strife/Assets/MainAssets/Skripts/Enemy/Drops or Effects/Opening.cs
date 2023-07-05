using UnityEngine;

public class Opening : MonoBehaviour
{
    [SerializeField] private int randomValue;
    [SerializeField] private GameObject[] dropPrefs;
    [SerializeField] private GameObject[] toHide;

    public void DropPrize()
    {
        int x = Random.Range(0, randomValue);
        if (x < dropPrefs.Length)
        {
            Instantiate(dropPrefs[x], transform.position, Quaternion.identity, null);
            toHide[x].SetActive(false);
        }
    }
}
