using UnityEngine;

public class Gems : MonoBehaviour
{
    public PlayerInventory playerInventory;
    private float rotationSpeed = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInventory.Gems += 1;
            playerInventory.Points += 110f;
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(transform.rotation.x, rotationSpeed, transform.rotation.z);
    }
}
