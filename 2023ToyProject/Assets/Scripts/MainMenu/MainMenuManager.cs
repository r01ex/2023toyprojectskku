using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private Text textBox;
    private bool isTextVisible = true;

    [SerializeField]
    private GameObject panel1;
    [SerializeField]
    private GameObject panel2;

    [SerializeField]
    private AudioClip audioMainMenuBgm;
    AudioSource audioSource;

    [SerializeField]
    private string nextSceneName;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(TextAnimationCoroutine());
        panel1.SetActive(true);
        panel2.SetActive(false);

        StartCoroutine(SceneTransitionCoroutine());
        if (audioMainMenuBgm != null)
        {
            // AudioSource에 배경 음악 할당
            audioSource.clip = audioMainMenuBgm;
            
            // 재생
            audioSource.Play();
        }
    }

    private IEnumerator TextAnimationCoroutine()
    {
        yield return new WaitForSeconds(1f);
        while (true){
            yield return new WaitForSeconds(0.5f);
            if (isTextVisible)
            {
                textBox.text = "";
            }
            else
            {
                textBox.text = "Press Any Key to Start!";
            }
            isTextVisible = !isTextVisible;
        }
    }
    private IEnumerator SceneTransitionCoroutine()
    {
        yield return new WaitForSeconds(1f);


        panel1.SetActive(false);
        panel2.SetActive(true);
    }    

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown){
            SceneManager.LoadScene(nextSceneName);
        }
        
    }
    public void StopBGM()
    {
        audioSource.Stop();
    }

    // 일시 정지
    public void PauseBGM()
    {
        audioSource.Pause();
    }
    public void ResumeBGM()
    {
        audioSource.UnPause();
    }
}
