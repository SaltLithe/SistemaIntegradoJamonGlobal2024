using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comedian : MonoBehaviour, ISceneryElement
{
    [SerializeField] private List<Transform> _positions;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Animation _movingAnimation;

    private int _currentPosition;
    private int _targetPosition;
    private bool _moving;


    private void Start()
    {
        _moving = false;
        _currentPosition = 0;
        _targetPosition = 0;
    }

    public void ReceiveInformation(SceneElementInformation info)
    {
        _targetPosition = Random.Range(0, _positions.Count);

        if (_targetPosition == _currentPosition)
        {
            //Do some bait
            var baitPos = _currentPosition++ % _positions.Count;

            StartCoroutine(BaitAndReturn(baitPos));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentPosition != _targetPosition)
        {
            _moving = true;
            transform.position = Vector3.MoveTowards(transform.position, _positions[_targetPosition].position, _movementSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _positions[_targetPosition].position) == 0)
            {
                _currentPosition = _targetPosition;
                _moving = false;
            }
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

    IEnumerator BaitAndReturn(int baitPos)
    {
        _targetPosition = baitPos;

        yield return new WaitForSeconds(1f);

        _targetPosition = _currentPosition;
        _currentPosition = baitPos;
    }

    public float GetMovementSpeed()
    {
        return _movementSpeed;
    }
}
