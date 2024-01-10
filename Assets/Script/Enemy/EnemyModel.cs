public class EnemyModel
{
    public float _speed = 1f;

    public bool _isLive = true;

    public int _maxHP;

    public float _health;

    public int _level = 1;

    public int _armor = 1;

    public float _dame;

    public int _exp;
    public float chaseDistanceThreshold, attackDistanceThreshold;

    public EnemyModel(float speed, bool islive, int maxHp, float health, int level, int armor, float dame, int exp, float chaseDistanceThreshold, float attackDistanceThreshold)
    {
        this._speed = speed;
        this._isLive = islive;
        this._maxHP = maxHp;
        this._health = health;
        this._level = level;
        this._armor = armor;
        this._dame = dame;
        this._exp = exp;

        this.chaseDistanceThreshold = chaseDistanceThreshold;
        this.attackDistanceThreshold = attackDistanceThreshold;
    }
    public void EnemyDie()
    {
        this._isLive = false;
    }
}
