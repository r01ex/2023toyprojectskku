using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField]
    private float maxYOffset; 
    
    [SerializeField]
    private float movementSpeed;

    private float initialY;
    private float timeElapsed = 0.0f;

    private void Start()
    {
        initialY = transform.position.y; // 초기 Y 좌표 저장
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime; 
        float newY = initialY + Mathf.Sin(timeElapsed * movementSpeed) * maxYOffset;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
