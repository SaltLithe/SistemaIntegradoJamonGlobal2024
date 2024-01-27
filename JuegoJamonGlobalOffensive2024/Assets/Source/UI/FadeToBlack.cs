using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{

    private bool _fading;
    private float _fadeDuration;
    private int _fadeDirection;
    private float _initTime;
    private Color _color;

    [SerializeField]
    private Image _image;

    private void Start()
    {
        _color = Color.black;
        _image.color = _color;
    }

    // Update is called once per frame
    void Update()
    {
        float timePassed = Time.time - _initTime;
        if (_fading)
        {
            _color.a = Mathf.MoveTowards(_color.a, _fadeDirection, _fadeDuration * Time.deltaTime);
            _image.color = _color;

            if (_color.a == _fadeDirection)
            {
                _fading = false;
            }

            //_color.a += Time.deltaTime/_fadeDuration * _fadeDirection;
            //if(timePassed >= _fadeDuration)
            //{
            //    _fading = false;
            //    if(_fadeDirection == 1) 
            //    { 
            //        _color.a = 1; 
            //    }
            //    else 
            //    {  
            //        _color.a = 0; 
            //    }
            //    _fadeDirection = 0;
            //}
        }

    }

    public void ActivateFade(bool fading, float duration, int direction)
    {
        this._fading = fading;
        this._fadeDuration = duration;
        this._fadeDirection = direction;
        this._initTime = Time.time;
    }
}
