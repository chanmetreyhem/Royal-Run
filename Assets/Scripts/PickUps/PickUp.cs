using System;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    private const string PLAYER_STRING = "Player";
    [SerializeField] protected float rotateSpeed = 100f;
    
    private void Update()
    {
        RotateAnimation();
    }

    private void RotateAnimation()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_STRING))
        {
           OnPickUp();
        }
    }
    protected abstract void OnPickUp();
}
