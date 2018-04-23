using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordsUsed : MonoBehaviour
{
    public List<string> swords { get; private set; }
    public List<string> suffixs { get; private set; }
    public TextAsset swordFile;
    public TextAsset suffixFile;

    void Awake()
    {
        swords = ReadFromFile(swordFile);
        suffixs = ReadFromFile(suffixFile);
        //Debug
        //PrintSwords();
    }

    // Update is called once per frame
    void Update() { }

    List<string> ReadFromFile(TextAsset fileData)
    {
        string fileContent = fileData.text;

        var lines = fileContent.Split('\n');
        List<string> tmp = new List<string>();
        foreach (var line in lines)
        {
            tmp.Add(line.ToLower().Trim());
        }
        return tmp;
    }

    public void PrintSwords()
    {
        foreach (var elem in swords)
        {
            Debug.Log(elem);
        }
    }
}
