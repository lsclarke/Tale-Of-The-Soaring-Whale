using UnityEngine;

public class PlatformerBounceStomp : MonoBehaviour
{
    [Header("Bounce and Stomp")]

    [SerializeField]
    private Rigidbody _rigidbody;

    private PlatformerCollisionDetection _player_CollisionDetection;

    public float bounceForce;

    public bool isBouncing;

    public float stompMultiplier;

    public GameObject bounceBallObj;

    public GameObject PlayerObj;

    public TrailRenderer trail;

    public bool readyToBounce;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _player_CollisionDetection = GetComponent<PlatformerCollisionDetection>();
    }
    public void ResetPlayerObj()
    {
        PlayerObj.SetActive(true);
        bounceBallObj.SetActive(false);
        trail.emitting = false;
    }

    public void Bounce()
    {
        if (readyToBounce)
        {
            readyToBounce = false;
            isBouncing = true;
            _rigidbody.AddForce(Vector3.down * (bounceForce * 2f), ForceMode.Impulse);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bounce Pad") && isBouncing) return;

        if (isBouncing)
        {
            _rigidbody.AddForce(transform.up * bounceForce * stompMultiplier);
            isBouncing = false;
            Invoke(nameof(ResetPlayerObj), 1f);
        }


    }

    private void FixedUpdate()
    {
        if (isBouncing)
        {
            PlayerObj.SetActive(false);
            bounceBallObj.SetActive(true);
            trail.emitting = true;
        }
    }
}
