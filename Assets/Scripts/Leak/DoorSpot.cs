using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpot : MonoBehaviour
{
    public Material greenMaterial;
    public Material redMaterial;
    public MeshRenderer spotRenderer;

    private void Start() {

        spotRenderer.material = redMaterial;
    }

    public void SetSpotToGreen(){
        spotRenderer.material = greenMaterial;
    }
    
}
