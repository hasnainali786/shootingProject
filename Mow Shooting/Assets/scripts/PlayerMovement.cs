using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 6f;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    CharacterController controller;


    [Header("Animation")]
    public Animator playerAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Inputs
        float Horizontal = CnInputManager.GetAxis("Horizontal");
        float Vertical = CnInputManager.GetAxis("Vertical");

        //Movement
        Vector3 direction = new Vector3(Horizontal, 0f, Vertical).normalized;
        if(direction.magnitude>=0.001f)
        {
            if (!AimingTowardsEnemy.instance.isInRange)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
            controller.Move(direction * speed * Time.deltaTime);
            AnimateThePlayer(direction);
        }
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
