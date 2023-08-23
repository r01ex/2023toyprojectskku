using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioMainMenuBGM;
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
        audioSource.clip = audioMainMenuBGM;
        if(audioSource.clip != null){
            audioSource.Play();
        }
    }
}
