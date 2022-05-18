using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Character
{
    public float firingRange = 15f;
    public float firingRate = 1f;
    public GameObject projectilePrefab;
    public LayerMask targetLayer;
    public Transform turretHead;
    public Transform firingPoint;
    public float searchRate = 0.5f;

    protected Transform target;

    private float _searchDelay;
    private float _firingDelay;

    void Start()
    {
        _searchDelay = searchRate;
        _firingDelay = firingRate;
        SearchTarget();
    }

    void Update()
    {
        Utilities.UpdateTimer(target == null, ref _searchDelay, searchRate, SearchTarget);
        if (target)
        {
            LookOnTarget();
            Utilities.UpdateTimer(true, ref _firingDelay, firingRate, Shoot);
        }
    }

    protected void SearchTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, firingRange, targetLayer);
        if (hitColliders.Length > 0)
        {
            target = hitColliders[0].transform;
        }
        else
        {
            target = null;
        }
    }

    protected void LookOnTarget()
    {
        turretHead.LookAt(target, Vector3.up);
    }

    protected virtual void Shoot()
    {
        Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, firingRange);
    }
}
