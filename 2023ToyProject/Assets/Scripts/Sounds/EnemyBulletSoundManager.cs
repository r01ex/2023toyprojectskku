using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioAttack;
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
    public void PlayAttackSound(int stage)
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource Not assigned");
            return;
        }

        if (stage < 0 || stage >= audioAttack.Length)
        {
            Debug.LogError("Invalid stage value");
            return;
        }

        if (audioAttack[stage] == null)
        {
            Debug.LogWarning("AudioClip is null");
            return;
        }

        audioSource.clip = audioAttack[stage];
        audioSource.Play();
    }
}
