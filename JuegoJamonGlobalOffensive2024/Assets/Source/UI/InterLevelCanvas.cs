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
        _image.sprite = _sprites[_currentSprite];
        _currentSprite++;
        _fadeCanvas.GetComponent<FadeToBlack>().ActivateFade(true, _duration, 0);
        StartCoroutine(WaitCoroutine(_duration));
        _canLoad = true;
    }

    IEnumerator WaitCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public void NextButtonPressed()
    {
        if (_canLoad && _currentSprite < _sprites.Length) 
        {
            SceneManager.LoadScene(_gameSceneIndex);
        }
        else if(_currentSprite >= _sprites.Length)
        {
            SceneManager.LoadScene(_mainMenuIndex);
        }
    }
}
