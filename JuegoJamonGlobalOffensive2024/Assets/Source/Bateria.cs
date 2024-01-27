using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bateria : MonoBehaviour, ISceneryElement
{
    [SerializeField] private Animator _animator;

    public void ReceiveInformation(SceneElementInformation info)
    {
        _animator.SetTrigger("Drums");
    }
}
