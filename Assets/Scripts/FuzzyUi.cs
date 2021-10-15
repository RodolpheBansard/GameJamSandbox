using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class FuzzyUi : MonoBehaviour
{
     public Volume volume;
     public Canvas canvas;

    private bool isGamePaused = false;

    private DepthOfField depthOfField;


    private void Start() {
        DepthOfField tmp;
        if( volume.profile.TryGet<DepthOfField>(out tmp)){
            depthOfField = tmp;
        }
        
        
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            ToggleCanva();
        }
        
    }


    private void ToggleCanva(){
        if(!isGamePaused){
            isGamePaused = true;
            depthOfField.active = true;
            Time.timeScale = 0;
            canvas.enabled=true;       
            
        }
         else if(isGamePaused){
            isGamePaused = false;
            depthOfField.active = false;
            Time.timeScale = 1;
            canvas.enabled=false;       
            
        }
    }
}
