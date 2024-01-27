using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    public static event Action<PlayerAction, SceneElementInformation> OnPlayerAction;

    [Header("References")]
    [SerializeField] private LineManager _lineManager;

    [SerializeField] private Button _parafernaliaButton;
    [SerializeField] private Toggle _microfonoButtonP1;
    [SerializeField] private Toggle _microfonoButtonP2;
    [SerializeField] private Button _bateriaButton;
    [SerializeField] private Toggle _lightLeftP1;
    [SerializeField] private Toggle _lightRightP1;
    [SerializeField] private Toggle _lightStopP1;
    [SerializeField] private Toggle _lightLeftP2;
    [SerializeField] private Toggle _lightRightP2;
    [SerializeField] private Toggle _lightStopP2;

    [SerializeField] private GameObject _lineUIPrefabP1;
    [SerializeField] private GameObject _lineUIPrefabP2;
    [SerializeField] private Transform _telePrompterLinesParent;

    [Header("Info")]
    [SerializeField] bool _mic1Activated = false;
    [SerializeField] bool _mic2Activated = false;


    private void Awake()
    {
        _microfonoButtonP1.onValueChanged.RemoveAllListeners();
        _microfonoButtonP2.onValueChanged.RemoveAllListeners();
        _bateriaButton.onClick.RemoveAllListeners();
        _lightLeftP1.onValueChanged.RemoveAllListeners();
        _lightRightP1.onValueChanged.RemoveAllListeners();
        _lightStopP1.onValueChanged.RemoveAllListeners();
        _lightLeftP2.onValueChanged.RemoveAllListeners();
        _lightRightP2.onValueChanged.RemoveAllListeners();
        _lightStopP2.onValueChanged.RemoveAllListeners();

        _microfonoButtonP1.onValueChanged.AddListener(PressMicrofonoP1);
        _microfonoButtonP2.onValueChanged.AddListener(PressMicrofonoP2);
        _bateriaButton.onClick.AddListener(PressBateria);
        _lightLeftP1.onValueChanged.AddListener((bool value) => { if (value) MoveLightP1(-1); });
        _lightRightP1.onValueChanged.AddListener((bool value) => { if (value) MoveLightP1(1); });
        _lightStopP1.onValueChanged.AddListener((bool value) => { if (value) MoveLightP1(0); });
        _lightLeftP2.onValueChanged.AddListener((bool value) => { if (value) MoveLightP2(-1); });
        _lightRightP2.onValueChanged.AddListener((bool value) => { if (value) MoveLightP2(1); });
        _lightStopP2.onValueChanged.AddListener((bool value) => { if (value) MoveLightP2(0); });
    }

    private void Start()
    {
        _lineManager.GenerateLines(20, new List<E_LineType>() { E_LineType.NO_EVENT, E_LineType.MUTE, E_LineType.DRUMS}, false);
        GetLines();
    }

    private void MoveLightP2(int direction)
    {
        OnPlayerAction?.Invoke(PlayerAction.P2MoveLight, new LightMovementInformation(direction));
    }

    private void MoveLightP1(int direction)
    {
        OnPlayerAction?.Invoke(PlayerAction.P1MoveLight, new LightMovementInformation(direction));
    }

    private void PressMicrofonoP1(bool value)
    {
        _mic1Activated = !_mic1Activated;
        OnPlayerAction?.Invoke(PlayerAction.P1Microphone, null);
        Debug.Log("Microphone UI pressed");
    }

    private void PressMicrofonoP2(bool value)
    {
        _mic2Activated = !_mic2Activated;
        OnPlayerAction?.Invoke(PlayerAction.P2Microphone, null);
        Debug.Log("Microphone UI pressed");
    }

    private void PressBateria()
    {
        if (_currentLine.GetLineType() == E_LineType.DRUMS)
        {
            if (!(_currentLineCounter > _currentLine.GetEventStamp() - .2f &&
                _currentLineCounter < _currentLine.GetEventStamp() + .2f))
            {
                Debug.Log("Battery pressed succesfully");
            }
            else
            {
                Error();
            }
        }

        OnPlayerAction?.Invoke(PlayerAction.Battery, null);
        Debug.Log("Battery UI pressed");
    }

    private void GetLines()
    {
        //GetLines
        ShowLines(_lineManager.GetLines(3));
    }

    public void ShowLines(IEnumerable<Line> lines)
    {
        Debug.Log("Show Lines :)");

        //Destroy Children
        for (int i = 0; i < _telePrompterLinesParent.childCount; i++)
        {
            Destroy(_telePrompterLinesParent.GetChild(i).gameObject);
        }

        if (lines.Count() == 0) 
        {
            Debug.Log("Finish Level");
            return;
        }

        //Instantiate Lines
        var aux = 0;
        foreach (Line line in lines)
        {
            var lineUI = Instantiate(_lineUIPrefabP1, _telePrompterLinesParent);

            if (line.GetLineType() == E_LineType.MUTE)
            {
                var aux2 = 0;
                foreach (var img in lineUI.GetComponentsInChildren<Image>())
                {
                    if (line.IsComedian1())
                    {
                        img.color = aux2 > 0 ? Color.red : Color.black;
                    }
                    else
                    {
                        img.color = aux2 > 0 ? Color.blue : Color.black;
                    }

                    aux2++;
                }
            }

            if (aux > 0) lineUI.transform.localScale *= .9f;
            else
            {
                _currentLineSlider = lineUI.transform.GetChild(0).GetComponent<Slider>();
            }
            aux++;
        }

        //Reset Line Control
        _currentLine = lines.First();
        _currentLineMaxTime = _currentLine.GetDuration();
        _currentLineCounter = 0;
        _currentLinePercentage = 0;
        _micChecked = false;

        //Activate Line Control
        _lineControlEnabled = true;
    }

    private bool _lineControlEnabled = false;
    private Line _currentLine;
    private Slider _currentLineSlider;
    
    double _currentLineMaxTime = 0;
    double _currentLineCounter = 0;
    double _currentLinePercentage = 0;

    // Mic Control
    double _initMicChangePercentaje = .1f;
    double _endMicChangePercentaje = .9f;
    bool _micChecked = false;

    private void Update()
    {
        if (!_lineControlEnabled) return;

        _currentLineCounter += Time.deltaTime;
        _currentLinePercentage = _currentLineCounter / _currentLineMaxTime;

        _currentLineSlider.value = (float)_currentLinePercentage;


        if (_currentLinePercentage > _initMicChangePercentaje &&
            _currentLinePercentage < _endMicChangePercentaje &&
            !_micChecked)
        {
            _micChecked = true;
            
            //Check for error
            if (_currentLine.GetLineType() == E_LineType.MUTE)
            {
                if (_currentLine.IsComedian1() ? _mic1Activated : _mic2Activated) Error();
            }
            else
            {
                if (_currentLine.IsComedian1() ? !_mic1Activated : !_mic2Activated) Error();
            }
        }

        if (_currentLinePercentage >= 1) // Get New Lines
        {
            _lineControlEnabled = false;
            GetLines();
        }
    }


    private void Error()
    {
        Debug.Log("Player Mistake");
    }
}