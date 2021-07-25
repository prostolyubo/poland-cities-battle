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
    public Text legend;
    public Text primaryMove;
    public Text secondaryMove;
    public TextAsset legends;
    public TextAsset attacks;

    string characterLegend;
    private string primary;
    private string secondary;

    private void Awake()
    {
        toggle.onValueChanged.AddListener(HandleCharacterSelected);
        if (prefab != null)
        {
            characterLegend = GetCharacterLegend();
            GetCharacterAttacks();
        }
        else
        {
            characterLegend = "Wybrana losowo postać.";
            primary = "Atak wylosowanej postaci";
            secondary = "Umiejętnosć specjalna wylosowanej postaci";
        }

        if (toggle.isOn)
        {
            legend.text = characterLegend;
            primaryMove.text = primary;
            secondaryMove.text = secondary;
        }
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
        primaryMove.text = primary;
        secondaryMove.text = secondary;
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
    private (string, string) GetCharacterAttacks()
    {
        using (StringReader reader = new StringReader(attacks.text))
        {
            string line = reader.ReadLine();
            while (line.Length == 0
                || !(line.StartsWith("#") && line.Substring(1).ToLower() == prefab.displayName.ToLower()))
            {
                line = reader.ReadLine();
                if (line == null)
                    break;
            }
            primary = reader.ReadLine();
            secondary = reader.ReadLine();
        }

        return (primary, secondary);
    }
}
