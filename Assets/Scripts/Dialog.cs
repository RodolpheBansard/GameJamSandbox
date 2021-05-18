using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public string fileName;
    public TMP_Text dialogText;

    private string[] dialogContent;
    private int nbLines;

    private void Start()
    {
        dialogText.text = "";
        string[] content = File.ReadAllLines(Application.dataPath + "/TextFiles/" + fileName + ".txt");
        nbLines = content.Length;

        StartCoroutine
        
    }

}
