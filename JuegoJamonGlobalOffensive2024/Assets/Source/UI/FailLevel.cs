using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailLevel : MonoBehaviour
{
    [SerializeField]
    private Canvas _fadeCanvas;

    [SerializeField]
    private float _duration;

    [SerializeField]
    private int _mainMenuIndex;

    private bool _canLoad = false;

    // Start is called before the first frame update
    void Start()
    {
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
        if (_canLoad)
        {
            SceneManager.LoadScene(_mainMenuIndex);
        }
    }
}
