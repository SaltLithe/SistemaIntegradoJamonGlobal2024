using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryLight : MonoBehaviour, ISceneryElement
{
    [SerializeField] private Transform _initialPosition;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private float _movementTime;


    public void ReceiveInformation(SceneElementInformation info)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
