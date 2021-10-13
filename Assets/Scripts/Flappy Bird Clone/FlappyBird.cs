using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlappyBird : MonoBehaviour
{
    [Header("Player Settings")]
    public Animator animator;
    public Rigidbody2D rigidbody2D;
    public AudioSource audioSource;
    public float JumpVelocity = 3;
    public TMP_Text scoreText;


    [Header("Audio")]
    public AudioClip hitSound;
    public AudioClip deathSound;
    public AudioClip flapSound;
    public AudioClip obstaclePassed;
    private int points = 0;

    private bool isAlive = true;
    private bool isInit = false;

    private void Start() {
        rigidbody2D.gravityScale = 0;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space) && isAlive){
            rigidbody2D.velocity = new Vector3(0,JumpVelocity,0);
            audioSource.PlayOneShot(flapSound);

            if(!isInit){
                isInit = true;
                rigidbody2D.gravityScale = 3;
                FindObjectOfType<ObstacleSpawner>().LaunchSpawn();
            }
        }
    }


    public void playerDied(){
        isAlive = false;
        audioSource.PlayOneShot(hitSound);
        
    }

    public void ObstaclePassed(){
        points++;
        scoreText.text = ""+points;
        audioSource.PlayOneShot(obstaclePassed);
    }

}
