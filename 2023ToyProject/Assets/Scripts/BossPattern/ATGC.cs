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
        Shoot(5, Random.Range(-2.1f, 2.1f), Random.Range(-25f, 25f), 8);
    }
    /// <summary>
    /// A : offset 1.4, rotation 30
    /// T : offset 2.2, rotation 30
    /// G : offset 1.8, rotation 30
    /// C : offset 1.8, rotation 30
    /// BigO : offset 2.5, rotation 0
    /// SmallO : offset 2.9, rotation 0
    /// ExclMark : offset 2.3, rotation 25
    /// SmallQuesMark : offset 2.5, rotation 25
    /// BigQuesMark : offset 2.1, rotation 25
    /// </summary>
    public void Shoot(float moveSpeed, float offset, float rotation, int letterNum, int stageIndex = 0)
    {
        GameObject letter = Instantiate(letterPrefabs[letterNum]);
        Vector3 spawnPos = new Vector3(transform.position.x + offset, 6.55f, transform.position.z);
        letter.transform.position = spawnPos;
        letter.transform.Rotate(new Vector3(0, 0, rotation));
        foreach (Transform t in letter.transform)
        {
            t.gameObject.GetComponent<EnemyBulletSet>().move1Init(moveSpeed);
        }
    }
    public void ShootToPlayer(float moveSpeed, float rotation, int letterNum)
    {
        GameObject letter = Instantiate(letterPrefabs[letterNum]);
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        letter.transform.position = spawnPos;
        letter.transform.Rotate(new Vector3(0, 0, rotation));
        Vector3 playerSnapshot = PlayerManager.Instance.transform.position;
        foreach (Transform t in letter.transform)
        {
            t.gameObject.GetComponent<EnemyBulletSet>().move3Init(moveSpeed, (playerSnapshot - this.transform.position).normalized);
        }
    }
}
