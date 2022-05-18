using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    public Transform target;
    public float rotateSpeed = 100f;
    public float damageRadius = 5f;

    private Vector3 _targetPosition;

    protected override void Update()
    {
        base.Update();
        RotateTowardsTarget();
    }

    protected void RotateTowardsTarget()
    {
        if (target) _targetPosition = target.position;
        Vector3 direction = _targetPosition - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
    }

    protected override void DestroyProjectile()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRadius, targetLayer);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Character character = hitColliders[i].GetComponent<Character>();
            if (character) character.TakeDamage(projectileDamage);
        }
        if (hitParticles) Instantiate(hitParticles, transform.position, transform.rotation);
        base.DestroyProjectile();
    }

    protected override void CheckHit()
    {
        Ray ray = new Ray(lastPosition, transform.forward);
        float distance = Vector3.Distance(lastPosition, transform.position);

        if (Physics.RaycastNonAlloc(ray, hit, distance, targetLayer) > 0)
        {
            DestroyProjectile();
        }
    }
}
