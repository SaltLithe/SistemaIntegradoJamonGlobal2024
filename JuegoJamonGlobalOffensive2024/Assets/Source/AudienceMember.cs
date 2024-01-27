using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class AudienceMember : MonoBehaviour
{
    private E_LineType _lineType;
    public int _id = -1;
    private Transform _exitPoint;

    private bool _tryExit = false; 
    // Start is called before the first frame update

    private void Awake()
    {
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_tryExit) 
        {
            transform.position = Vector3.Lerp(transform.position, _exitPoint.position, Time.deltaTime);
        }
    }

    public void Initialize(int id, Transform exitPointf) 
    {
        _id = id;
        _exitPoint = exitPointf;
    }

    public void ExitAudience() 
    {
        _tryExit = true; 
    }

    public void Laugh()
    {
        Debug.Log(_id + " : JAJAJAJAJ"); 
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "DespawnPoint")
        {
            Debug.Log("LEAVING");
            Destroy(gameObject);

        }
    }

}
