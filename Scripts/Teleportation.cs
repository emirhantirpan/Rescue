using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Teleportation : MonoBehaviour
{
    [SerializeField] private Button _teleportButon;
    [SerializeField] private Player _player;

    private void Start()
    {
    }
    private void Update()
    {
        if (_player.score == 100)
        {
            _teleportButon.onClick.AddListener(TeleportButton); 
        }
    }
    
    private void TeleportButton()
    {
        _player.isTeleport = true;
        Destroy(gameObject);
    }
}
