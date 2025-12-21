using UnityEngine;

public class MovingPlatformVertical : MonoBehaviour  //  PARTS OF THE CODE WERE DONE USING VIDEO TUTORIAL and modified for this game needs - Controls vertical movement of a platform between two points
{
    public Transform pointA, pointB;
    public float speed;
    public bool moveUp;

    void Start()
    {
        
    }

    void FixedUpdate()  // Move platform and reverse direction at endpoints
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
