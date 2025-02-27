using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Instructions : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _pressE;
    [SerializeField] private TMP_Text _dontHave;
    private void Start()
    {
        _dontHave.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (_player.score != 100)
        {
            _dontHave.gameObject.SetActive(true);
        }
        else
        {
            _dontHave.gameObject.SetActive(false);
        }
    } 
}
