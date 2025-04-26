using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable")]
public class SpreadSheetData : ScriptableObject
{
    public List<QuizTemplate> quizTemplates { get; set; }
}
