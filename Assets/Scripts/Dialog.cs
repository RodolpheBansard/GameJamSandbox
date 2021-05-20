using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public string fileName;
    public TMP_Text dialogText;
    public float writeSpeed;

    private string[] dialogContent;

    private IEnumerator writeText;
    private int lineIndex;

    private bool dialogIsRunning;
    

    private void Start()
    {
        dialogIsRunning = false;
        lineIndex = 0;
        dialogText.text = "";
        dialogContent = File.ReadAllLines(Application.dataPath + "/TextFiles/" + fileName + ".txt");
        NextLine();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (dialogIsRunning)
            {
                dialogText.text = dialogContent[lineIndex];
                StopCoroutine(writeText);
                lineIndex++;
                dialogIsRunning = false;
            }
            else
            {
                if(lineIndex < dialogContent.Length)
                {
                    NextLine();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }



    IEnumerator WriteText(string line)
    {
        dialogIsRunning = true;
        dialogText.text = "";
        for(int i = 0; i<line.Length; i++)
        {
            dialogText.text += line[i];
            yield return new WaitForSeconds(writeSpeed);
        }
        lineIndex++;
        dialogIsRunning = false;

        
        
    }


    private void NextLine()
    {
        writeText = WriteText(dialogContent[lineIndex]);
        StartCoroutine(writeText);
    }
}
