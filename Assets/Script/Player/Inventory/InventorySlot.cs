using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySlot : MonoBehaviour
{

    public bool IsStackable { get; set; }

    public int ID;
    public int MaxStackSize { get; set; }

    public string Name { get; set; }

    public int quantity { get; set; }

    public Sprite ItemImage { get; set; }

    public bool isEmpty = true;
    
    [SerializeField]
    private TMP_Text quantitytex;

    [SerializeField]
    private Image image;

    [SerializeField]
    private Image borderImage;

    [SerializeField]
    private GameObject panelSelectOption;


    public Action<InventorySlot> pointClickEvent, pointClickRight;
    public void AddItem(ItemModel item, int quantity)
    {
       
       this.IsStackable = item.IsStackable; 
       this.ID = item.ID;
       this.MaxStackSize = item.MaxStackSize;
       this.Name = item.Name;
       this.quantity = quantity;
       this.ItemImage = item.ItemImage;
       this.quantitytex.enabled = true;
       this.quantitytex.text = quantity.ToString();
       this.image.sprite = item.ItemImage;
       this.image.enabled = true;
       isEmpty = false;
        
    }

    public void OpenDetailUI()
    {
        GameObject go = GameObject.Find("InventoryDescription").gameObject;
        go.active = true;
    }


    public void ResetData()
    {
        isEmpty = true;
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

        this.quantity = newQuantity;
        this.quantitytex.text = newQuantity.ToString();
    }

    public void OnClickPointer(BaseEventData data)
    {
        PointerEventData p = data as PointerEventData;

        if(p.button == PointerEventData.InputButton.Right)
        {
            pointClickRight.Invoke(this);
        }else
        {
            pointClickEvent.Invoke(this);
        }
    }

}
