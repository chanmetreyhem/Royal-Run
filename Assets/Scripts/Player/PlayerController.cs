using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{  
    private Rigidbody rb;
    private Vector2 movement;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float zClamps = 2;
    [SerializeField]float xClamps = 2;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 currentPos = rb.position;
        Vector3 moveDirection = new Vector3(movement.x, 0 ,movement.y);
        Vector3 newMovePos = currentPos + moveDirection * (moveSpeed * Time.fixedDeltaTime);
        newMovePos.x = Mathf.Clamp(newMovePos.x ,-xClamps,xClamps);
        newMovePos.z = Mathf.Clamp(newMovePos.z ,-zClamps,zClamps);
        rb.MovePosition(newMovePos);
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        Debug.Log(movement.sqrMagnitude);
    }
}
