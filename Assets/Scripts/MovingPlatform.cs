using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public Transform[] points;
    private int i;

    private Vector2 lastPosition;
    public Vector2 PlatformVelocity { get; private set; }

    void Start()
    {
        transform.position = points[startingPoint].position;
        lastPosition = transform.position;
        
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        PlatformVelocity = ((Vector2)transform.position-lastPosition) / Time.deltaTime;
        lastPosition = transform.position;  
    }
}
