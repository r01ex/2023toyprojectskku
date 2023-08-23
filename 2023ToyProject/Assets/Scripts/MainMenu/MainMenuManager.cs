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
    private string nextSceneName;

    Coroutine flashanykey;
    bool flag = false;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(TextAnimationCoroutine());
        panel1.SetActive(true);
        panel2.SetActive(false);

        flashanykey = StartCoroutine(SceneTransitionCoroutine());
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
    private IEnumerator TextAnimationCoroutineLoad()
    {
        isTextVisible = false;
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (isTextVisible)
            {
                textBox.text = "";
            }
            else
            {
                textBox.text = "Loading";
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
        if (Input.anyKeyDown&&flag==false){
            StopCoroutine(flashanykey);
            StartCoroutine(TextAnimationCoroutineLoad());
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2);
            flag = true;
        }
        
    }
}
