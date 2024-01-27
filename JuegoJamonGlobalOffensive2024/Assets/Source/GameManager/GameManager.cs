using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private float _life;

    public static event Action<float> Exit;

    [SerializeField]
    private Stack<LevelConfiguration> _levelConfigurations;

    [SerializeField]
    private LevelConfiguration _postGame;

    private LineManager _lineManager;
    private AudienceManager _audienceManager;
    private PanelControl _panelControl;
    //private ScenaryManager _scenaryManager;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        _lineManager = GameObject.Find("LineManager").GetComponent<LineManager>();
        _audienceManager =  GameObject.Find("AudienceManager").GetComponent<AudienceManager>();
        _panelControl = GameObject.Find("PanelControl").GetComponent<PanelControl>();
        // _scenaryManager = GameObject.Find("ScenaryManager").GetComponent<ScenaryManager>();
        LoadNextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerError(float percentage)
    {
        _life -= percentage;
        if (_life < 0)
        {
            _life = 0;
        }
        Exit?.Invoke(_life);
    }

    public void LoadNextLevel()
    {
        if(_levelConfigurations.Count != 0)
        {
            LevelConfiguration config = _levelConfigurations.Pop();
            _lineManager.GenerateLines(config.GetGeneratedLinesAmmount(), config.GetAllowedLineTypes(), config.GetAllowedSecondComedian());
            _lineManager.AddScriptedLines(config.GetScriptedLines());
            //_panelControl.Init(config.GetC2Active(), config.GetDrumsActive(), config.GetLightsActive());
            // SCENARY MANAGER INITIALIZE
            // AUDIENCE MANAGER INITIALIZE
        }
    }
}
