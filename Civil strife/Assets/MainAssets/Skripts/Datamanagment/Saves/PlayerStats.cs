using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public bool isAlive;

    public float health;
    public float posX;
    public float posY;

    public int weaponID;
    public int campID;
    
    public PlayerStats(PlayerInventar pl)
    {
        isAlive = pl.isAlive;
        health = pl.healthPoint;
        posX = pl.pos.position.x;
        posY = pl.pos.position.y;

        weaponID = pl.idOfWeapon;
        campID = pl.idOfCamp;
    }   

}
