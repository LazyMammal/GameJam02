using UnityEngine;
using UnityEngine.UI;

public class InfoBox : ScriptableObject
{
    public static Text infobox()
    {
        return GameObject.FindWithTag("InfoBox").GetComponent<Text>();
    }

    public static void PrependLine(string line)
    {
        infobox().text = line + "\n" + infobox().text;
    }

    public static void AppendLine(string line)
    {
        infobox().text += "\n" + line;
    }
}
