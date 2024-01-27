using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comedian : MonoBehaviour, ISceneryElement
{
    [SerializeField] private List<Transform> _positions;
    [SerializeField] private float _movementTime;
    [SerializeField] private Animation _movingAnimation;

    private int _currentPosition;
    private int _targetPosition;
    [SerializeField] private float _movementSpeed;
    private bool _moving;


    private void Start()
    {
        _moving = false;
        _currentPosition = 0;
        _targetPosition = 0;
        _movementSpeed = Vector3.Distance(_positions[_currentPosition].position, _positions[_targetPosition].position) / _movementTime;
    }

    public void ReceiveInformation(SceneElementInformation info)
    {
        _targetPosition = Random.Range(0, _positions.Count);

        if (_targetPosition == _currentPosition)
        {
            //Do some bait
        }
        else
        {
            _movementSpeed = Vector3.Distance(_positions[_currentPosition].position, _positions[_targetPosition].position) / _movementTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentPosition != _targetPosition)
        {
            _moving = true;
            transform.position = Vector3.MoveTowards(transform.position, _positions[_targetPosition].position, _movementSpeed * Time.deltaTime);
        }
        else
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
