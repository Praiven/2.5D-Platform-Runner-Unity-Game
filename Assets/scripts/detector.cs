using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Detector : MonoBehaviour  // This script activates a trigger if the player is touching the had of the enemy - and thus must kill him 
{
    public UnityEvent<Collider> onTriggerEnter;
    public UnityEvent<Collider> onTriggerStay;
    public UnityEvent<Collider> onTriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemyHead"))
        {
            onTriggerEnter?.Invoke(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        onTriggerStay?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        onTriggerExit?.Invoke(other);
    }
}
