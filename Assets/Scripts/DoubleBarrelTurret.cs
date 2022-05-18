using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBarrelTurret : Turret
{
    public Transform firingPoint2;

    private bool _firingPointOccupied = false;

    protected override void Shoot()
    {
        if (_firingPointOccupied == false)
        {
            Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
        }
        if (_firingPointOccupied == true)
        {
            Instantiate(projectilePrefab, firingPoint2.position, firingPoint2.rotation);
        }
        _firingPointOccupied = !_firingPointOccupied;
    }
}
