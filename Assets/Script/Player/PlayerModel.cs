using System.Collections.Generic;
using Unity.Mathematics;

[System.Serializable]
public class PlayerModel
{
    private bool isPlayer = true;

    public float _speed = 1f;

    public bool _isLive = true;

    public int _maxHP;

    public float _health;

    public int _level = 1;

    public int _armor = 1;

    public float _dame;

    public int _exp;

    public List<ItemModel> itemBody = new List<ItemModel>();

    public List<ItemBag> itemToBag = new List<ItemBag>();

    public PlayerModel()
    {
        this._speed = 1f;
        this._isLive = true;
        this._maxHP = 10;
        this._health = 10;
        this._level = 1;
        this._armor = 1;
        this._dame = 5;
        this._exp = 0;
        this.isPlayer = true;

    }

    public PlayerModel(float speed, bool islive, int maxHp, float health, int level, int armor, float dame, int exp, bool player)
    {
        this._speed = speed;
        this._isLive = islive;
        this._maxHP = maxHp;
        this._health = health;
        this._level = level;
        this._armor = armor;
        this._dame = dame;
        this._exp = exp;
        this.isPlayer = player;

    }

    public void initItemBody()
    {
        for (int i = 0; i < 4; i++)
        {
            itemBody.Add(new ItemModel());
        }

    }

    public void initItemToBag()
    {
        for (int i = 0; i < 40; i++)
        {
            itemToBag.Add(new ItemBag(new ItemModel(), 0, true));
        }
    }

    public void setExp(int exp)
    {
        this._exp += exp;
    }

    public void ChangItemInBag(int index, ItemBag itemBag)
    {
        itemToBag[index] = itemBag;
    }
    public void getPowerItem(ItemModel item, CanvasController canvas)
    {
        switch (item.type)
        {
            case 1:
                //mu
                itemBody[0] = item;
                this._maxHP += item.HP;

                canvas.UpdateHP(this._health, _maxHP);
                canvas.initInfoHp(this._health, this._maxHP);
                break;
            case 2:
                //ao
                itemBody[1] = item;
                this._armor += item.Armor;

                break;
            case 3:
                //giay
                itemBody[2] = item;
                this._speed += item.Speed / 10;

                break;
            case 4:
                //kiem
                itemBody[3] = item;
                this._dame += item.dame;

                break;
            default:
                break;
        }
    }

    public void subPowerItem(ItemModel item, CanvasController canvas)
    {
        switch (item.type)
        {
            case 1:
                //mu
                itemBody[0] = item;
                this._maxHP -= item.HP;
                canvas.UpdateHP(this._health, _maxHP);
                canvas.initInfoHp(this._health, this._maxHP);
                break;
            case 2:
                //ao
                itemBody[1] = item;
                this._armor -= item.Armor;

                break;
            case 3:
                //giay
                itemBody[2] = item;
                this._speed -= item.Speed / 10;

                break;
            case 4:
                //kiem
                itemBody[3] = item;
                this._dame -= item.dame;

                break;
            default:
                break;

        }

    }

    public void UpLevel()
    {

        this._level += 1;
        int upHp;

        if (itemBody[0].HP > 0)
        {
            upHp = (int)math.round((this._maxHP - itemBody[0].HP) * 0.4);

        }
        else
        {
            upHp = (int)math.round(this._maxHP * 0.4);
        }

        this._health += upHp;
        this._maxHP += upHp;
        this._dame += (int)math.round(this._dame * 0.5);
        this._armor += 1;
        this._speed += (float)math.round(this._speed * 0.2);
        this._exp = 1;


    }

    public void PlayerDie()
    {
        this._isLive = false;
    }

    public int ExpToUpLevel()
    {
        int exptempalte = 10;
        return this._level * exptempalte;
    }

}

[System.Serializable]
public class ItemBag
{
    public int quantity;
    public bool isEmpty;
    public ItemModel itemModel;

    public ItemBag(ItemModel itemModel, int quantity, bool isEmpty)
    {
        this.quantity = quantity;
        this.itemModel = itemModel;
        this.isEmpty = isEmpty;

    }

}