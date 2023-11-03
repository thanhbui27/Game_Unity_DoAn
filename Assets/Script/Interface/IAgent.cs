using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgent 
{
    public int Level { set; get; }
    public float Heath { set; get; }

    public float Dame { set; get; }
    public bool TargetAttack { set; get; }
    public void OnHit(float dame);
    public void OnHit(float dame, Vector2 knock);

    public void Death();
}
