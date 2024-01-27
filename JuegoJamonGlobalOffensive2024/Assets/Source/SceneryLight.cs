using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneryLight : MonoBehaviour, ISceneryElement
{
    [SerializeField] private Transform _leftBoundary;
    [SerializeField] private Transform _rightBoundary;
    [SerializeField] private float _baseSpeed;

    private float _movementSpeed;
    private LightMovementInformation _movementInformation;
    private Vector3 _direction;

    public void ReceiveInformation(SceneElementInformation info)
    {
        //throw new System.NotImplementedException();
        _movementInformation = (LightMovementInformation)info;
        _direction = Vector3.right * _movementInformation.Direction;
    }

    // Start is called before the first frame update
    void Start()
    {
        _movementSpeed = _baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + _direction, _movementSpeed * Time.deltaTime);
        this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, _leftBoundary.position.x, _rightBoundary.position.x), this.transform.position.y, this.transform.position.z);
    }
}
