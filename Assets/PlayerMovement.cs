using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float playerSpeed = 3;  
    private float horizontal; 
    private float jumpPower = 7; 

    private bool canDash = true; 
    private bool isDashing = false; 
    private float dashTime = 0.2f; 
    private float dashingPower = 12f; 
    private float dashCooldown = 0.5f; 

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr; 
    [SerializeField] private SpriteRenderer sr; 
    [SerializeField] private GameObject gameObject; 
    [SerializeField] private Animator animator; 

    private GravityManager gravityManager = new GravityManager(); 
    
    private void Update()
    {   
        if(isDashing) 
            return; 

        horizontal = Input.GetAxisRaw("Horizontal");  

        if(Input.GetKeyDown(KeyCode.Space))
            rb.velocity = gravityManager.getJumpRelativeToGravity(rb, jumpPower);   

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            StartCoroutine(Dash()); 

        if(horizontal != 0) 
            animator.SetBool("isRunning", true); 
        else 
            animator.SetBool("isRunning", false); 
            
        gravityManager.flipRelativeToGravity(sr, horizontal, transform); 
        gravityManager.flipCameraRelativeToGravity(gameObject); 
             
    }

    private void FixedUpdate()
    {   
        if(isDashing) 
            return; 

        movePlayer(); 
    }
 
    private void movePlayer()
    { 
        rb.velocity = gravityManager.getVelocityRelativeToGravity(rb, horizontal * playerSpeed);       
    }

    private IEnumerator Dash()
    { 
        canDash = false; 
        isDashing = true; 
        float gravTemp = rb.gravityScale; 
        rb.gravityScale = 0.0f; 
        rb.velocity = gravityManager.getDashRelativeToGravity(sr, rb, dashingPower, transform);
        tr.emitting = true; 
        yield return new WaitForSeconds(dashTime); 
        tr.emitting = false; 
        rb.gravityScale = gravTemp; 
        isDashing = false; 
        yield return new WaitForSeconds(dashCooldown); 
        canDash = true; 

    }

    private void OnTriggerEnter2D(Collider2D collider)
    { 
        if(collider.tag == "Bottom")
            gravityManager.flipToBottom(); 
        else if(collider.tag == "Right")
            gravityManager.flipToRight(); 
        else if(collider.tag == "Top")
            gravityManager.flipToTop(); 
        else if(collider.tag == "Left")
            gravityManager.flipToLeft(); 
 
    }
}
