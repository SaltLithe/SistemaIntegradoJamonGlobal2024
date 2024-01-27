using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudienceMember : MonoBehaviour
{
    private E_LineType _lineType;

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

    public void SetLineType(E_LineType lineType) 
    {
        _lineType = lineType;
        GetComponent<MeshRenderer>().material.SetColor("_Color", ColorManager.getLineColor(lineType));
    }
}
