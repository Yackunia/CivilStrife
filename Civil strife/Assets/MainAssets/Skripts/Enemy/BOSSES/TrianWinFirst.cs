
using UnityEngine;


public class TrianWinFirst : MonoBehaviour
{
    [SerializeField] private PlayerInventar invent;
    public void CloseLearning()
    {
        invent.idOfCamp = 6;
        invent.Respawn();
    }
}
