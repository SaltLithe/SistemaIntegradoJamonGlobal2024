using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfiguration", menuName = "ScriptableObjects/LevelConfigurationSO", order = 1)]
public class LevelConfiguration : ScriptableObject
{
    [SerializeField] private List<Line> scriptedLines;
    [SerializeField] private int generatedLinesAmmount;
    [SerializeField] private List<E_LineType> allowedGeneratedLineTypes;
    [SerializeField] private bool C2Active, drumsActive, lightsActive;
    [SerializeField] private int audienceMembersCount;

    public List<Line> GetScriptedLines() { return scriptedLines; }
    public int GetGeneratedLinesAmmount() {  return generatedLinesAmmount; }
    public List<E_LineType> GetAllowedLineTypes() { return allowedGeneratedLineTypes; }
    public bool GetC2Active() {  return C2Active;}
    public bool GetDrumsActive() {  return drumsActive;} 
    public bool GetLightsActive() {  return lightsActive; }
    public int GetAudienceMembersCount() {  return audienceMembersCount; }
}
