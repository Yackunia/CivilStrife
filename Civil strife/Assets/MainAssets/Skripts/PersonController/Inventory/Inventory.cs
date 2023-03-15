using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public DataBase data;

    // Максимальное количество предметов в инвентаре
    public int maxItems = 10;

    // Список слотов в инвентаре
    public List<ItemInventory> itemCell = new List<ItemInventory>();

    public GameObject gameObjectShow;

    public GameObject InventoryMainObject;

    public ItemInventory currentItem;
    public int currentId; 

    public Camera cam;
    public EventSystem es;

    public RectTransform movingObj;
    public Vector3 offset;

    public void AddItem(int id, Item item, int count)
    {
        itemCell[id].id = item.id;
        itemCell[id].count = item.id;
        itemCell[id].itemObj.GetComponent<Image>().sprite = item.img;

        if (count > 1 && item.id != 0) // ? 
        {
            itemCell[id].itemObj.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            itemCell[id].itemObj.GetComponentInChildren<Text>().text = "";
        }
    }
    private void Start()
    {
        if(itemCell.Count == 0)
        {
            AddGraphics();
        }

        for(int i = 0; i < maxItems; i++)
        {
            AddItem(i, data.items[Random.Range(0, data.items.Count)], Random.Range(0, 99));
        }
    }
    public void Update()
    {
        if(currentId != -1)
        {
            MoveObjects();
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
    public void AddGraphics()
    {
        for(int i = 0; i < maxItems; i++)
        {
            GameObject newItem = Instantiate(gameObjectShow, InventoryMainObject.transform) as GameObject;

            newItem.name = i.ToString();

            ItemInventory II = new ItemInventory();
            II.itemObj = newItem;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectedObject(); });

            itemCell.Add(II);
        }
    }
    public void UpdateInventory()
    {
        for(int i = 0; i < maxItems; i++)
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
        if (currentId == -1)
        {
            currentId = int.Parse(es.currentSelectedGameObject.name);
            currentItem = CopyInventoryItem(itemCell[currentId]);
            movingObj.gameObject.SetActive(true);
            movingObj.GetComponent<Image>().sprite = data.items[currentItem.id].img;

            AddItem(currentId, data.items[0], 0);
        }
        else
        {
            AddInventoryItem(currentId, itemCell[int.Parse(es.currentSelectedGameObject.name)]);
            AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
            currentId = -1;

            movingObj.gameObject.SetActive(false);

        }
    }
    public void MoveObjects()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = InventoryMainObject.GetComponent<RectTransform>().position.z;
        movingObj.position = cam.ScreenToWorldPoint(pos);
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
    public int id;
    public GameObject itemObj;

    public int count;
}