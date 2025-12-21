using UnityEngine;

public class MovingPlatformHorizontal : MonoBehaviour   //  PARTS OF THE CODE WERE DONE USING VIDEO TUTORIAL and modified for this game needs - Controls horizontal movement of a platform between two points
{
    public Transform pointA, pointB;
    public float speed;
    public bool moveRight;

    void Start()
    {
        
    }

    void FixedUpdate()  // Move platform and reverse direction at endpoints
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
