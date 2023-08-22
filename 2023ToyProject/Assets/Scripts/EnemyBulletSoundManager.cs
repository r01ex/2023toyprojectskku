using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioH2OAttack;
    [SerializeField]
    private AudioClip audioH2OHit;
    [SerializeField]
    private AudioClip audioCH4Attack;
    [SerializeField]
    private AudioClip audioCH4Hit;
    AudioSource audioSource;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string action, int index){
        Debug.Log("Sound "+ action+ " "+index );
        switch(index){
            case 1:
                switch(action){
                    case "ATTACK":
                        audioSource.clip = audioH2OAttack;
                        break;
                    case "Hit":
                        audioSource.clip = audioH2OHit;
                        break;  
                }
                break;
            case 2:
                switch(action){
                    case "ATTACK":
                        audioSource.clip = audioCH4Attack;
                        break;
                    case "Hit":
                        audioSource.clip = audioCH4Hit;
                        break;  
                }
                break;
        }

        audioSource.Play();
    }
}
