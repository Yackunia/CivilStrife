using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class ItemInfo 
{
    public int id;
    public int count;

    public ItemInfo(int id, int count)
    {
        this.id = id;
        this.count = count;
    }
}


public class Inventory2 : MonoBehaviour
{
    public DataBase data;

    // Максимальное количество предметов в инвентаре
    public int maxItems = 4;

    // Список слотов в инвентаре
    public List<ItemInventory> itemCell = new List<ItemInventory>();

    public GameObject gameObjectShow;

    public GameObject BackgroundCell;

    public ItemInventory currentItem;
    public int currentId;

    public Camera cam;
    public EventSystem es;

    public RectTransform movingObj;
    public Vector3 offset;


    public GameObject infoDescriptionObject;
    public Text objInfo;

    public PlayerDataData playerData;

    public List<ItemInfo> Items = new List<ItemInfo>();
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
        ItemInfo temp = new ItemInfo(id,count);
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
    }

    public bool RemoveItem(int id, int count)
    {
        Item item = null;

        for (int i = 0; i < data.items.Count; i++)
        {
            if (data.items[i].id == id)
            {
                item = data.items[i];
                break;
            }
        }

        if (count == 0) return true;

        for (int i = 0; i < itemCell.Capacity; i++)
        {
            if (itemCell[i].count >= count && itemCell[i].id == item.id)
            {
                itemCell[i].count -= count;
                itemCell[i].itemObj.GetComponentInChildren<Text>().text = itemCell[i].count.ToString();

                Remove(item.id, count);
                return true;
            }
        }
        return false;
    }
    public void OnApplicationQuit()
    {
        //SaveInventory.SavePlayerData(playerData);
        PlayerDataData d = new PlayerDataData(Items);
        SaveInventory.SavePlayerData(d);
    }
    public void AddItem(int id, int count)
    {
        Item item = null;

        for (int i = 0; i < data.items.Count; i++)
        {
            if (data.items[i].id == id)
            {
                item = data.items[i];
                break;
            }
        }
        if (count == 0) return;
        for(int i = 0; i < itemCell.Capacity; i++)
        {
            if (itemCell[i].count == 128) continue;
            if(itemCell[i].id == item.id)
            {              
                Add(item.id, count);
                if (itemCell[i].count + count <= 128)
                {
                    itemCell[i].count += count;
                    itemCell[i].itemObj.GetComponentInChildren<Text>().text = itemCell[i].count.ToString();
                    itemCell[i].itemObj.GetComponent<Image>().sprite = item.img;

                    return;
                }
                else
                {
                    count = itemCell[i].count + count - 128;
                    itemCell[i].count = 128;
                    itemCell[i].itemObj.GetComponentInChildren<Text>().text = "128";

                    int g = -1;
                    for(int j = 0; j < itemCell.Capacity; j++)
                    {
                        if (itemCell[j].count == 0)
                        {
                            g = j;
                            break;
                        }
                    }
                    if(g == -1)
                    {
                        Debug.Log("НЕТУ МЕСТА В ИНВЕНТАРЕ");
                        return;
                    }

                    itemCell[g].id = item.id;
                    itemCell[g].count = count;
                    itemCell[g].itemObj.GetComponent<Image>().sprite = item.img;
                    itemCell[g].itemObj.GetComponentInChildren<Text>().text = count.ToString();
                    return;
                }
            }
        }

        int g1 = -1;
        for (int j = 0; j < itemCell.Capacity; j++)
        {
            if (itemCell[j].count == 0)
            {
                g1 = j;
                break;
            }
        }
        if (g1 == -1)
        {
            Debug.Log("НЕТУ МЕСТА В ИНВЕНТАРЕ");
            return;
        }
        itemCell[g1].id = item.id;
        itemCell[g1].count = count;
        Add(item.id, count);
        itemCell[g1].itemObj.GetComponent<Image>().sprite = item.img;
        itemCell[g1].itemObj.GetComponentInChildren<Text>().text = count.ToString();

    }

    private void Start()
    {
           if (File.Exists(Application.persistentDataPath
      + "/loli.log"))
           {
               BinaryFormatter bf = new BinaryFormatter();
               FileStream file =
                 File.Open(Application.persistentDataPath
                 + "/loli.log", FileMode.Open);
               PlayerDataData data = (PlayerDataData)bf.Deserialize(file);
               file.Close();

               for(int i = 0; i < data.Items.Count; i++)
               {
                   AddItem(data.Items[i].id, data.Items[i].count);
               }
               Debug.Log("Game data loaded!");
           }
           else
               Debug.Log("There is no save data!");

    }
    public void Update()
    {
       /* if (currentId != -1)
        {
            MoveObjects();
        }*/

        if (Input.GetKeyDown(KeyCode.I))
        {
            BackgroundCell.SetActive(!BackgroundCell.activeSelf);
            infoDescriptionObject.SetActive(false);
            Cursor.visible = BackgroundCell.activeSelf;
        }

    }
 
    public void AddInventoryItem(int id, ItemInventory itemInv)
    {
        itemCell[id].id = itemInv.id;
        itemCell[id].count = itemInv.id;
        itemCell[id].itemObj.GetComponent<Image>().sprite = data.items[itemInv.id].img;

        if (itemInv.count > 1 && itemInv.id != 0) // ? 
        {
            itemCell[id].itemObj.GetComponentInChildren<Text>().text = itemInv.ToString();
        }
        else
        {
            itemCell[id].itemObj.GetComponentInChildren<Text>().text = "";
        }
    }
    /*public void AddGraphics()
    {
        for (int i = 0; i < maxItems*2; i++)
        {
            GameObject newItem = Instantiate(gameObjectShow, BackgroundCell.transform) as GameObject;

            newItem.name = i.ToString();

            ItemInventory II = new ItemInventory();
            II.itemObj = newItem;

         //   RectTransform rt = newItem.GetComponent<RectTransform>();
         //   rt.localPosition = new Vector3(0, 0, 0);
         //   rt.localScale = new Vector3(1, 1, 1);
         //   newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectedObject(); });

            itemCell.Add(II);
        }
    }*/
    public void UpdateInventory()
    {
        for (int i = 0; i < maxItems; i++)
        {
            if (itemCell[i].id != 0 && itemCell[i].count > 1)
            {
                itemCell[i].itemObj.GetComponent<Text>().text = itemCell[i].count.ToString();
            }
            else
            {
                itemCell[i].itemObj.GetComponent<Text>().text = "";
            }
            itemCell[i].itemObj.GetComponent<Image>().sprite = data.items[itemCell[i].id].img;
        }
    }
    public void SelectedObject()
    {
        currentId = int.Parse(es.currentSelectedGameObject.name);

        infoDescriptionObject.SetActive(true);
        if (itemCell[currentId].count < 1) return;

        objInfo.text = data.items[itemCell[currentId].id].description.ToString();

        if (currentId == -1)
        {
            currentId = int.Parse(es.currentSelectedGameObject.name);
            if (itemCell[currentId].count == 0) return;

            currentItem = itemCell[currentId];
            movingObj.gameObject.SetActive(true);
            movingObj.GetComponent<Image>().sprite = data.items[currentItem.id].img;

            infoDescriptionObject.SetActive(true);

         //   AddItem(currentId, data.items[0], 0);
        }
        else
        {
            ItemInventory II = itemCell[int.Parse(es.currentSelectedGameObject.name)];
            int temp = int.Parse(es.currentSelectedGameObject.name);
           /* if (currentItem.id != II.id)
            {
                AddInventoryItem(currentId, itemCell[int.Parse(es.currentSelectedGameObject.name)]);
                AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
            }
            else
            {
                if (II.count + currentItem.count <= 128)
                {
                    II.count += currentItem.count;
                }
                else
                {
                    AddItem(currentId, data.items[II.id], II.count + currentItem.count - 128);

                    II.count = 128;
                }

                II.itemObj.GetComponentInChildren<Text>().text = II.count.ToString();
            }

            currentId = -1;
           
            movingObj.gameObject.SetActive(false);*/
            if (currentItem.id == II.id)
            {
                // nothing
            }
            else
            {
                if(II.count == 0)
                {
                  //  itemCell[currentId].itemObj.gameObject.GetComponent<Image>().sprite = null;

                  //  currentId = II.id;

                    itemCell[temp].itemObj.gameObject.GetComponent<Image>().sprite = itemCell[currentId].itemObj.gameObject.GetComponent<Image>().sprite;
                    itemCell[temp].id = itemCell[currentId].id;
                    itemCell[temp].count = itemCell[currentId].count;
                    itemCell[temp].itemObj.GetComponentInChildren<Text>().text = itemCell[temp].count.ToString();

                    itemCell[currentId].itemObj.GetComponentInChildren<Text>().text = "";
                    itemCell[currentId].itemObj.gameObject.GetComponent<Image>().sprite = null;
                    itemCell[currentId].count = 0;
                    itemCell[currentId].id = -1;
                }
            }
            currentId = -1;

            movingObj.gameObject.SetActive(false);


        }
    }
    public void MoveObjects()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = BackgroundCell.GetComponent<RectTransform>().position.z;
        movingObj.position = Input.mousePosition + offset;
    }
    public ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory();

        New.id = old.id;
        New.itemObj = old.itemObj;
        New.count = old.count;

        return New;
    }
}

[System.Serializable]
public class ItemInventory
{
    public int id = -1;
    public GameObject itemObj;

    public int count = 0;
}