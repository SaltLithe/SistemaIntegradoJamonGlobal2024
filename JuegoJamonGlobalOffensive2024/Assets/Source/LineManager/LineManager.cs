using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LineManager : MonoBehaviour
{

    private Stack<Line> _lines;

    private Line _currentLines;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateLines(int numLines, List<E_LineType> disabledLineTypes, bool doubleComedian)
    {
        List<Line> lines = new List<Line>();
        Array values = Enum.GetValues(typeof(E_LineType));
        System.Random random = new System.Random();
        E_LineType randomBar = (E_LineType)values.GetValue(random.Next(values.Length));
    }

    public List<Line> GetLines()
    {
        List<Line> lines = new List<Line>();
        return lines;
    }

}
