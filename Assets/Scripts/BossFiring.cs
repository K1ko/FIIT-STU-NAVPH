using UnityEngine;

public class BossFiring : MonoBehaviour
{
    public GameObject firePoint;
    public Transform firePos;

    private float timer;
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= 10.0f)
        {
            timer += Time.deltaTime;
            //Debug.Log(distanceToPlayer);

            if (timer >= 1.0f)
            {
                timer = 0f;
                Fire();
                
            }
        }
    }

    void Fire()
    {
        Instantiate(firePoint, firePos.position, Quaternion.identity);
        timer = 0f;
    }
}
