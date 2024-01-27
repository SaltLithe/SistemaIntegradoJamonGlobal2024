using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line 
{
    private E_LineType _type; //Line type for the teleprompter
    private double _eventStamp; //Time stamp for the line event
    private double _duration; //Duration in second
    private bool _comedian1; //If the line is from the first comedian

    public Line(E_LineType type, double duration, double eventStamp, bool comedian1) 
    { 
        this._type = type; 
        this._eventStamp = eventStamp;
        this._duration = duration;
        this._comedian1 = comedian1;
    } 

    public E_LineType GetLineType() {  return _type; }

    public double GetEventStamp() { return _eventStamp; }

    public double GetDuration() { return _duration;}

    public bool IsComedian1() { return _comedian1; }

}
