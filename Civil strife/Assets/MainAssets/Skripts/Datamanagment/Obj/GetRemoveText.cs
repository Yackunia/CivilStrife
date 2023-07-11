using UnityEngine.UI;
using UnityEngine;

public class GetRemoveText : MonoBehaviour
{
    [SerializeField] private Text[] t;

    private void Start()
    {
        t[1].text = t[0].text;
        Invoke("CheckToDestr", 1f);
    }
    private void CheckToDestr()
    {
        Destroy(gameObject);
    }
}
