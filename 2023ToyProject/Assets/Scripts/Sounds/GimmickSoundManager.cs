using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioGimmick;
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
    public void PlayGimmickSound(int soundIndex)
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource Not assigned");
            return;
        }

        if (soundIndex < 0 || soundIndex >= audioGimmick.Length)
        {
            Debug.LogError("Invalid stage value");
            return;
        }

        if (audioGimmick[soundIndex] == null)
        {
            Debug.LogWarning("AudioClip is null");
            return;
        }

        audioSource.clip = audioGimmick[soundIndex];
        audioSource.Play();
    }
}
