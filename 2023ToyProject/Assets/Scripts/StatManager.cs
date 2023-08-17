using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class StatManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI text1, text2, text3, text4, text5;


    private int atomicNum = 1;
    private int atk = 0;
    private int speed = 0;
    private int shieldTime = 0;
    private int grabRange = 0;

    public void atomicNumUpgradeStat(){
        atomicNum +=1;
        text1.SetText(atomicNum.ToString());

    }

    public void atkUpgradeStat(){
        atk +=1;
        text2.SetText(atk.ToString());

    }

    public void speedUpgradeStat(){
        speed +=1;
        text3.SetText(speed.ToString());

    }

    public void shieldTimeUpgradeStat(){
        shieldTime +=1;
        text4.SetText(shieldTime.ToString());

    }

    public void grabRangeUpgradeStat(){
        grabRange +=1;
        text5.SetText(grabRange.ToString());

    }

    public void GoShopScene(){
        SceneManager.LoadScene("ShopScene");
    }
}
