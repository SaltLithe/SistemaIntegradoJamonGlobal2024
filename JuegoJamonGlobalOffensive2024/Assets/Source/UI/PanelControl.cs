using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

    [SerializeField] private GameObject _lightC1ON;
    [SerializeField] private GameObject _lightC1OFF;
    [SerializeField] private GameObject _lightC2ON;
    [SerializeField] private GameObject _lightC2OFF;
    [SerializeField] private GameObject _lineUIPrefabP1;
    [SerializeField] private GameObject _lineUIPrefabP2;
    [SerializeField] private Transform _telePrompterLinesParent;
    [SerializeField] private Transform _canvasParent;
    [SerializeField] private TMP_Text _playerInteresttxt;

    [SerializeField] private float _errorParafernalia;
    [SerializeField] private float _errorMicrofono;
    [SerializeField] private float _errorBateria;

    [Header("Info")]
    [SerializeField] bool _showActive = false;
    [SerializeField] bool _mic1Activated = false;
    [SerializeField] bool _mic2Activated = false;


    private void Awake()
    {
        _parafernaliaButton.onClick.RemoveAllListeners();
        _microfonoButtonP1.onValueChanged.RemoveAllListeners();
        _microfonoButtonP2.onValueChanged.RemoveAllListeners();
        _bateriaButton.onClick.RemoveAllListeners();
        _lightLeftP1.onValueChanged.RemoveAllListeners();
        _lightRightP1.onValueChanged.RemoveAllListeners();
        _lightStopP1.onValueChanged.RemoveAllListeners();
        _lightLeftP2.onValueChanged.RemoveAllListeners();
        _lightRightP2.onValueChanged.RemoveAllListeners();
        _lightStopP2.onValueChanged.RemoveAllListeners();

        GameManager.Exit += UpdatePlayerInterest;
    }

    private void UpdatePlayerInterest(float obj)
    {
        _playerInteresttxt.text = $"{obj * 100} %";
    }

    private void ParafernaliaPressed()
    {
        _showActive = true;
        OnPlayerAction?.Invoke(PlayerAction.Parafernalia, null);
        AudioManager.Instance.PlayRedButton();
    }

    public void Init(bool c2Active, bool bateriaActive, bool lightsActive)
    {
        _playerInteresttxt.text = "100 %";
        _parafernaliaButton.onClick.AddListener(ParafernaliaPressed);
        _microfonoButtonP1.onValueChanged.AddListener(PressMicrofonoP1);
        if (c2Active) _microfonoButtonP2.onValueChanged.AddListener(PressMicrofonoP2);
        if (bateriaActive) _bateriaButton.onClick.AddListener(PressBateria);
        if (lightsActive) _lightLeftP1.onValueChanged.AddListener((bool value) => { if (value) MoveLightP1(-1); });
        if (lightsActive) _lightRightP1.onValueChanged.AddListener((bool value) => { if (value) MoveLightP1(1); });
        if (lightsActive) _lightStopP1.onValueChanged.AddListener((bool value) => { if (value) MoveLightP1(0); });
        if (c2Active) _lightLeftP2.onValueChanged.AddListener((bool value) => { if (value) MoveLightP2(-1); });
        if (c2Active) _lightRightP2.onValueChanged.AddListener((bool value) => { if (value) MoveLightP2(1); });
        if (c2Active) _lightStopP2.onValueChanged.AddListener((bool value) => { if (value) MoveLightP2(0); });


        if (!c2Active)
        {
            _microfonoButtonP2.interactable = false;
            _lightLeftP2.interactable = false;
            _lightStopP2.interactable = false;
            _lightRightP2.interactable = false;
            _lightLeftP2.interactable = false;
        }

        if (!bateriaActive)
        {
            _bateriaButton.interactable = false;
        }

        if (!lightsActive)
        {
            _lightLeftP1.interactable = false;
            _lightStopP1.interactable = false;
            _lightRightP1.interactable = false;
        }

        GetLines();
    }

    private void MoveLightP2(int direction)
    {
        OnPlayerAction?.Invoke(PlayerAction.C2MoveLight, new LightMovementInformation(direction));
        AudioManager.Instance.PlayClickNeutral();
    }

    private void MoveLightP1(int direction)
    {
        OnPlayerAction?.Invoke(PlayerAction.C1MoveLight, new LightMovementInformation(direction));
        AudioManager.Instance.PlayClickNeutral();
    }

    private void PressMicrofonoP1(bool value)
    {
        if (_mic1Activated)
        {
            AudioManager.Instance.PlayClickOFF();
            _lightC1OFF.SetActive(true);
            _lightC1ON.SetActive(false);
        }
        else
        {
            AudioManager.Instance.PlayClickON();
            _lightC1ON.SetActive(true);
            _lightC1OFF.SetActive(false);
        }

        _mic1Activated = !_mic1Activated;

        OnPlayerAction?.Invoke(PlayerAction.C1Microphone, null);
        Debug.Log("Microphone UI pressed");
    }

    private void PressMicrofonoP2(bool value)
    {
        if (_mic2Activated)
        {
            AudioManager.Instance.PlayClickOFF();
            _lightC2OFF.SetActive(true);
            _lightC2ON.SetActive(false);
        }
        else
        {
            AudioManager.Instance.PlayClickON();
            _lightC2ON.SetActive(true);
            _lightC2OFF.SetActive(false);
        }

        _mic2Activated = !_mic2Activated;
        OnPlayerAction?.Invoke(PlayerAction.C2Microphone, null);
        Debug.Log("Microphone UI pressed");
    }

    private void PressBateria()
    {
        if(!_showActive) 
        {
            return;
        }
        if (_currentLine.GetLineType() == E_LineType.DRUMS)
        {
            if ((_currentLineCounter > _currentLine.GetEventStamp() - .5f &&
                _currentLineCounter < _currentLine.GetEventStamp() + .5f))
            {
                Debug.Log("Battery pressed succesfully");
                Success();
                _batteryPressedInTime = true;
            }
            else
            {
                Debug.Log("Battery pressed succesfullyn't");
                Error(_errorBateria);
            }
        }
        else
        {
            Debug.Log("Battery pressed succesfullyn't");
            Error(_errorBateria);
        }

        OnPlayerAction?.Invoke(PlayerAction.Battery, null);
        AudioManager.Instance.PlayDrums();
    }

    private void GetLines()
    {
        //GetLines
        ShowLines(_lineManager.GetLines(10));
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
            GameManager.Instance.FinishLevelWin();
            return;
        }

        //Instantiate Lines
        var aux = 0;
        foreach (Line line in lines)
        {
            var lineUI = Instantiate(line.IsComedian1() ? _lineUIPrefabP1 : _lineUIPrefabP2, _telePrompterLinesParent);

            if (line.GetLineType() == E_LineType.MUTE)
            {
                var aux2 = 0;
                foreach (var img in lineUI.GetComponentsInChildren<Image>())
                {
                    if (line.IsComedian1())
                    {
                        img.color = aux2 > 0 ? img.color : Color.yellow;
                    }
                    else
                    {
                        img.color = aux2 > 0 ? img.color : Color.magenta;
                    }

                    aux2++;
                }
            }

            if (line.GetLineType() == E_LineType.DRUMS)
            {
                var positionPercentaje = line.GetEventStamp() / line.GetDuration();
                var sliderBat = lineUI.transform.GetChild(1).GetComponent<Slider>();
                sliderBat.gameObject.SetActive(true);
                sliderBat.SetValueWithoutNotify((float)positionPercentaje);
            }

            if (aux == 0)
            {
                _currentLineSlider = lineUI.transform.GetChild(0).GetComponent<Slider>();
            }
            else
            {
                lineUI.transform.localScale *= 1f + 0.2f * aux;
                lineUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().color -= new Color(.1f * aux, .1f * aux, .1f * aux, 0);
            }
            aux++;
        }

        //Reset Line Control
        _currentLine = lines.First();
        
        if (_showActive)
        {
            AudioManager.Instance.StopVoice();
            if (_mic1Activated && _currentLine.IsComedian1())
            {
                AudioManager.Instance.PlayVoice(Character.Cass, UnityEngine.Random.Range(0, 7));
            }
            else if (_mic2Activated && !_currentLine.IsComedian1())
            {
                AudioManager.Instance.PlayVoice(Character.Delilah, UnityEngine.Random.Range(0, 7));
            }
        }
        _currentLineMaxTime = _currentLine.GetDuration();
        _currentLineCounter = 0;
        _currentLinePercentage = 0;
        _micChecked = false;
        _batteryPressedInTime = false;

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
    double _initMicChangePercentaje = .3f;
    double _endMicChangePercentaje = .7f;
    bool _micChecked = false;

    // Battery Control
    bool _batteryPressedInTime = false;

    private void Update()
    {
        if (!_showActive) return;
        if (!_lineControlEnabled) return;

        _currentLineCounter += Time.deltaTime;
        _currentLinePercentage = _currentLineCounter / _currentLineMaxTime;

        _currentLineSlider.value = (float)_currentLinePercentage;

        //Check Mic
        CheckMic();

        //CheckBattery
        CheckBattery();

        if (_currentLinePercentage >= 1) // Get New Lines
        {
            _lineControlEnabled = false;
            GetLines();
        }
    }

    private void CheckMic()
    {
        if (_currentLinePercentage > _initMicChangePercentaje &&
            _currentLinePercentage < _endMicChangePercentaje &&
            !_micChecked)
        {
            _micChecked = true;

            //Check for error
            if (_currentLine.GetLineType() == E_LineType.MUTE)
            {
                if (_currentLine.IsComedian1() ? _mic1Activated : _mic2Activated) Error(_errorMicrofono);
            }
            else
            {
                if (_currentLine.IsComedian1() ? !_mic1Activated : !_mic2Activated) Error(_errorMicrofono);
            }
        }
    }

    private void CheckBattery()
    {
        if (_currentLinePercentage < 1) return;
        if (_currentLine.GetLineType() != E_LineType.DRUMS) return;

        if (!_batteryPressedInTime) Error(_errorBateria);
    }


    private void Error(float errorAmmount)
    {
        Debug.Log("Player Mistake");
        GameManager.Instance.PlayerError(errorAmmount);
    }

    private void Success()
    {
        Debug.Log("Player Success");
        GameManager.Instance.PlayerSuccess();
    }
}