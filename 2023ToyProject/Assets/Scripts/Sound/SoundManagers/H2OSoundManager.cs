using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H2OSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioBubble;
    [SerializeField]
    private AudioClip audioHidropump;
    [SerializeField]
    private AudioClip audioAttacked;
    [SerializeField]
    private AudioClip audioDeath;
    [SerializeField]
    private AudioClip audioMove;

    AudioSource audioSource;
    
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySound (string action){
        switch (action){
            case "BUBBLE":
                audioSource.clip = audioBubble;
                break;
            case "HIDROPUMP":
                audioSource.clip = audioHidropump;
                break;
            case "ATTACKED":
                audioSource.clip = audioAttacked;
                break;
            case "DEATH":
                audioSource.clip = audioDeath;
                break;   
            case "MOVE":
                audioSource.clip = audioMove;
                break;              
        }
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}
