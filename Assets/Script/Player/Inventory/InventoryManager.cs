using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IDataSaveGame
{
    private bool menuActive;
    private bool infoActive;
    public GameObject inventory;
    public GameObject InfoPlayer;
    public GameObject inventorySlot;
    public int inventorySize = 40;
    public InventorySlot prefabSlot;
    public List<InventorySlot> listOfUIItems = new List<InventorySlot>();
    private PlayerController playerController;

    public void Awake()
    {
        playerController = gameObject.GetComponentInParent<PlayerController>();
        initInventory();
    }

    private void Start()
    {
        PrePareData();
        setDataInfoPlayer();


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
        if (listOfUIItems[index].isEmpty)
            return;

        listOfUIItems[index].select();

    }

    public void PrePareData()
    {
        //for (int i = 0; i < playerController.playerModel.itemToBag.Count; i++)
        //{
        //    if (!playerController.playerModel.itemToBag[i].isEmpty)
        //    {
        //        AddItem(playerController.playerModel.itemToBag[i].itemModel, playerController.playerModel.itemToBag[i].quantity);
        //    }
        //}
    }



    public void CloseDetailUI()
    {
        GameObject go = GameObject.Find("InventoryDescription").gameObject;
        go.active = false;
    }

    public void AddItem(ItemModel item, int quantity)
    {
        AudioManager.instance.PlaySFX("player take item");
        if (item.IsStackable == false)
        {
            AddItemToFirstFreeSlot(item, quantity);
        }
        else
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
                playerController.playerModel.ChangItemInBag(i, new ItemBag(item, quantity, false));
                return;
            }
        }
    }
    private void AddStackableItem(ItemModel item, int quantity)
    {
        for (int i = 0; i < listOfUIItems.Count; i++)
        {
            if (!listOfUIItems[i].isEmpty)
            {
                if (listOfUIItems[i].itemModel.ID == item.ID)
                {
                    int amountStack = listOfUIItems[i].itemModel.MaxStackSize - listOfUIItems[i].quantity;
                    if (quantity > amountStack)
                    {
                        listOfUIItems[i].ChangeQuantity(listOfUIItems[i].itemModel.MaxStackSize);
                        playerController.playerModel.ChangItemInBag(i, new ItemBag(item, listOfUIItems[i].itemModel.MaxStackSize, false));
                        return;
                    }
                    else
                    {
                        listOfUIItems[i].ChangeQuantity(listOfUIItems[i].quantity + quantity);
                        playerController.playerModel.ChangItemInBag(i, new ItemBag(item, listOfUIItems[i].quantity + quantity, false));
                        return;
                    }
                }
            }

        }

        AddItemToFirstFreeSlot(item, quantity);
    }

    void OnOpenInfoPlayer()
    {
        setDataInfoPlayer();
        if (infoActive)
        {
            InfoPlayer.SetActive(false);
            infoActive = false;
        }
        else if (!infoActive)
        {

            InfoPlayer.SetActive(true);
            infoActive = true;
        }
    }

    public void DropItemToBody(int pos)
    {
        InfoPlayerController info = InfoPlayer.GetComponent<InfoPlayerController>();
        ItemModel item = playerController.playerModel.itemBody[pos];
        AddItem(item, 1);
        playerController.subPowerItem(item);
        playerController.playerModel.itemBody[pos] = new ItemModel();
        info.setIndexValue(playerController);

    }

    public void setDataInfoPlayer()
    {
        InfoPlayerController info = InfoPlayer.GetComponent<InfoPlayerController>();

        foreach (ItemModel item in playerController.playerModel.itemBody)
        {
            if (item != null)
            {
                info.setIconBody(item.type, item.ItemImage);
                info.setIndexValue(playerController);
            }
        }
    }

    void OnOpenInventory()
    {
        if (menuActive)
        {
            Time.timeScale = 1;
            inventory.SetActive(false);
            menuActive = false;
        }
        else if (!menuActive)
        {
            Time.timeScale = 0;
            inventory.SetActive(true);
            menuActive = true;
        }
    }

    public void LoadData(GameData gameData)
    {

        for (int i = 0; i < gameData.player.itemToBag.Count; i++)
        {
            if (gameData.player.itemToBag[i].isEmpty != true)
            {

                listOfUIItems[i].AddItem(gameData.player.itemToBag[i].itemModel, gameData.player.itemToBag[i].quantity);
            }

        }
        //Debug.Log("listOfUIItems : " + listOfUIItems.Count);
        //Debug.Log(" gameData.player.itemToBag.Count : " + gameData.player.itemToBag.Count);

    }


    public void SaveData(ref GameData gameData)
    {
        //for (int i = 0; i < listOfUIItems.Count; i++)
        //{
        //    if (!listOfUIItems[i].isEmpty)
        //    {
        //        gameData.player.ChangItemInBag(i, new ItemBag(listOfUIItems[i].itemModel, listOfUIItems[i].quantity, false));

        //    }

        //}

    }


}
