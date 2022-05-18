using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MobileFighter : MeleeFighter
{
    protected NavMeshAgent navMeshAgent;
    protected bool isFighting = false;

    protected virtual new void Start()
    {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected void CheckFighting(bool isOpponent, bool fighting, float speed, bool nextIsFighting)
    {
        if (isOpponent && fighting)
        {
            navMeshAgent.speed = speed;
            isFighting = nextIsFighting;
        }
    }
}
