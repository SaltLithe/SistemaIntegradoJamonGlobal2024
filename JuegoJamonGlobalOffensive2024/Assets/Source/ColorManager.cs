using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorManager 
{
    public static Dictionary<E_LineType, Color> _colorDictionary = new Dictionary<E_LineType, Color>()
    {
        {E_LineType.RED, Color.red },
        {E_LineType.GREEN, Color.green },
        {E_LineType.BLUE, Color.blue },
        {E_LineType.PURPLE, Color.magenta },
        {E_LineType.YELLOW, Color.yellow },
        {E_LineType.BLACK, Color.black },
        {E_LineType.NEUTRAL, Color.white }

    };

   public static Color getLineColor(E_LineType lineType) 
    {
        return _colorDictionary[lineType];
    }
}

