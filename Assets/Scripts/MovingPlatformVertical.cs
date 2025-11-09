using UnityEngine;

public class MovingPlatformVertical : MonoBehaviour
{
    public Transform pointA, pointB;
    public float speed;
    public bool moveUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveUp == true)
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
        else
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }
        if (transform.position.y <= pointB.position.y)
        {
            moveUp = true;
        }
        if (transform.position.y >= pointA.position.y)
        {
            moveUp = false;
        }
    }
}
