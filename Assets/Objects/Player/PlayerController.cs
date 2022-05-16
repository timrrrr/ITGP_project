using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;
    [SerializeField] private Animator animator;
    
    private Rigidbody Rb;
    //private Animator Animator;
    
    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }
    
    private Vector3 StepRotate(Vector3 desiredDirection)
    {
        float step = RotationSpeed * Time.fixedDeltaTime;
        // we can get current rotation from transform.forward
        Vector3 newRotation = Vector3.RotateTowards(transform.forward, desiredDirection, step, 0);
        return newRotation;
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 desiredDirection = new Vector3(horizontal, 0f, vertical).normalized;
        
        Vector3 lookRotation = StepRotate(desiredDirection);
        
        Debug.Log(Rb.velocity.magnitude);
        if (Rb.velocity.magnitude > 0.01f)
            Rb.rotation = Quaternion.LookRotation(lookRotation); 

        float OldVertVelocity = Rb.velocity.y;
        Vector3 velocity = desiredDirection * Speed;
        Rb.velocity = new Vector3(velocity.x, OldVertVelocity, velocity.z);

        //Debug.Log(Rb.velocity.magnitude);
        if (Rb.velocity.magnitude > 0.01f)
            animator.SetBool("IsWalking", true);
        else
            animator.SetBool("IsWalking", false);


    }
}
