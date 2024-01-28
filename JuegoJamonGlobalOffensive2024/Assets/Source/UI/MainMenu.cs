using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string _GameSceneName;
    [SerializeField] private Button _ButtonPlayGame;
    [SerializeField] private Button _ButtonExitGame;

    private void Awake()
    {
        _ButtonPlayGame.onClick.RemoveAllListeners();
        _ButtonPlayGame.onClick.AddListener(StartGame);
        _ButtonExitGame.onClick.AddListener(ExitGame);
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
