using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

public class CutsceneManager : MonoBehaviour
{
    public TextMeshPro playerText;
    public TextMeshPro bossText;
    public List<TextScriptable> arguments = new List<TextScriptable>();



    public void StartShowText(int index)
    {
        StartShowText(arguments[index].text, arguments[index].interval, arguments[index].textObjs, arguments[index].init);
    }
    public void StartShowText(string text, float interval, TextObjs textObj, string init = "")
    {
        TextMeshPro use;
        if(textObj == TextObjs.boss)
        {
            use = bossText;
        }
        else { use = playerText; }
        StartCoroutine(ShowText(text, interval, use, init));
    }
    public void ClearText(string texts)
    {
        if (texts == TextObjs.boss.ToString())
        {
            bossText.text = "";
        }
        else { playerText.text = ""; }
    }
    private IEnumerator ShowText(string text, float interval, TextMeshPro textObj, string init = "")
    {
        textObj.text = init;
        foreach(char c in text)
        {
            textObj.text += c.ToString();
            yield return new WaitForSeconds(interval);
        }
        yield break;
    }
    public void PlayMusic(string name)
    {
        AudioManager.instance.PlayMusic(name);
    }
}
