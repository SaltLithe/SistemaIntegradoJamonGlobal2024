using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [SerializeField] private Button returntoMenu;

    private void Start()
    {
        AudioManager.Instance.StartEndingMusic();
        returntoMenu.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
    }
}
