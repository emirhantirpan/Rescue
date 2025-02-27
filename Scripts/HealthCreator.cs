using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCreator : MonoBehaviour
{
    private float _x;
    private float _y;
    private float _z;

    [SerializeField] private GameObject _healthPot;

    private void Start()
    {
        StartCoroutine(Create());
    }
    private Vector3 RandomPosition()
    {
        _x = Random.Range(1170, 1180);
        _y = 5;
        _z = Random.Range(1170, 1130);
        return new Vector3(_x, _y, _z);
    }
    private IEnumerator Create()
    {
        while (true)
        {
            
            GameObject anObject = Instantiate(_healthPot, RandomPosition(),transform.rotation);
            
            
            yield return new WaitForSeconds(10f);
        }
    }
}
