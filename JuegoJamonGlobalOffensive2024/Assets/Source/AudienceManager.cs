using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudienceManager : MonoBehaviour
{
    public GameObject _audienceMemberPrefab;
    public BoxCollider _spawnVolume;

    private Bounds _spawnBounds;

    void Awake()
    {
        GenerateAudience(new E_LineType[] { E_LineType.RED }, new int[] { 1 });
       
    }
    // Start is called before the first frame update
    void Start()
    {
        _spawnBounds = _spawnVolume.bounds;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Dos arrays , uno con colores y otro con numeros para cada color, si el primer elemento de tipos es rojo , y el primer
    //elemento de audiencia es 5 , saldran 5 rojos
    private void GenerateAudience(E_LineType[] audienceColors , int[] audienceNumbers) 
    {
        for (int i = 0; i< audienceColors.Length; i++) 
        {
            for(int j = 0; j < audienceNumbers[i]; j++) 
            {
                Vector3 nextPosition = GenNextPosition();
                SpawnAudienceMember(audienceColors[i],nextPosition);
            }
        }
    }

    private void SpawnAudienceMember(E_LineType color,Vector3 nextPosition) 
    {
        GameObject newLineMember = new GameObject();
        newLineMember.SetActive(false);
        newLineMember = Instantiate(_audienceMemberPrefab, nextPosition, Quaternion.identity);
        newLineMember.GetComponent<AudienceMember>().SetLineType(color);
        newLineMember.SetActive(true);
    }

    private Vector3 GenNextPosition() 
    {
        float offsetX = Random.Range(-_spawnBounds.extents.x, _spawnBounds.extents.x);
        float offsetZ = Random.Range(-_spawnBounds.extents.z, _spawnBounds.extents.z);
        return new Vector3(offsetX,0, offsetZ);
    }

}
