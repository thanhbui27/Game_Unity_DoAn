using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    private bool menuActive;
    public GameObject inventory;
    public GameObject inventorySlot;
    public int inventorySize { get; private set; } = 35;
    public InventorySlot prefabSlot;
    public List<InventorySlot> listOfUIItems = new List<InventorySlot>();
    public event Action<Dictionary<int, InventorySlot>> OnInventoryUpdated;
    private void Start()
    {

        initInventory();
        PrePareData();
    }


    public void initInventory()
    {
        if (inventorySize <= 0)
            return;

        for (int i = 0; i < inventorySize; i++)
        {
            InventorySlot slot = Instantiate(prefabSlot, Vector3.zero, Quaternion.identity);
            listOfUIItems.Add(slot);
            slot.transform.SetParent(inventorySlot.transform);
        }
    }
    public void PrePareData()
    {
       
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
