using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerOldPosition 
{
    public float posX;
    public float posY;
    public PlayerOldPosition(PlayerInventar pl)
    {
        posX = pl.pos.position.x;
        posY = pl.pos.position.y;
    }
}
