using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class PlayerDataData
{
    public List<ItemInfo> Items = new List<ItemInfo>();

    public PlayerDataData(List<ItemInfo> II)
    {
        Items = II;
    }
    /*
    public void Add(int id, int count)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].id == id)
            {
                Items[i].count += count;
                return;
            }
        }
        ItemInfo temp = null;
        temp.id = id;
        temp.count = count;
        Items.Add(temp);
    }

    public void Remove(int id, int count)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].id == id)
            {
                Items[i].count -= count;
                return;
            }
        }
    }*/


}

