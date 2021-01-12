
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "CutsceneText")]
public class TextScriptable : ScriptableObject
{
    public string text;
    public float interval;
    public TextObjs textObjs;
    public string init = "";
}
