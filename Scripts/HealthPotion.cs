using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void Start()
    {
        _player = GameObject.FindWithTag( "Player" ).GetComponent<Player>();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            IncreaseHealth();
        }
    }
    private void IncreaseHealth()
    {
        _player.currentHealth += 1000;
    }
    
}
