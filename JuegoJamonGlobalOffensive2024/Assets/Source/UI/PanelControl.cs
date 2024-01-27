using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    public static event Action<PlayerAction> OnPlayerAction;

    [Header("References")]
    [SerializeField] private Button _parafernaliaButton;
    [SerializeField] private Button _microfonoButtonP1;
    [SerializeField] private Button _microfonoButtonP2;
    [SerializeField] private Button _bateriaButtonP1;
    [SerializeField] private Button _bateriaButtonP2;
    [SerializeField] private Button _lightLeftP1;
    [SerializeField] private Button _lightRightP1;
    [SerializeField] private Button _lightStopP1;
    [SerializeField] private Button _lightLeftP2;
    [SerializeField] private Button _lightRightP2;
    [SerializeField] private Button _lightStopP2;

    [SerializeField] private GameObject _lineUIPrefabP1;
    [SerializeField] private GameObject _lineUIPrefabP2;
    [SerializeField] private Transform _telePrompterLinesParent;

    [Header("Info")]
    [SerializeField] bool _micActivated = false;


    private void Awake()
    {
        _microfonoButtonP1.onClick.RemoveAllListeners();
        _microfonoButtonP2.onClick.RemoveAllListeners();
        _bateriaButtonP1.onClick.RemoveAllListeners();
        _bateriaButtonP2.onClick.RemoveAllListeners();

        _microfonoButtonP1.onClick.AddListener(PressMicrofonoP1);
        _microfonoButtonP2.onClick.AddListener(PressMicrofonoP2);
        _bateriaButtonP1.onClick.AddListener(PressBateriaP1);
        _bateriaButtonP2.onClick.AddListener(PressBateriaP2);
    }

    private void PressMicrofonoP1()
    {
        OnPlayerAction?.Invoke(PlayerAction.Microphone);
        Debug.Log("Microphone UI pressed");
    }

    private void PressMicrofonoP2()
    {
        OnPlayerAction?.Invoke(PlayerAction.Microphone);
        Debug.Log("Microphone UI pressed");
    }

    private void PressBateriaP1()
    {
        OnPlayerAction?.Invoke(PlayerAction.Battery);
        Debug.Log("Battery UI pressed");
    }
    private void PressBateriaP2()
    {
        OnPlayerAction?.Invoke(PlayerAction.Battery);
        Debug.Log("Battery UI pressed");
    }

    public void ShowLines(IEnumerable<Line> lines)
    {
        Debug.Log("Show Lines :)");

        //Destroy Children
        for (int i = 0; i < _telePrompterLinesParent.childCount; i++)
        {
            Destroy(_telePrompterLinesParent.GetChild(0).gameObject);
        }

        //Instantiate Lines
        var aux = 0;
        foreach (Line line in lines)
        {
            var lineUI = Instantiate(_lineUIPrefabP1, _telePrompterLinesParent);

            if (aux > 0) lineUI.transform.localScale *= .9f;
            aux++;
        }

        //Activate Line Control
    }

    private bool _lineControlEnabled = false;
    private Line _currentLine;
    
    float _currentLineMaxTime = 0;
    float _currentLineCounter = 0;
    float _currentLinePercentage = 0;

    // Mic Control
    float _initMicChangePercentaje = .1f;
    float _endMicChangePercentaje = .9f;
    bool _micChecked = false;

    private void Update()
    {
        if (!_lineControlEnabled) return;

        _currentLineCounter += Time.deltaTime;
        _currentLinePercentage = _currentLineCounter / _currentLineMaxTime;

        if (_currentLinePercentage > _initMicChangePercentaje &&
            _currentLinePercentage < _endMicChangePercentaje &&
            !_micChecked)
        {
            _micChecked = true;
            
            //Check for error
            if (_currentLine.GetLineType() == E_LineType.MUTE)
            {
                if (_micActivated) Error();
            }
            else
            {
                if (!_micActivated) Error();
            }
        }

        if (_currentLinePercentage >= 1)
        {
            _lineControlEnabled = false;
        }
    }


    private void Error()
    {
        Debug.Log("Player Mistake");
    }
}