using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N2Gimmick : MonoBehaviour
{
    bool isInhale = false;
    bool isEmmision = false;
    [SerializeField] float power;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInhale)
        {
            Vector2 player_to_boss = (PlayerManager.Instance.transform.position - this.transform.position).normalized;
            PlayerManager.Instance.transform.Translate(-player_to_boss * power * Time.deltaTime);
        }
        else if (isEmmision)
        {
            Vector2 player_to_boss = (PlayerManager.Instance.transform.position - this.transform.position).normalized;
            PlayerManager.Instance.transform.Translate(player_to_boss * power * Time.deltaTime);
        }
    }
    public void Inhale(bool isinhale)
    {
        isInhale = isinhale;
    }
    public void Emission(bool isemssion)
    {
        isEmmision = isemssion;
    }
}
