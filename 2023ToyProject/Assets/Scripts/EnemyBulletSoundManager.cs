using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] Attack;
    [SerializeField]
    private AudioClip[] Hit;

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
        switch(action){
            case "ATTACK":
                audioSource.clip = Attack[index];
                break;
            case "Hit":
                audioSource.clip = Hit[index];
                break;
        }

        audioSource.Play();
    }
}
