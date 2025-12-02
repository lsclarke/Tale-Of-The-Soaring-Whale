using UnityEngine;

public class PlatformerPlayerLeap : MonoBehaviour
{

    [Header("Air Dive Jump")]

    public float diveForwardForce;
    public float diveUpForce;

    [SerializeField]
    private Rigidbody _rigidbody;

    private PlatformerCollisionDetection _player_CollisionDetection;

    public GameObject leapBallObj;

    public GameObject PlayerObj;

    public TrailRenderer trail;

    public bool readyToLeap;

    public bool isLeaping;

    public Vector3 leapDirection;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void AirLeap()
    {
        _rigidbody.useGravity = false;
        leapDirection = ((PlayerObj.transform.forward * diveForwardForce) + (PlayerObj.transform.up * diveUpForce));

        if (readyToLeap)
        {
            _rigidbody.useGravity = true;
            isLeaping = true;
            readyToLeap = false;
            _rigidbody.AddForce(leapDirection, ForceMode.Force);
        }
        else return;
    }

    public void ResetPlayerObj()
    {
        PlayerObj.SetActive(true);
        leapBallObj.SetActive(false);
        trail.emitting = false;
    }

    private void FixedUpdate()
    {
        if (isLeaping)
        {
            PlayerObj.SetActive(false);
            leapBallObj.SetActive(true);
            trail.emitting = true;
            Invoke(nameof(ResetPlayerObj), 1.8f);
        }
    }
}
