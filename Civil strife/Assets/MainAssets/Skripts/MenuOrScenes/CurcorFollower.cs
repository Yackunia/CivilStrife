using UnityEngine;

public class CurcorFollower : MonoBehaviour
{
    public Vector2 mousePos;
    public Transform pos;
    private void Start()
    {
        Cursor.visible = true;
    }

    protected virtual void Update()
    {
        mousePos = Input.mousePosition;
        pos.position = new Vector2(mousePos.x / 1000, mousePos.y / 1000);
    }
}
