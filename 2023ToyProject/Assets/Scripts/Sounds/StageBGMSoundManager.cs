using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBGMSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioStageBGM;
    AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource.loop = true;
        PlaySound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySound(){
        audioSource.clip = audioStageBGM;
        if(audioSource.clip != null){
            audioSource.Play();
        }
    }
    public void StopSound(){
        audioSource.Stop();
    }
}
