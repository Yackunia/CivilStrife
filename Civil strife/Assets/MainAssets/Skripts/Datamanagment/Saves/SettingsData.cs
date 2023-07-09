using System;
using UnityEngine;

[System.Serializable]

public class SettingsData
{
    public string jumpAction;
    public string dashAction;

    public string attackAction;
    public string sekondAttackAction;
    public string standAction;

    public string pauseAction;
    public string inventoryAction;
    public string bestiaryAction;
    public string taskAction;
    public string settingsAction;
    public string mapAction;
    public  SettingsData(Settings data)
    {
        jumpAction = data.jumpAction.ToString();
        dashAction = data.dashAction.ToString();

        attackAction = data.attackAction.ToString();
        sekondAttackAction = data.sekondAttackAction.ToString();
        standAction = data.standAction.ToString();

        pauseAction = data.pauseAction.ToString();
        inventoryAction = data.inventoryAction.ToString();
        taskAction = data.taskAction.ToString();
        settingsAction = data.settingsAction.ToString();
        mapAction = data.mapAction.ToString();
    }
}
