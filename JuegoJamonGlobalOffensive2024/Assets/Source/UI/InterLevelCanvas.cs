using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class InterLevelCanvas : MonoBehaviour
{

    [SerializeField]
    private Canvas _fadeCanvas;

    [SerializeField]
    private float _duration;

    [SerializeField]
    private int _gameSceneIndex, _mainMenuIndex;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private Sprite[] _sprites;

    private static int _currentSprite = 0;

    private bool _canLoad = false;

    // Start is called before the first frame update
    void Start()
    {
        _fadeCanvas.GetComponent<FadeToBlack>().ActivateFade(true, _duration, 0);
        _canLoad = true;
        AudioManager.Instance.StopMusic();
    }
}
