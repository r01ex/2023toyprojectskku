using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    ///To contain times during several Panels to transition animation
    [SerializeField]
    private float transitionTime;
    [SerializeField]
    private AudioClip audioLogo;
    AudioSource audioSource;

    // Start is called before the first frame update

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        audioSource.clip = audioLogo;
        audioSource.Play();
        StartCoroutine(DelayTime(transitionTime));
    }

    //To convert panel between intro and start panel.
    IEnumerator DelayTime(float time){
        yield return new WaitForSeconds(time);
        GoMainMenuScene();
    }
    public void GoMainMenuScene(){
        SceneManager.LoadScene("MainMenuScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
