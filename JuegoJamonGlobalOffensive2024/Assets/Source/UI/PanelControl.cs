using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    public static event Action<PlayerAction> OnPlayerAction;

    [SerializeField] private Button _MicrofonoButton;
    [SerializeField] private Button _BateriaButton;

    private void Awake()
    {
        _MicrofonoButton.onClick.RemoveAllListeners();
        _BateriaButton.onClick.RemoveAllListeners();

        _MicrofonoButton.onClick.AddListener(PressMicrofono);
        _BateriaButton.onClick.AddListener(PressBateria);
    }

    private void PressMicrofono()
    {
        OnPlayerAction?.Invoke(PlayerAction.Microphone);
    }

    private void PressBateria()
    {
        OnPlayerAction?.Invoke(PlayerAction.Battery);
    }

    public void ShowLines(IEnumerable<Line> lines)
    {
        Debug.Log("Show Lines :)");
    }
}