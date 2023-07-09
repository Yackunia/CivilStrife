using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMusic : MonoBehaviour
{
    public int countOfAttackingEnemies;

    private bool isFight;
    private bool isAlive;

    [SerializeField] private float valueOfBattleMusicVolume;
    [SerializeField] private float valueOfMidleMusicVolume;
    [SerializeField] private float maxOfMid;
    [SerializeField] private float maxOfBat;

    [SerializeField] private AudioListener plListener;

    [SerializeField] private AudioSource battleMusic;
    [SerializeField] private AudioSource midleMusic;
    private void Start()
    {
        valueOfMidleMusicVolume = midleMusic.volume;
        valueOfBattleMusicVolume = midleMusic.volume;
    }
    private void Update()
    {
        CheckMusicStatus();
    }
    private void CheckMusicStatus()
    {
        CheckFight();
        CheckMiddle();
    }

    private void CheckMiddle()
    {
        if (countOfAttackingEnemies == 0 && isFight)
        {
            isFight = false;
        }
        if (!isFight && valueOfBattleMusicVolume > 0)
        {
            valueOfBattleMusicVolume -= Time.deltaTime * 0.6f;
            battleMusic.volume = valueOfBattleMusicVolume;
        }
        else if (!isFight && valueOfBattleMusicVolume <= 0 && valueOfMidleMusicVolume < maxOfMid)
        {
            valueOfMidleMusicVolume += Time.deltaTime * 0.6f;
            midleMusic.volume = valueOfMidleMusicVolume;
        }
    }
    private void CheckFight()
    {
        if (countOfAttackingEnemies > 0 && !isFight)
        {
            isFight = true;
        }
        if (isFight && valueOfMidleMusicVolume > 0)
        {
            valueOfMidleMusicVolume -= Time.deltaTime;
            midleMusic.volume = valueOfMidleMusicVolume;
        }
        else if (isFight && valueOfMidleMusicVolume <= 0 && valueOfBattleMusicVolume < maxOfBat)
        {
            valueOfBattleMusicVolume += Time.deltaTime * 0.6f;
            battleMusic.volume = valueOfBattleMusicVolume;
        } 
    }

}
