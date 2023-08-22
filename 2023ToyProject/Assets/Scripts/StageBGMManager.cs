using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBGMManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioStageBgm;
    AudioSource audioSource;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        audioSource.loop = true;
        StartBGM();
    }

    // Update is called once per frame
    void Update()
    {       
    }
    public void StartBGM(){
        if (audioStageBgm != null)
        {
            Debug.Log("PlayStageBGM");
            audioSource.clip = audioStageBgm;
            audioSource.Play();
        }

    }
    public void StopBGM()
    {
        Debug.Log("StopStageBGM");
        audioSource.Stop();
    }
}
