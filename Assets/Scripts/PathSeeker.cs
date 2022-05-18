using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PathSeeker : MobileFighter
{
    public Paths paths;
    public float startSpeed = 5f;

    private Transform _target;
    [SerializeField] private Path _path;
    private int _pathNodeIndex = 0;
    private bool _pathCompleted = false;

    protected override void Start()
    {
        base.Start();
        SeekForPaths();
        _path = paths.GetRandomPath();
        _target = _path.nodes[_pathNodeIndex];
        navMeshAgent.speed = startSpeed;
        navMeshAgent.SetDestination(_target.position);
    }

    protected virtual void SeekForPaths()
    {
        paths = FindObjectOfType<Paths>();
    }

    protected override void Update()
    {
        base.Update();
        if (!_pathCompleted && Vector3.Distance(transform.position, _target.position) <= 0.4f)
        {
            SetNextTargetInPath();
        }
        CheckFighting(opponent != null, !isFighting, 0f, true);
        CheckFighting(opponent == null, isFighting, startSpeed, false);
    }


    private void SetNextTargetInPath()
    {
        _pathNodeIndex++;
        if (_pathNodeIndex < _path.nodes.Length)
        {
            _target = _path.nodes[_pathNodeIndex];
            navMeshAgent.SetDestination(_target.position);
        }
        else
            _pathCompleted = true;
    }
}
