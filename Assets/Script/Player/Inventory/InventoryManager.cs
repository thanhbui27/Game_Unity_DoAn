using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    private bool menuActive;
    public GameObject inventory;
    public GameObject inventorySlot;
    public int inventorySize  = 40;
    public InventorySlot prefabSlot;
    public List<InventorySlot> listOfUIItems = new List<InventorySlot>();


    private void Start()
    {

        initInventory();
        PrePareData();
    }

    internal void ResetSelectItems()
    {
        foreach (var item in listOfUIItems)
        {
            item.DeSelect();
        }
    }



    public void initInventory()
    {
        if (inventorySize <= 0)
            return;

        for (int i = 0; i < inventorySize; i++)
        {
            InventorySlot slot = Instantiate(prefabSlot, Vector3.zero, Quaternion.identity);
            slot.pointClickEvent += handleOnclickItem;
            listOfUIItems.Add(slot);
            slot.transform.SetParent(inventorySlot.transform);
        }
    }



    private void handleOnclickItem(InventorySlot data)
    {
        int index = listOfUIItems.IndexOf(data);
        if (index == -1)
            return;

        ResetSelectItems();
        listOfUIItems[index].select();
        Debug.Log("click");
    }

    public void PrePareData()
    {
       
    }

    public void OpenDetailUI()
    {
        GameObject go = GameObject.Find("InventoryDescription").gameObject;
        go.active = true;
    }

    public void CloseDetailUI()
    {
        GameObject go = GameObject.Find("InventoryDescription").gameObject;
        go.active = false;
    }

    public void AddItem(ItemModel item,int quantity)
    {
      
        if(item.IsStackable == false)
        {
            AddItemToFirstFreeSlot(item, quantity);
        }else
        {
            AddStackableItem(item, quantity);
        }

       
    }


    private bool InventoryFull() => listOfUIItems.Where(item => item.isEmpty).Any() == false;

    private void AddItemToFirstFreeSlot(ItemModel item, int quantity)
    {

        for (int i = 0; i < listOfUIItems.Count; i++)
        {
            if (listOfUIItems[i].isEmpty)
            {
                listOfUIItems[i].AddItem(item, quantity);
                return;
            }
        }
    }
    private void AddStackableItem(ItemModel item, int quantity)
    {
        for (int i = 0; i < listOfUIItems.Count; i++)
        {
            if (listOfUIItems[i].ID == item.ID)
            {
                int amountStack = listOfUIItems[i].MaxStackSize - listOfUIItems[i].quantity;
                if(quantity > amountStack)
                {
                    listOfUIItems[i].ChangeQuantity(listOfUIItems[i].MaxStackSize);
                    return;
                }
                else
                {
                    listOfUIItems[i].ChangeQuantity(listOfUIItems[i].quantity + quantity);
                    return;
                }
            }
        }

        AddItemToFirstFreeSlot(item, quantity);

    }

    

    void OnOpenInventory()
    {
        if (menuActive)
        {
            Time.timeScale = 1;
            inventory.SetActive(false);
            menuActive = false;
        }else if (!menuActive)
        {
            Time.timeScale = 0;
            inventory.SetActive(true);
            menuActive = true;
        }
    }
}
