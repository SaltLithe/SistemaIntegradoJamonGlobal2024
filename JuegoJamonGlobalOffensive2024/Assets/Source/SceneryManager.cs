using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SceneryManager : MonoBehaviour
{
    [SerializeField] Parafernalia _parafernaliaObject;
    [SerializeField] Bateria _batteryObject;
    [SerializeField] SceneryLight _lightC1Object;
    [SerializeField] SceneryLight _lightC2Object;
    [SerializeField] Comedian _c1Object;
    [SerializeField] Comedian _c2Object;


    private bool _canUseC1;
    private bool _canUseC2;

    private void Awake()
    {
        PanelControl.OnPlayerAction += ManagePanelControlAction;
    }

    public void Init(bool canUseC1, bool canUseC2)
    {
        _canUseC1 = canUseC1;
        _canUseC2 = canUseC2;

        StopAllCoroutines();
        StartCoroutine(TriggerRandomSceneEvents());
    }

    IEnumerator TriggerRandomSceneEvents()
    {
        while(gameObject.activeSelf && (_canUseC1 || _canUseC2))
        {
            yield return new WaitForSeconds(Random.Range(2f, 3f));

            if(Random.Range(0f, 1f) >= 0)
            {
                if (Random.Range(0f, 1f) >= .5f)
                {
                    if (_canUseC1)
                    {
                        _c1Object.ReceiveInformation(null);
                    }
                }
                else
                {
                    if (_canUseC2)
                    {
                        _c2Object.ReceiveInformation(null);
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        PanelControl.OnPlayerAction -= ManagePanelControlAction;
    }

    private void ManagePanelControlAction(PlayerAction action, SceneElementInformation information)
    {
        switch (action)
        {
            case PlayerAction.Parafernalia:
                if (_parafernaliaObject) _parafernaliaObject.ReceiveInformation(information);
                break;
            case PlayerAction.Battery:
                if (_batteryObject) _batteryObject.ReceiveInformation(information);
                break;
            case PlayerAction.C1MoveLight:
                if (_lightC1Object) _lightC1Object.ReceiveInformation(information);
                break;
            case PlayerAction.C2MoveLight:
                if (_lightC2Object) _lightC2Object.ReceiveInformation(information);
                break;
        }
    }
}
