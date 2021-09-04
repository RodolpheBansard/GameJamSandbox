using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{

    public ParticleSystem afterburn1;
    public ParticleSystem afterburn2;

    [Range (0.0f,1.0f)]
    public float pitchSpeed;
    [Range(0.0f, 1.0f)]
    public float rollSpeed;

    [Range(0.0f, 1.0f)]
    public float moveSpeed;

    public void Start()
    {
        afterburn1.Stop();
        afterburn2.Stop();
    }

    public void Pitch(float angle)
    {
        this.transform.Rotate(this.transform.right, angle, Space.World);
    }

    public void Roll(float angle)
    {
        this.transform.Rotate(this.transform.up, angle, Space.World);
    }

    public void Thrust()
    {
        this.transform.Translate(0, -moveSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            afterburn1.Play();
            afterburn2.Play();
        }
        else if (Input.GetKey(KeyCode.Space))
            Thrust();
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            afterburn1.Stop();
            afterburn2.Stop();
        }

        if (Input.GetKey(KeyCode.UpArrow))        
            Pitch(pitchSpeed);
        if (Input.GetKey(KeyCode.DownArrow))
            Pitch(-pitchSpeed);
        if (Input.GetKey(KeyCode.RightArrow))
            Roll(rollSpeed);
        if (Input.GetKey(KeyCode.LeftArrow))
            Roll(-rollSpeed);

    }
}
