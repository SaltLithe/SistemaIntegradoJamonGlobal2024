using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comedian : MonoBehaviour, ISceneryElement
{
    [SerializeField] private Transform _initialPosition;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private float _movementTime;
    [SerializeField] private Animation _movingAnimation;

    private Transform _targetPosition;
    private float _movementSpeed;
    private bool _isAtInitialPosition;
    private bool _moving;


    private void Start()
    {
        _isAtInitialPosition = true;
        _moving = false;
        _targetPosition = _initialPosition;
        _movementTime = Vector3.Distance(_initialPosition.position, _endPosition.position) / _movementTime;
    }

    public void ReceiveInformation(SceneElementInformation info)
    {
        _targetPosition = _isAtInitialPosition ? _endPosition : _initialPosition;
        _isAtInitialPosition = !_isAtInitialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _targetPosition.position) > 0) 
        {
            _moving = true;
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition.position, _movementSpeed * Time.deltaTime);
        }else
        {
            _moving = false;
        }


        if (_moving)
        {
            _movingAnimation.Play();
        }
        else
        {
            _movingAnimation.Stop();
        }
    }
}
