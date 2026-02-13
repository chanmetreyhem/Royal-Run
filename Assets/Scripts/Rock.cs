using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] float shakeModifer = 10f;
    CinemachineImpulseSource impulseSource;
    [SerializeField] ParticleSystem effect;
    [SerializeField] AudioSource audioSource;

    float collisionCountDown = 1f;
    float collisionTimer = 0;
    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
         collisionTimer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionTimer < collisionCountDown) return;
        CollisionFX(collision);
        FireImpuse();
        collisionTimer = 0;
    }

    private void FireImpuse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1f / distance) * shakeModifer;
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);
        impulseSource.GenerateImpulse(shakeIntensity);
    }

    void CollisionFX(Collision collision)
    {
        audioSource.Play();
        ContactPoint contact = collision.contacts[0];
        effect.transform.position = contact.point;
        effect.Play();
    }
}
