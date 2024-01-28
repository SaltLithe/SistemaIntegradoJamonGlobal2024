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

    float _laughAmount = 0.07f;
    private float _laughInterval = 0.07f;
    private int _laughShakes = 10; 

    private bool _tryExit = false;
    private bool _tryLaugh = false;

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

        if (_tryLaugh) 
        {
            StartCoroutine(Tremble());
            _tryLaugh = false; 
        }
    }
    IEnumerator Tremble()
    {
        for (int i = 0; i < _laughShakes; i++)
        {
            transform.localPosition += new Vector3(0, _laughAmount, 0);
            yield return new WaitForSeconds(_laughInterval);
            transform.localPosition -= new Vector3(0, _laughAmount, 0);
            yield return new WaitForSeconds(_laughInterval);
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

        _tryLaugh = true; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DespawnPoint")
        {
            Destroy(gameObject);
        }
    }

}
