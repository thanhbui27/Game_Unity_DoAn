using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPlayerController : MonoBehaviour
{
    public Image ItemMu;
    public Image ItemQU;
    public Image ItemGI;
    public Image ItemVK;

    public TMP_Text _hp;
    public TMP_Text _mp;
    public TMP_Text _armor;
    public TMP_Text _speed;
    public TMP_Text _attack;

    private InventoryManager _inventoryMG;
    private void Start()
    {
        _inventoryMG = gameObject.GetComponentInParent<InventoryManager>();
    }


    public int indexTypeItem;

    public void setIconBody(int pos, Sprite img)
    {
        switch (pos)
        {
            case 1:
                ItemMu.enabled = true;
                ItemMu.sprite = img;
                break;
            case 2:
                ItemQU.enabled = true;
                ItemQU.sprite = img;
                break;
            case 3:
                ItemGI.enabled = true;
                ItemGI.sprite = img;
                break;
            case 4:
                ItemVK.enabled = true;
                ItemVK.sprite = img;
                break;
            default:
                break;

        }
    }

    public void removeItemMu()
    {
        ItemMu.enabled = false;
        ItemMu.sprite = null;
        _inventoryMG.DropItemToBody(0);
    }
    public void removeItemAo()
    {
        ItemQU.enabled = false;
        ItemQU.sprite = null;
        _inventoryMG.DropItemToBody(1);
    }

    public void removeItemGiay()
    {
        ItemGI.enabled = false;
        ItemGI.sprite = null;
        _inventoryMG.DropItemToBody(2);
    }

    public void removeItemVK()
    {
        ItemVK.enabled = false;
        ItemVK.sprite = null;
        _inventoryMG.DropItemToBody(3);
    }



    public void setIndexValue(PlayerController pl)
    {
        if (pl == null)
            return;

        _hp.text = pl.playerModel._health.ToString();
        _mp.text = "1";
        _armor.text = pl.playerModel._armor.ToString();
        _attack.text = pl.playerModel._dame.ToString();
        _speed.text = pl.playerModel._speed.ToString();

    }

}
