namespace ArionDigital
{
    using System.Collections;
    using UnityEngine;

    public class CrashCrate : MonoBehaviour
    {
        [Header("Whole Create")]
        public MeshRenderer wholeCrate;
        public BoxCollider boxCollider;
        [Header("Fractured Create")]
        public GameObject fracturedCrate;
        [Header("Audio")]
        public AudioSource crashAudioClip;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "PlayerAttackBox")
            {
                wholeCrate.enabled = false;
                boxCollider.enabled = false;
                fracturedCrate.SetActive(true);
                crashAudioClip.Play();
                StartCoroutine(DestroyCrate());
            }
        }

        IEnumerator DestroyCrate()
        {
            yield return new WaitForSeconds(1f);
            Destroy(this.gameObject);
        }

        [ContextMenu("Test")]
        public void Test()
        {
            wholeCrate.enabled = false;
            boxCollider.enabled = false;
            fracturedCrate.SetActive(true);
        }
    }
}