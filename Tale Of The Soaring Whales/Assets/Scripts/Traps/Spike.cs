using UnityEngine;
using UnityEngine.UI;

public class Spike : MonoBehaviour
{
    public Rigidbody rb;
    public Image HealthBar;
    public float knockBackPower;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log($"HEALTH: {HealthBar.fillAmount}");
            HealthBar.fillAmount -= 33.333f;

            // Calculate the opposite direction of current velocity
            Vector3 oppositeVelocity = -rb.linearVelocity;

            // Normalize the vector to get just the direction, then apply power
            Vector3 brakeForce = oppositeVelocity.normalized * knockBackPower;

            // Apply the force as an acceleration that is continuous
            rb.AddForce(brakeForce, ForceMode.Acceleration);
        }
    }
}
