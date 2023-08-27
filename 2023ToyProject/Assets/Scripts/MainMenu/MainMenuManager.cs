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

    bool flag = false;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(TextAnimationCoroutine());
    }

    private IEnumerator TextAnimationCoroutine()
    {
        while (true){
            yield return new WaitForSeconds(0.5f);
            if (isTextVisible)
            {
                textBox.text = "";
            }
            else
            {
                if (flag)
                {
                    textBox.text = "Loading";
                }
                else
                {
                    textBox.text = "Press Any Key to Start!";
                }
            }
            isTextVisible = !isTextVisible;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown&&flag==false){
            flag = true;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
        }
        
    }
}
