using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Rigidbody rb;
    public Image HealthBar;
    public float knockBackPower;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log($"HEALTH: {HealthBar.fillAmount}");
            HealthBar.fillAmount -= 33.333f;
            Vector3 oppositeVelocity = -rb.linearVelocity;

            // Normalize the vector to get just the direction, then apply power
            Vector3 knockBackForce = oppositeVelocity.normalized * knockBackPower;

            // Apply the force as an acceleration that is continuous
            rb.AddForce(knockBackForce, ForceMode.Acceleration);

        }
    }
}
