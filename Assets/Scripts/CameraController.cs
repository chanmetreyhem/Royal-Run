using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float minFov = 20f;
    [SerializeField] float maxFov = 120f;
    [SerializeField] float zoomDuration = 1f;
    [SerializeField] float speedModifier = 5f;
    [SerializeField] ParticleSystem speedUpPartical;
    float elapsedTime = 0f;
    CinemachineCamera camera;

    private void Awake()
    {
        camera = GetComponent<CinemachineCamera>();
    }
    public void ChangeCameraFOV(float speedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFovCoroutine(speedAmount));
    }
    IEnumerator ChangeFovCoroutine(float speedAmount)
    {
        float startFov = camera.Lens.FieldOfView;
        float targetFov = Mathf.Clamp(startFov + speedAmount * speedModifier ,minFov,maxFov);

        if(speedAmount > 0)
        {
            speedUpPartical.Play();
        }
        while (elapsedTime < zoomDuration)
        {
           
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / zoomDuration;
            camera.Lens.FieldOfView = Mathf.Lerp(startFov, targetFov, t);
            yield return null;
        }

        camera.Lens.FieldOfView = targetFov;

    }
}
