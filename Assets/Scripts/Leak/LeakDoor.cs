using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeakDoor : MonoBehaviour
{
    public Animator animator;
    public DoorSpot doorSpot;
    public Crack[] cracks;

    public void UpdateLeak(){
        if(IsCracksSealed()){
            animator.SetTrigger("Open");
            doorSpot.SetSpotToGreen();
        }
    }

    private bool IsCracksSealed(){
        foreach(Crack crack in cracks){
            if(crack.GetIsSealed() == false){
                return false;
            }
        }
        return true;
    }
}
