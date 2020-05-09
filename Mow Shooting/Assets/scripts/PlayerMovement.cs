using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class PlayerMovement : MonoBehaviour
{
    [Header("Camera")]
    public Camera mainCamera;

    [Header("Movement")]
    public float speed = 4.5f;



    [Header("Animation")]
    public Animator playerAnimator;

    Rigidbody playerRigidbody;

    private Transform _mainCameraTransform;
    // Start is called before the first frame update
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        _mainCameraTransform = Camera.main.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        var inputVector = new Vector3(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"));
        Vector3 inputDirection = inputVector;
        Vector3 movementVector = Vector3.zero;
        if (inputVector.sqrMagnitude > 0.001f)
        {
            movementVector = _mainCameraTransform.TransformDirection(inputVector);

            movementVector.y = 0f;
            movementVector.Normalize(); 
        }

        movementVector += Physics.gravity;
        MoveThePlayer(movementVector);
        AnimateThePlayer(movementVector);
        //Quaternion newRotation = Quaternion.LookRotation(inputVector);
        //newRotation.x = 0;
        //newRotation.z = 0;
        //newRotation.Normalize();
        //var newRotation = inputVector;
        //newRotation.z = 0;
        //transform.rotation = Quaternion.LookRotation(newRotation);
    }

    void MoveThePlayer(Vector3 desiredDirection)
    {
        Vector3 movement = new Vector3(desiredDirection.x, 0f, desiredDirection.z);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }
    void AnimateThePlayer(Vector3 desiredDirection)
    {
        if (!playerAnimator)
            return;

        Vector3 movement = new Vector3(desiredDirection.x, 0f, desiredDirection.z);
        float forw = Vector3.Dot(movement, transform.forward);
        float stra = Vector3.Dot(movement, transform.right);

        playerAnimator.SetFloat("Forward", forw);
        playerAnimator.SetFloat("Strafe", stra);
    }
}
