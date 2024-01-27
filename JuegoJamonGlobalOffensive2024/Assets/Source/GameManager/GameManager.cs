using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //private bool _created = false;

    private float _life;

    public static event Action<float> Exit;
    public static event Action<float> Laugh;

    [SerializeField]
    private static Stack<LevelConfiguration> _levelConfigurations;

    [SerializeField]
    private static int _winSceneIndex, _failSceneIndex;

    [SerializeField]
    private Canvas _fadeToBlackCanvas;
    [SerializeField]
    private float _fadeDuration;

    private LineManager _lineManager;
    private AudienceManager _audienceManager;
    private PanelControl _panelControl;
    private SceneryManager _sceneryManager;

    private void Awake()
    {
        //if(!_created)
        //{
        //    DontDestroyOnLoad(this.gameObject);
        //    _created = true;
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        InitLevel();
        _fadeToBlackCanvas.GetComponent<FadeToBlack>().ActivateFade(true, _fadeDuration, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(_life <= 0)
        {
            FinishLevelFail();
        }
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

    public void PlayerSuccess()
    {
        Laugh?.Invoke(UnityEngine.Random.Range((float)0.7, 1));
    }

    public void LoadLevelConfig(LevelConfiguration config)
    {       
            _lineManager.GenerateLines(config.GetGeneratedLinesAmmount(), config.GetAllowedLineTypes(), config.GetAllowedSecondComedian());
            _lineManager.AddScriptedLines(config.GetScriptedLines());
            _panelControl.Init(config.GetC2Active(), config.GetDrumsActive(), config.GetLightsActive());
            // SCENARY MANAGER INITIALIZE
            _audienceManager.GenerateAudience(config.GetAudienceMembersCount());
    }

    //public void InitNextLevel()
    //{
    //    _lineManager = GameObject.Find("LineManager").GetComponent<LineManager>();
    //    _audienceManager = GameObject.Find("AudienceManager").GetComponent<AudienceManager>();
    //    _panelControl = GameObject.Find("PanelControl").GetComponent<PanelControl>();
    //    // _sceneryManager = GameObject.Find("ScenaryManager").GetComponent<ScenaryManager>();
    //    if (_levelConfigurations.Count != 0)
    //    {
    //        LevelConfiguration config = _levelConfigurations.Pop();
    //        LoadLevelConfig(config);
    //    }
    //}

    public void InitLevel()
    {
        do
        {
            _lineManager = GameObject.Find("LineManager").GetComponent<LineManager>();
        } 
        while (_lineManager == null);

        do
        { 
            _audienceManager = GameObject.Find("AudienceManager").GetComponent<AudienceManager>();
        }
        while (_audienceManager == null);

        do
        {
            _panelControl = GameObject.Find("PanelControl").GetComponent<PanelControl>();
        }
        while (_panelControl == null);

        do
        {
            _sceneryManager = GameObject.Find("ScenaryManager").GetComponent<SceneryManager>();
        }
        while (_sceneryManager == null);

        if (_levelConfigurations.Count != 0)
        {
            LevelConfiguration config = _levelConfigurations.Pop();
            LoadLevelConfig(config);
        }
    }

    public void FinishLevelWin()
    {
        _fadeToBlackCanvas.GetComponent<FadeToBlack>().ActivateFade(true, _fadeDuration, 1);
        StartCoroutine(WaitCoroutine(_fadeDuration));
        SceneManager.LoadScene(_winSceneIndex);
    }

    public void FinishLevelFail()
    {
        _fadeToBlackCanvas.GetComponent<FadeToBlack>().ActivateFade(true, _fadeDuration, 1);
        StartCoroutine(WaitCoroutine(_fadeDuration));
        SceneManager.LoadScene(_failSceneIndex);
    }

    IEnumerator WaitCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
