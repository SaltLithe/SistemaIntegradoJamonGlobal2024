using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //private bool _created = false;

    private float _life = 1;

    public static event Action<float> Exit;
    public static event Action<float> Laugh;

    public static int _currentLevel = 0;

    [SerializeField]
    private LevelConfiguration[] _levelConfigurationArray;
    private static Stack<LevelConfiguration> _levelConfigurations;

    [SerializeField]
    private int _winSceneIndex, _winFinalSceneIndex, _failSceneKass, _failSceneKassDel;

    [SerializeField]
    private Canvas _fadeToBlackCanvas;
    [SerializeField]
    private float _fadeDuration;

    private LineManager _lineManager;
    private AudienceManager _audienceManager;
    private PanelControl _panelControl;
    private SceneryManager _sceneryManager;

    public static GameManager Instance;

    private void Awake()
    {
        //if(!_created)
        //{
        //    DontDestroyOnLoad(this.gameObject);
        //    _created = true;
        //}
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitLevel();
        _fadeToBlackCanvas.GetComponent<FadeToBlack>().ActivateFade(true, _fadeDuration, 0);
        AudioManager.Instance.StartMonologueMusic();
        //AudioManager.Instance.PlayWhispers();
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
        Debug.LogWarning($"Current Life {_life}");

        Exit?.Invoke(_life);
    }

    public void PlayerSuccess()
    {
        Laugh?.Invoke(UnityEngine.Random.Range((float)0.7, 1));
    }

    public void LoadLevelConfig(LevelConfiguration config)
    {       
            _lineManager.GenerateLines(config.GetGeneratedLinesAmmount(), config.GetAllowedLineTypes(), config.GetC2Active());
            _lineManager.AddScriptedLines(config.GetScriptedLines());
            _panelControl.Init(config.GetC2Active(), config.GetDrumsActive(), config.GetLightsActive());
            _sceneryManager.Init(config.GetLightsActive(), config.GetC2Active());
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
            _lineManager = FindObjectOfType<LineManager>();
        } 
        while (_lineManager == null);

        do
        {
            _audienceManager = GameObject.Find("AudienceManager").GetComponent<AudienceManager>();
        }
        while (_audienceManager == null);

        do
        {
            _panelControl = FindObjectOfType<PanelControl>();
        }
        while (_panelControl == null);

        do
        {
            _sceneryManager = FindObjectOfType<SceneryManager>();
        }
        while (_sceneryManager == null);

        if (_levelConfigurationArray.Length != 0 && _currentLevel < _levelConfigurationArray.Length)
        {
            LevelConfiguration config = _levelConfigurationArray[_currentLevel];
            LoadLevelConfig(config);
            _currentLevel++;
        }
    }

    public void FinishLevelWin()
    {
        _fadeToBlackCanvas.GetComponent<FadeToBlack>().ActivateFade(true, _fadeDuration, 1);
        StartCoroutine(WaitCoroutine(_fadeDuration));
        AudioManager.Instance.StopAll();
        if (_currentLevel == 3)
        {
            DialogueManager.loadMainMenu = true;
            SceneManager.LoadScene(_winFinalSceneIndex);
        }
        else
        {
            DialogueManager.loadMainMenu = false;
            SceneManager.LoadScene(_winSceneIndex);
        }
    }

    public void FinishLevelFail()
    {
        _fadeToBlackCanvas.GetComponent<FadeToBlack>().ActivateFade(true, _fadeDuration, 1);
        StartCoroutine(WaitCoroutine(_fadeDuration));
        AudioManager.Instance.StopAll();
        DialogueManager.loadMainMenu = true;
        if (_currentLevel == 2)
        {
            SceneManager.LoadScene(_failSceneKass);
        }
        else if (_currentLevel == 3)
        {
            SceneManager.LoadScene(_failSceneKassDel);
        }
    }

    IEnumerator WaitCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
