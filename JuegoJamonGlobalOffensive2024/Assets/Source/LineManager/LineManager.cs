using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LineManager : MonoBehaviour
{

    private Stack<Line> _lines;

    [SerializeField]
    private double _lineDuration;

    private void Awake()
    {
        _lines = new Stack<Line>();
    }

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
        double eventStamp = 0;
        bool comedian1 = true;
        for (int i = 0; i < numLines; i++)
        {
            E_LineType randomLineType;
            do
            {
                randomLineType = (E_LineType)values.GetValue(random.Next(values.Length));
            } while (!disabledLineTypes.Contains(randomLineType));
            eventStamp = random.NextDouble() * _lineDuration;
            if(doubleComedian)
            {
                int randomInt = random.Next(0, 2);
                comedian1 = randomInt == 0;
            }
            lines.Add(new Line(randomLineType, _lineDuration, eventStamp, comedian1));
        }
        ShuffleLines(lines);
        foreach (Line line in lines)
        {
            _lines.Push(line);
        }

    }

    public void AddScriptedLines(List<Line> scriptedLines)
    {
        if (scriptedLines != null)
        {
            scriptedLines.Reverse();
            foreach (Line line in scriptedLines)
            {
                _lines.Push(line);
            }
        }
    }

    public List<Line> GetLines(int numLines)
    {
        if (numLines > _lines.Count)
        {
            numLines = _lines.Count;
        }

        List<Line> lines = new List<Line>();
        Stack<Line> stack = new Stack<Line>();
        Line line = null;
        for (int i = 0; i < numLines; i++)
        {
            line = _lines.Pop();
            lines.Add(line);
            stack.Push(line);
        }
        for (int i = 0; i < numLines - 1; i++)
        {
            line = stack.Pop();
            _lines.Push(line);
        }
        return lines;
    }


    private void ShuffleLines(List<Line> lines)
    {
        System.Random random = new System.Random();
        int n = lines.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Line value = lines[k];
            lines[k] = lines[n];
            lines[n] = value;
        }
    }

    public void ResetLines()
    {
        _lines = new Stack<Line>();
    }
}
