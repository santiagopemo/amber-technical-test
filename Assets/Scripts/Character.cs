using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class Character : MonoBehaviour
{
    public float health;
    public GameObject healthBarPrefab;

    protected HealthBar healthBar;

    [SerializeField] private float _currentHealth;

    protected virtual void Awake()
    {
        _currentHealth = health;
        SetHealthBar();
    }

    private void SetHealthBar()
    {
        if (healthBarPrefab == null) return;

        Collider collider = GetComponent<Collider>();
        float height = (collider.bounds.size.y / 2) + 0.5f;
        Vector3 healtBarPosition = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        GameObject healthBarGO = Instantiate(healthBarPrefab, healtBarPosition, transform.rotation, transform);
        healthBar = healthBarGO.transform.GetComponent<HealthBar>();
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        if (healthBar) healthBar.SetHealth(_currentHealth / health);
        if (_currentHealth <= 0f) Die();
    }

    public void Die()
    {
        Destroy(transform.parent.gameObject);
    }
}
