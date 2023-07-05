using System;
using UnityEngine;
using UnityEngine.UI;


public class Settings : MonoBehaviour
{
    [SerializeField] private Slovar slovar;
    [Header("Keys")]
    private bool isRebinding = false;

    private int IdOfKey;

    [SerializeField] private Text[] keyTexts;
    [SerializeField] private Button[] buttons;

    public KeyCode jumpAction;
    public KeyCode dashAction;

    public KeyCode attackAction;
    public KeyCode sekondAttackAction;
    public KeyCode standAction;

    public KeyCode pauseAction;
    public KeyCode inventoryAction;
    public KeyCode bestiaryAction;
    public KeyCode taskAction;
    public KeyCode settingsAction;
    public KeyCode mapAction;

    private void Start()
    {
        Load();
        OutputActions();
    }

    private void Update()
    {
        CheckKeyToRebind();
    }
    #region Key Rebinding
    public void StartRebinding(int id)
    {
        if (!isRebinding)
        {
            isRebinding = true;
            IdOfKey = id;

            keyTexts[id].text = "...";

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].enabled = false;
            }
        }
        slovar.enabled = false;
    }

    private void CheckKeyToRebind()
    {
        if (Input.anyKeyDown && isRebinding)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                    DoRebind(kcode);
            }
        }
        
    }
    private void DoRebind(KeyCode keyCode)
    {
        Debug.Log("Назначена клавиша: " + keyCode + "для кнопки - " + getAction(IdOfKey));
        RebindAction(IdOfKey, keyCode);
        OutputActions();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = true;
        }

        isRebinding = false;
        slovar.enabled = true;

        Save();
    }
    private void RebindAction(int id, KeyCode keyCode)
    {
        switch (id)
        {
            case 0:
                jumpAction = keyCode; break;
            case 1:
                dashAction = keyCode; break;
            case 2:
                attackAction = keyCode; break;
            case 3:
                sekondAttackAction = keyCode; break;
            case 4:
                standAction = keyCode; break;
            case 5:
                pauseAction = keyCode; break;
            case 6:
                inventoryAction = keyCode; break;
            case 7:
                bestiaryAction = keyCode; break;
            case 8:
                taskAction = keyCode; break;
            case 9:
                settingsAction = keyCode; break;
            case 10:
                mapAction = keyCode; break;
        }
    }

    #region Output
    private void OutputActions()
    {
        for (int i = 0; i < keyTexts.Length; i++)
        {
            keyTexts[i].text = getAction(i).ToString();
        }
    }
    #endregion
    #endregion

    #region Data Managment

    private void Load()
    {
        var data = SaveData.LoadSettingsData();
        EnumJump(data);
        EnumDash(data);
        EnumAttack(data);
        EnumSekArrack(data);
        EnumStandoPower(data);
        EnumPause(data);
        EnumInventory(data);
        EnumBestiary(data);
        EnumTasks(data);
        EnumSettings(data);
        EnumMap(data);
    }

    private void Save()
    {
        SaveData.SaveSettingsData(this);
    }

    #region enumeration

    private void EnumJump(SettingsData set)
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (kcode.ToString() == set.jumpAction) jumpAction = kcode;
        }
    }

    private void EnumDash(SettingsData set)
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (kcode.ToString() == set.dashAction) dashAction = kcode;
        }
    }

    private void EnumAttack(SettingsData set)
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (kcode.ToString() == set.attackAction) attackAction = kcode;
        }
    }

    private void EnumSekArrack(SettingsData set)
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (kcode.ToString() == set.sekondAttackAction) sekondAttackAction = kcode;
        }
    }

    private void EnumStandoPower(SettingsData set)
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (kcode.ToString() == set.standAction) standAction = kcode;
        }
    }

    private void EnumPause(SettingsData set)
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (kcode.ToString() == set.pauseAction) pauseAction = kcode;
        }
    }

    private void EnumInventory(SettingsData set)
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (kcode.ToString() == set.inventoryAction) inventoryAction = kcode;
        }
    }

    private void EnumBestiary(SettingsData set)
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (kcode.ToString() == set.bestiaryAction) bestiaryAction = kcode;
        }
    }

    private void EnumTasks(SettingsData set)
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (kcode.ToString() == set.taskAction) taskAction = kcode;
        }
    }

    private void EnumSettings(SettingsData set)
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (kcode.ToString() == set.settingsAction) settingsAction = kcode;
        }
    }

    private void EnumMap(SettingsData set)
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (kcode.ToString() == set.mapAction) mapAction = kcode;
        }
    }
    #endregion

    #endregion

    private KeyCode getAction(int id)
    {
        switch (id)
        {
            case 0:
                return jumpAction;
            case 1:
                return dashAction;
            case 2:
                return attackAction;
            case 3:
                return sekondAttackAction;
            case 4:
                return standAction;
            case 5:
                return pauseAction;
            case 6:
                return inventoryAction;
            case 7:
                return bestiaryAction;
            case 8:
                return taskAction;
            case 9:
                return settingsAction;
            case 10:
                return mapAction;
            default:
                return KeyCode.None;
        }
    }

}
