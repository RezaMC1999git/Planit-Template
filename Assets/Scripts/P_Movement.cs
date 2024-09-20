using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_Movement : MonoBehaviour
{
    public static P_Movement instance;
    [SerializeField]
    float walkSpeed = 2f, runSpeed = 2f;

    public Animator playerAnimator;
    public Rigidbody2D rigidbody;
    public Transform playerTransform;
    [HideInInspector] public bool canMove;

    float horizontalMovement, verticalMovement, movementSpeed;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        canMove = true;
    }

    private void Update()
    {
        if (canMove) 
        {
            if (Input.GetKey(KeyCode.LeftShift))
                movementSpeed = runSpeed;
            else
                movementSpeed = walkSpeed;

            float walkSpeedX = movementSpeed * horizontalMovement;
            float walkSpeedY = movementSpeed * verticalMovement;
            rigidbody.velocity = new Vector2(walkSpeedX, walkSpeedY);
            if (walkSpeedX < 0)
                playerTransform.transform.localScale = new Vector3(-1, 1, 1);
            else if (walkSpeedX > 0)
                playerTransform.transform.localScale = new Vector3(1, 1, 1);
        }

    }

    public void Move(InputAction.CallbackContext value)
    {
        if (!canMove)
            return;
        horizontalMovement = value.ReadValue<Vector2>().x;
        verticalMovement = value.ReadValue<Vector2>().y;
        if (value.phase == InputActionPhase.Started || value.phase == InputActionPhase.Performed)
        {
            playerAnimator.SetBool("Walk", true);
        }
        else if (value.phase == InputActionPhase.Canceled)
        {
            playerAnimator.SetBool("Walk", false);
        }
    }
}
