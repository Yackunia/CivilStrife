using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QuestsInfo2 
{
    public List<bool> flags;
    public List<int> dialogs;
    public bool isQuest;

    public bool isStart;

    public bool isAlenaReturn;

    public QuestsInfo2(List<bool> flags, bool isQuest, List<int> DialValue, bool isStart, bool isAlenaReturn)
    {
        this.flags = flags;
        this.isQuest = isQuest;
        dialogs = DialValue;
        this.isStart = isStart;
        this.isAlenaReturn = isAlenaReturn;
    }
}
