using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _backButton;

    private void Start()
    {
        _mainPanel.SetActive(true);
        _settingsPanel.SetActive(false);

        _startButton.onClick.AddListener(StartButton);
        _settingsButton.onClick.AddListener(SettingsButton);
        _quitButton.onClick.AddListener(QuitButton);
        _backButton.onClick.AddListener(BackButton);
    }
    private void StartButton()
    {
        SceneManager.LoadScene("Level1");
    }
    private void SettingsButton()
    {
        _mainPanel.SetActive(false);
        _settingsPanel.SetActive(true);
    }
    private void QuitButton()
    {
        Application.Quit();
    }
    private void BackButton()
    {
        _settingsPanel.SetActive(false);
        _mainPanel.SetActive(true);
    }

}
