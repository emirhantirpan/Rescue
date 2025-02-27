using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private bool _isPaused = false;

    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    private void Awake()
    {
        _panel.SetActive(false);
        _pauseButton.onClick.AddListener(PauseButton);
        _resumeButton.onClick.AddListener(ResumeButton);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isPaused = !_isPaused;

            if (_panel.activeInHierarchy)
            {
                _panel.SetActive(false);
                Time.timeScale = 1f; 
            }
            else
            {
                _panel.SetActive(true);
                Time.timeScale = 0f;
            }
            
        }
    }
    public void PauseButton()
    {
        _panel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeButton()
    {
        _panel.SetActive(false);
        Time.timeScale = 1f;
    }
}
