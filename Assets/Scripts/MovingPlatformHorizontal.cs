using UnityEngine;

public class MovingPlatformHorizontal : MonoBehaviour
{
    public Transform pointA, pointB;
    public float speed;
    public bool moveRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveRight == true)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (transform.position.x <= pointB.position.x)
        {
            moveRight = true;
        }
        if (transform.position.x >= pointA.position.x)
        {
            moveRight = false;
        }
    }
}
