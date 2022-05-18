using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancePathSeeker : PathSeeker
{
    public int barrierAttackMultiplier = 2;
    protected override void OnTriggerEnter(Collider other)
    {
        Trap trap = other.GetComponent<Trap>();
        if (trap)
        {
            trap.TakeDamage(int.MaxValue);
        }
        Barrier barrier = other.GetComponent<Barrier>();
        if (barrier)
        {
            barrier.TakeDamage(attackPower * barrierAttackMultiplier);
        }
        base.OnTriggerEnter(other);
    }
}
