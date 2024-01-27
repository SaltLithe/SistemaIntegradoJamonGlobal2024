using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line 
{
    private E_LineType _type; //Line type for the teleprompter
    private float _beginWindow; //Percentage of the duration (0.0 - 1.0)
    private float _endWindow; //Percentage of the duration (0.0 - 1.0)
    private float _duration; //Duration in second
    private bool _comedian1; //If the line is from the first comedian

    public Line(E_LineType type, float beginWindow, float endWindow, float duration, bool comedian1) 
    { 
        this._type = type; 
        this._beginWindow = beginWindow;
        this._endWindow = endWindow;
        this._duration = duration;
        this._comedian1 = comedian1;
    }

    public E_LineType GetLineType() {  return _type; }
    
    public float GetBeginWindow() {  return _beginWindow; }

    public float GetEndWindow() { return _endWindow; }

    public float GetDuration() { return _duration;}

    public bool IsComedian1() { return _comedian1; }

}
