using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string _GameSceneName;
    [SerializeField] private Button _ButtonPlayGame;

    private void Awake()
    {
        _ButtonPlayGame.onClick.RemoveAllListeners();
        _ButtonPlayGame.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(_GameSceneName);
    }
}
