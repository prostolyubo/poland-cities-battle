using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    public bool isFirst;
    public Toggle toggle;
    public RoundManager manager;
    public PlayerActor prefab;
    public Text name;
    public Text legend;
    public TextAsset legends;

    string characterLegend;

    private void Awake()
    {
        if (prefab == null)
        {
            toggle.interactable = false;
            name.text = "PLACEHOLDER";
            return;
        }
        toggle.onValueChanged.AddListener(HandleCharacterSelected);
        name.text = prefab.displayName;
        characterLegend = GetCharacterLegend();
        if (toggle.isOn)
            legend.text = characterLegend;
    }

    private void HandleCharacterSelected(bool isSelected)
    {
        if (!isSelected)
            return;

        if (isFirst)
            manager.first = prefab;
        else
            manager.second = prefab;

        legend.text = characterLegend;
    }

    private string GetCharacterLegend()
    {
        StringBuilder builder = new StringBuilder();

        using (StringReader reader = new StringReader(legends.text))
        {
            string line = reader.ReadLine();
            while ( line.Length == 0 
                || !(line.StartsWith("#") && line.Substring(1).ToLower() == prefab.displayName.ToLower()))
            {
                line = reader.ReadLine();
                if (line == null)
                    break;
            }
            line = reader.ReadLine();
            while (line != null && !line.StartsWith("#"))
            {
                builder.AppendLine(line);
                line = reader.ReadLine();
            }
        }

        return builder.ToString().Trim();
    }
}
