using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tower : MonoBehaviour
{
    public int allowedIntruders = 5;
    public LayerMask enemyLayer;
    [SerializeField] private int _currentIntruders = 0;

    public delegate void OnTowerFallen();
    public event OnTowerFallen TowerHasFallenEvent;

    public delegate void OnIntruderTraspassed(int currentIntruders);
    public event OnIntruderTraspassed IntruderHasTraspassed;
    
    private void OnTriggerEnter(Collider other)
    {
        if ((enemyLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            other.GetComponent<Character>()?.Die();
            _currentIntruders++;

            IntruderHasTraspassed?.Invoke(_currentIntruders);

            if (_currentIntruders == allowedIntruders)
            {
                TowerHasFallenEvent?.Invoke();
            }
        }
    }
}
