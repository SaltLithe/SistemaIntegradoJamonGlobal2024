using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudienceManager : MonoBehaviour
{
    public GameObject _audienceMemberPrefab;
    public BoxCollider _spawnBounds;
    public int _audienceMembersCount = 20; 

    void Awake()
    {
        GenerateAudience(_audienceMembersCount);
       
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Dos arrays , uno con colores y otro con numeros para cada color, si el primer elemento de tipos es rojo , y el primer
    //elemento de audiencia es 5 , saldran 5 rojos
    private void GenerateAudience(int audienceMembersCount) 
    {
        for (int i = 0; i< audienceMembersCount; i++) 
        {       
                Vector3 nextPosition = GenNextPosition();
                SpawnAudienceMember(nextPosition);
        }
    }

    private void SpawnAudienceMember(Vector3 nextPosition) 
    {

        GameObject newLineMember = Instantiate(_audienceMemberPrefab, nextPosition, Quaternion.identity);
        newLineMember.SetActive(true);
    }

    private Vector3 GenNextPosition() 
    {
        float offsetX = Random.Range(_spawnBounds.transform.position.x-(_spawnBounds.transform.localScale.x/2), _spawnBounds.transform.position.x + (_spawnBounds.transform.localScale.x / 2));
        float offsetZ = Random.Range(_spawnBounds.transform.position.y - (_spawnBounds.transform.localScale.y / 2), _spawnBounds.transform.position.y + (_spawnBounds.transform.localScale.y / 2));
        return new Vector3(offsetX,2, offsetZ);
    }

}
