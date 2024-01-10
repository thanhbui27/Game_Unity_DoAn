using UnityEngine;

public interface IAgent
{
    public bool TargetAttack { set; get; }
    public void OnHit(float dame);
    public void OnHit(float dame, Vector2 knock);

    public void Death();
}
