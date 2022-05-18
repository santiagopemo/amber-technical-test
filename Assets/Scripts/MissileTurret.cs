using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTurret : Turret
{
    protected override void Shoot()
    {
        GameObject proyectile = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
        Missile missile = proyectile.GetComponent<Missile>();
        missile.target = target;
    }
}
