using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHexagon : MonoBehaviour
{
    int IntervalCounter = 100;
    int Counter = 0;
    int Interval;
    int[] randlist = new int[53];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 53; i++)
        {
            randlist[i] = i;
        }
        for (int i=0;i<53;i++)
        {
            int r = Random.Range(0, 53);
            int temp = randlist[i];
            randlist[i] = randlist[r];
            randlist[r] = temp;
        }
        Init(800);
    }

    // Update is called once per frame
    void Update()
    {
        IntervalCounter--;
        if (IntervalCounter <= 0)
        {
            this.transform.GetChild(randlist[Counter]).gameObject.SetActive(true);
            Counter++;
            IntervalCounter = Interval;
        }
    }
    public void Init(int interval)
    {
        IntervalCounter = interval;
        Interval = interval;
    }
}
