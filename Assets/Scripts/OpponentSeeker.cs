using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class OpponentSeeker : MobileFighter
{
    public float startSpeed = 5f;
    public float searchRadius = 3f;
    public float searchRate = 2f;
    public float chasingRate = 0.2f;
    public LayerMask opponentLayer;

    private Transform _target = null;
    private float _searchDelay;
    private Vector3 _initialPosition;
    private float _chasingDelay;

    protected override void Start()
    {
        base.Start();
        navMeshAgent.speed = 0;
        _searchDelay = searchRate;
        _chasingDelay = chasingRate;
        _initialPosition = transform.position;
        SearchTarget();
    }

    protected override void Update()
    {
        base.Update();
        Utilities.UpdateTimer(_target == null, ref _searchDelay, searchRate, SearchTarget);
        Utilities.UpdateTimer(_target != null, ref _chasingDelay, chasingRate, Chase);
        CheckFighting(opponent != null, !isFighting, 0f, true);
        CheckFighting(opponent == null, isFighting, startSpeed, false);
    }

    private void Chase()
    {
        navMeshAgent.SetDestination(_target.position);
    }

    protected void SearchTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchRadius, opponentLayer);
        if (hitColliders.Length > 0 && hitColliders[0].transform.tag.Equals(attackToTag))
        {
            _target = hitColliders[0].transform;
            navMeshAgent.SetDestination(_target.position);
            navMeshAgent.speed = startSpeed;
        }
        else
        {
            _target = null;
            navMeshAgent.SetDestination(_initialPosition);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
