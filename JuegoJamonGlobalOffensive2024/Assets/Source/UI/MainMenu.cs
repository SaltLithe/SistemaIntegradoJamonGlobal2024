using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string _GameSceneName;
    [SerializeField] private Button _ButtonPlayGame;
    [SerializeField] private Button _ButtonExitGame;
    [SerializeField] private Button _ButtonCreditsGame;

    private void Awake()
    {
        _ButtonPlayGame.onClick.RemoveAllListeners();
        _ButtonPlayGame.onClick.AddListener(StartGame);
        _ButtonExitGame.onClick.AddListener(ExitGame);
        _ButtonCreditsGame.onClick.AddListener(Credits);
    }

    private void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        AudioManager.Instance.StartMenuMusic();
    }

    private void StartGame()
    {
        SceneManager.LoadScene(_GameSceneName);
    }
}
