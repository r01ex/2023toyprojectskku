using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] AudioSource playershoot;
    [SerializeField] AudioSource playerabsorb;
    [SerializeField] AudioSource playershield;
    [SerializeField] AudioSource playerhit;
    [SerializeField] AudioSource enemyhit;
    [SerializeField] AudioSource victory;
    [SerializeField] AudioSource bgm;
    public static SoundEffectManager Instance;
    bool ison = true;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayShoot()
    {
        playershoot.Play();
    }
    public void PlayAbsorb()
    {
        playerabsorb.Play();
    }
    public void PlayShield()
    {
        playershield.Play();
    }
    public void PlayHit()
    {
        enemyhit.Play();
    }
    public void PlayEnemyHit()
    {
        enemyhit.Play();
    }
    public void PlayVictory()
    {
        victory.Play();
    }
    public void bgmonoff()
    {
        if(ison)
        {
            bgm.mute = true;
        }
        else
        {
            bgm.mute = false;
        }
        ison = !ison;
    }
    public void setBgmVolume(float setTo)
    {
        bgm.volume = setTo;
    }
}
