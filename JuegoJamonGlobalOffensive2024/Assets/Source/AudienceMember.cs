using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudienceMember : MonoBehaviour
{
    private E_LineType _lineType;
    public int _id = -1; 
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
        
    }

    public void SetId(int id) 
    {
        _id = id;
    }

    public void ExitAudience() 
    {
        Destroy(gameObject);
    }

    public void Laugh()
    {
        Debug.Log(_id + " : JAJAJAJAJ"); 
    }

}
