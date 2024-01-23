using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool movingRight = false;     // The enemy starts by looking towards the left
    public float speed = 1;

    // Update is called once per frame
    void Update()
    {
        if (movingRight)  // if the enemy is moving towards the right, continue to do so
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the enemy collides with a game object and is not any of the following, change direction
        if (!other.CompareTag("Player") && !other.CompareTag("Health") && !other.CompareTag("Speed") && !other.CompareTag("Shield") && !other.CompareTag("Jump"))
        {
            movingRight = !movingRight;
            if (movingRight)
            {
                gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y - 180, gameObject.transform.eulerAngles.z);
            }
            else
            {
                gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y + 180, gameObject.transform.eulerAngles.z);
            }
            
        }
    }
}
