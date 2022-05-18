using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float _updateRate = 0.02f;
    private float _updateDelay;
    private Image _healthBarImage;

    private void Start()
    {
        _updateDelay = _updateRate;
        _healthBarImage = transform.GetChild(1).GetComponent<Image>();
    }

    private void Update()
    {
        Utilities.UpdateTimer(true, ref _updateDelay, _updateRate, UpdateHealthBarRotation);
    }

    private void UpdateHealthBarRotation()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back,
            Camera.main.transform.rotation * Vector3.down);
    }

    public void SetHealth(float value)
    {
        _healthBarImage.fillAmount = value;
    }
}
