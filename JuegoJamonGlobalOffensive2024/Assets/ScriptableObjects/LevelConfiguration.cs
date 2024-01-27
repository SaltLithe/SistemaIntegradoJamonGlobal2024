using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfiguration", menuName = "ScriptableObjects/LevelConfigurationSO", order = 1)]
public class LevelConfiguration : ScriptableObject
{
    [SerializeField] private List<Line> scriptedLines;
    [SerializeField] private int generatedLinesAmmount;
    [SerializeField] private List<E_LineType> allowedGeneratedLineTypes;
    [SerializeField] private bool allowedSecondComedian;
}
