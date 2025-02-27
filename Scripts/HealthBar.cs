using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float _minValue = 0;
    private float _maxValue = 5000;

    [SerializeField] private Image fillImage;
    [SerializeField] private Slider _slider;
    [SerializeField] private Player _player;

    void Awake()
    {

        _slider.value = 5000;
        _slider.maxValue = _maxValue;
        _slider.minValue = _minValue;
    }
    void Update()
    {
        if (_slider.value <= _slider.minValue)
        {
            fillImage.enabled = false;
        }
        if (_slider.value > _slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }
        _slider.value = _player.currentHealth;

        if (_slider.value <=5000 && _slider.value >= 3000)
        {
            fillImage.color = Color.green;
        }
        else if (_slider.value < 3000 && _slider.value >= 1000)
        {
            fillImage.color = Color.yellow;
        }
        else if (_slider.value < 1000 && _slider.value >= 0)
        {
            fillImage.color = Color.red;
        }

    }
}
