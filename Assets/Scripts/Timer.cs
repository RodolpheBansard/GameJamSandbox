using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] float minutes;
    [SerializeField] float secondes;

    [SerializeField] TMP_Text timerText;
    [SerializeField] Image timerImage;

    float totalNbSecondes;
    float currentNbSecondes;

    private void Start()
    {
        totalNbSecondes = 60 * minutes + secondes;
        currentNbSecondes = totalNbSecondes;

        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        

        while(currentNbSecondes >= 0)
        {
            timerText.text = getTime(currentNbSecondes);
            timerImage.fillAmount = currentNbSecondes / totalNbSecondes;
            yield return new WaitForSeconds(0.01f);
            currentNbSecondes -= 0.01f;
        }
        
        
    }

    private string getTime(float nbSecondes)
    {
        
        TimeSpan t = TimeSpan.FromSeconds(nbSecondes);
        string answer = string.Format("{0:D2}:{1:D2}", t.Minutes,t.Seconds);
        return answer;
    }
}
