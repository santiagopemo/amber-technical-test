using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileDamage = 15f;
    public float lifeTime = 2f;
    public float projectileSpeed = 500f;
    public GameObject hitParticles;
    public LayerMask targetLayer;

    protected RaycastHit[] hit = new RaycastHit[1];    
    protected Vector3 lastPosition;

    private float _lifeTimeDelay;

    protected virtual void Start()
    {
        _lifeTimeDelay = lifeTime;
    }

    protected virtual void Update()
    {
        Utilities.UpdateTimer(true, ref _lifeTimeDelay, lifeTime, DestroyProjectile);
        lastPosition = transform.position;
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
        CheckHit();
    }

    protected virtual void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    protected virtual void Hit(Character character)
    {
        character.TakeDamage(projectileDamage);        
    }

    protected virtual void CheckHit()
    {
        Ray ray = new Ray(lastPosition, transform.forward);
        float distance = Vector3.Distance(lastPosition, transform.position);

        if (Physics.RaycastNonAlloc(ray, hit, distance, targetLayer) > 0)
        {
            if (hitParticles) 
                Instantiate(hitParticles, hit[0].point, Quaternion.LookRotation(hit[0].normal));
            Character character = hit[0].transform.GetComponent<Character>();
            if (character) 
                character.TakeDamage(projectileDamage);
            DestroyProjectile();
        }
    }
}
