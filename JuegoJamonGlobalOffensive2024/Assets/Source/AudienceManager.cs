using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AudienceManager : MonoBehaviour
{
    public GameObject _audienceMemberPrefab;
    public Transform _exitPoint; 
    public BoxCollider _spawnBounds;
    public BoxCollider _wallBack; 
    public int _audienceMembersCount = 20;
    private int _nextId = -1;
    private Dictionary<int, GameObject> _audienceMembers = new Dictionary<int, GameObject>();
    public System.Random rnd = new System.Random();
    public float _spawnVerticalOffset = 5; 

    private float lastHealthDEBUG = 1;
    private float _lastHealthUpdate = 1;
    private float _lastAccumulation = 0;

    public float _healthZeroMargin = 0.05f; 
    void Awake()
    {
        //GenerateAudience(_audienceMembersCount);
        GameManager.Exit += Exit;
        GameManager.Laugh += Laugh;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //REMOVE ON PRODUCTION
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            float random = (float)rnd.NextDouble();
            lastHealthDEBUG = random; 
            Laugh(random);

        }
        
        if(Input.GetKeyDown(KeyCode.S)) 
        {

            Exit(lastHealthDEBUG);
            lastHealthDEBUG = lastHealthDEBUG - 0.1f; 
        }
    }

    private void OnDestroy()
    {
        //TODO unsuscribe
        GameManager.Exit -= Exit;
        GameManager.Laugh -= Laugh;
    }

    //Salen tantos miembros del publico como pongas en el audienceMembersCount
    public void GenerateAudience(int audienceMembersCount) 
    {
        for (int i = 0; i< audienceMembersCount; i++) 
        {       
                Vector3 nextPosition = GenNextPosition();
                SpawnAudienceMember(nextPosition);
        }

        _wallBack.enabled = false; 
    }

    //Spawnea un miembro del publico dentro del volumen definido por el spawn
    private void SpawnAudienceMember(Vector3 nextPosition) 
    {
        GameObject newAudienceMember = Instantiate(_audienceMemberPrefab, nextPosition, Quaternion.identity);
        _nextId++;
        newAudienceMember.GetComponent<AudienceMember>().Initialize(_nextId, _exitPoint); 
        newAudienceMember.SetActive(true); 
        _audienceMembers.Add(_nextId, newAudienceMember);
    }

    //Genera una posición aleatoria para spawnear a cada miembro del publico , cuidado que colisionan entre ellos y si son muchos la liamos
    private Vector3 GenNextPosition() 
    {
        float offsetX = Random.Range(_spawnBounds.transform.position.x-(_spawnBounds.transform.localScale.x/2), _spawnBounds.transform.position.x + (_spawnBounds.transform.localScale.x / 2));
        float offsetZ = Random.Range(_spawnBounds.transform.position.y - (_spawnBounds.transform.localScale.y / 2), _spawnBounds.transform.position.y + (_spawnBounds.transform.localScale.y / 2));
        return new Vector3(offsetX,_spawnVerticalOffset, offsetZ);
    }

    //set on awake
    //GameManager.nombreEvento += Laugh
    //UNSUBSCRIBE ON ONDESTROY

    private List<int> GenerateRandomActions (int actionCount, List<int> keylist) 
    {
        List<int> randomActions = new List<int>(); 

        for (int i = 0; i < actionCount; i++) 
        {
            int nextkey = rnd.Next(0, keylist.Count);
            randomActions.Add(keylist[nextkey]);
            keylist.RemoveAt(nextkey);
        }

        return randomActions;
    }

    private void Laugh (float laughPercent) 
    {
        int laughCount = (int)Mathf.Floor(_audienceMembers.Count * laughPercent);
        List<int> randomLaughs = GenerateRandomActions(laughCount, _audienceMembers.Keys.ToList());

        if(laughPercent <= 0.8f) 
        {
            AudioManager.Instance.PlayLaugh(3);
        }
        else if (laughPercent <= 0.9f)
        {
            AudioManager.Instance.PlayLaugh(2);
        }
        else if(laughPercent <= 1f)
        {
            AudioManager.Instance.PlayLaugh(1);
        }

        foreach (int laughCandidate in randomLaughs) 
        {
            _audienceMembers[laughCandidate].GetComponent<AudienceMember>().Laugh(); 
        }
       
    }

    private void Exit (float currentHealh)
    {
        if (_audienceMembers.Count != 0 && currentHealh > _healthZeroMargin)
        {
            float exitPercentage = _lastHealthUpdate - currentHealh + _lastAccumulation;
            _lastHealthUpdate = currentHealh;
            int exitCount = (int)Mathf.Floor(_audienceMembers.Count * exitPercentage);
            List<int> randomExits = GenerateRandomActions(exitCount, _audienceMembers.Keys.ToList());

            foreach (int exitCandidate in randomExits)
            {
                var toDestroy = _audienceMembers[exitCandidate];
                _audienceMembers.Remove(exitCandidate);
                toDestroy.GetComponent<AudienceMember>().ExitAudience();
            }

            
        }
        else if (currentHealh <= _healthZeroMargin)
        {
            foreach (int exitCandidate in _audienceMembers.Keys.ToList())
            {
                var toDestroy = _audienceMembers[exitCandidate];
                _audienceMembers.Remove(exitCandidate);
                toDestroy.GetComponent <AudienceMember>().ExitAudience();
            }
        
        }
     
    }

  
}
