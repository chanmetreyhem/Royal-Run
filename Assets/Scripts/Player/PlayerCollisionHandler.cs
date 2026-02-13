using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] Animator animator;
    [SerializeField] float countDownHit = 1f;
    private const string HIT_STRING = "Hit";
    float countDownTimer = 0;
    float adjustMoveSpeed = -2f;
    private void Update()
    {
       countDownTimer += Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (countDownTimer < countDownHit) return;
        animator.SetTrigger(HIT_STRING);
        levelGenerator.ChangeChunkSpeed(adjustMoveSpeed);
        countDownTimer = 0;
    }
}
