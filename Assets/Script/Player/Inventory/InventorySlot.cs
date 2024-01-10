using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    //public bool IsStackable { get; set; }

    //public int ID;
    //public int MaxStackSize { get; set; }

    //public string Name { get; set; }

    public int quantity { get; set; }

    //public string description { get; set; }

    //public Sprite ItemImage { get; set; }

    public bool isEmpty = true;

    public ItemModel itemModel;

    [SerializeField]
    private TMP_Text quantitytex;

    [SerializeField]
    private Image image;

    [SerializeField]
    private Image borderImage;

    [SerializeField]
    private GameObject panelSelectOption;

    [SerializeField]
    private GameObject inventoryDescription;


    public Action<InventorySlot> pointClickEvent, pointClickRight;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    public void AddItem(ItemModel item, int quantity)
    {

        this.itemModel = item;
        this.quantity = quantity;
        this.quantitytex.enabled = true;
        this.quantitytex.text = quantity.ToString();
        this.image.sprite = item.ItemImage;
        this.image.enabled = true;
        isEmpty = false;

    }


    public void OpenDetailUI()
    {
        GameObject inventory = GameObject.Find("InventoryCanvas").gameObject;
        GameObject ivtrDs = Instantiate(inventoryDescription);

        InventoryDescription ids = ivtrDs.GetComponent<InventoryDescription>();
        ids.ShowInfoItem(this.itemModel.ItemImage, this.itemModel.Name, this.itemModel.Description);

        ivtrDs.transform.SetParent(inventory.transform);
        ivtrDs.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

    }

    public void useItem()
    {
        PlayerController pl = player.GetComponent<PlayerController>();

        int index = pl.playerModel.itemToBag.FindLastIndex(item => item.itemModel.ID == this.itemModel.ID);
        pl.playerModel.ChangItemInBag(index, new ItemBag(new ItemModel(), 0, true));

        switch (this.itemModel.ID)
        {
            case 1:

                break;
            case 2:
                pl.RestoreHP(this.itemModel.HP);
                this.ChangeQuantity(this.quantity - 1);
                break;
            case 3:
                pl.RestoreHP(this.itemModel.HP);
                this.ChangeQuantity(this.quantity - 1);
                break;
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
            case 19:
            case 20:
            case 21:
            case 22:
            case 23:
            case 24:
            case 25:
            case 26:
                pl.getPowerItem(itemModel);

                this.ChangeQuantity(this.quantity - 1);
                break;

            default:
                pl.TextCT("không thể sử dụng vật phẩm này", Color.yellow);
                break;

        }

    }


    public void ResetData()
    {

        borderImage.enabled = false;
        panelSelectOption.active = false;
        isEmpty = true;
        itemModel = null;
        quantitytex.enabled = false;
        image.enabled = false;
        image.sprite = null;

    }

    public void select()
    {

        borderImage.enabled = true;
        panelSelectOption.active = true;
    }

    public void DeSelect()
    {
        borderImage.enabled = false;
        panelSelectOption.active = false;
    }


    public void ChangeQuantity(int newQuantity)
    {
        if (newQuantity < 1)
        {
            ResetData();
            return;
        }
        this.quantity = newQuantity;
        this.quantitytex.text = newQuantity.ToString();

    }

    public void OnClickPointer(BaseEventData data)
    {
        PointerEventData p = data as PointerEventData;

        if (p.button == PointerEventData.InputButton.Right)
        {
            pointClickRight.Invoke(this);
        }
        else
        {
            pointClickEvent.Invoke(this);
        }
    }

}
