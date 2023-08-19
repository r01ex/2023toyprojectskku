using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATGC : MonoBehaviour
{
    [SerializeField] GameObject[] letterPrefabs;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void debug()
    {
        Shoot(5, Random.Range(-1.7f, 1.7f), Random.Range(-20f, 20f), 0);
    }    

    public void Shoot(float moveSpeed, float offset, float rotation, int letterNum)
    {
        GameObject letter = Instantiate(letterPrefabs[letterNum]);
        Vector3 spawnPos = new Vector3(transform.position.x + offset, 6.55f, transform.position.z);
        letter.transform.position = spawnPos;
        letter.transform.Rotate(new Vector3(0, 0, rotation));
        foreach (Transform t in letter.transform)
        {
            t.gameObject.GetComponent<EnemyBullet>().move1Init(moveSpeed);
        }
    }
}
