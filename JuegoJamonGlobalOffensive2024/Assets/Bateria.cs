using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bateria : MonoBehaviour, ISceneryElement
{
    [SerializeField] private Animation _animation;

    public void ReceiveInformation(SceneElementInformation info)
    {
        _animation.Play();
    }
}
