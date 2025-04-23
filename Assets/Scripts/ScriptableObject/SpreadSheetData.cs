using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable")]
public class SpreadSheetData : ScriptableObject
{
    [SerializeField]
    public StringReader reader { get; set; }
}
