using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GravityManager
{   

    private String grav = "Bottom"; 

    public void flipToBottom()
    { 
        grav = "Bottom"; 
        Physics2D.gravity = new Vector2(0.0f, -9.8f); 
    }

    public void flipToRight()
    { 
        grav = "Right"; 
        Physics2D.gravity = new Vector2(9.8f, 0.0f); 
    }

    public void flipToTop()
    { 
        grav = "Top"; 
        Physics2D.gravity = new Vector2(0.0f, 9.8f); 
    }

    public void flipToLeft()
    { 
        grav = "Left"; 
        Physics2D.gravity = new Vector2(-9.8f, 0.0f); 
    }

    public Vector2 getVelocityRelativeToGravity(Rigidbody2D rb, float movementSpeed)
    { 
        if(grav == "Bottom")
            return new Vector2(movementSpeed, rb.velocity.y); 
        else if(grav == "Right")
            return new Vector2(rb.velocity.x, movementSpeed); 
        else if(grav == "Top")
            return new Vector2(-movementSpeed, rb.velocity.y); 
        else if(grav == "Left")
            return new Vector2(rb.velocity.x, -movementSpeed); 
        else 
            return new Vector2(rb.velocity.x, rb.velocity.y); 
    }

    public Vector2 getJumpRelativeToGravity(Rigidbody2D rb, float jumpPower)
    { 
         if(grav == "Bottom")
            return new Vector2(rb.velocity.x, jumpPower); 

        else if(grav == "Right")
            return new Vector2(-jumpPower, rb.velocity.y); 

        else if(grav == "Top")
            return new Vector2(rb.velocity.x, -jumpPower); 

        else if(grav == "Left")
            return new Vector2(jumpPower, rb.velocity.y); 

        else  
            return new Vector2(rb.velocity.x, rb.velocity.y); 
    }

    public void flipRelativeToGravity(SpriteRenderer sr, float horizontal, Transform transform)
    {   
        if(grav == "Bottom") { 
            transform.rotation = new quaternion(0,0,0,0);
            if(horizontal < 0) 
                sr.flipX = false;
            else if(horizontal > 0)
                sr.flipX = true;  
        }

        else if(grav == "Right") { 
            transform.rotation = new quaternion(0,0,90,90); 
            if(horizontal > 0) 
                sr.flipX = true;
            else if(horizontal < 0)
                sr.flipX = false;  
        }
        
        else if(grav == "Top") { 
            transform.rotation = new quaternion(0,0,180,0); 
            if(horizontal > 0) 
                sr.flipX = true;
            else if(horizontal < 0)
                sr.flipX = false;  
        }

        else if(grav == "Left") { 
            transform.rotation = new quaternion(0,0,-90,90); 
            if(horizontal > 0) 
                sr.flipX = true;
            else if(horizontal < 0)
                sr.flipX = false;  
        }
    }

    public void flipCameraRelativeToGravity(GameObject gameObject) 
    { 
        if(grav == "Bottom")
            gameObject.transform.rotation = new Quaternion(0,0,0,0); 

        else if(grav == "Right")
            gameObject.transform.rotation = new Quaternion(0,0,90,90); 

        else if(grav == "Top")
            gameObject.transform.rotation = new Quaternion(0,0,180,0); 

        else if(grav == "Left")
            gameObject.transform.rotation = new Quaternion(0,0,-90,90); 
    }

    public Vector2 getDashRelativeToGravity(SpriteRenderer sr, Rigidbody2D rb, float dashingPower, Transform transform)
    { 
        if(grav == "Bottom") { 
            if(sr.flipX)
                return new Vector2(transform.localScale.x * dashingPower, 0.0f); 
            else 
                return new Vector2(transform.localScale.x * -dashingPower, 0.0f); 
        }

        else if(grav == "Right") { 
            if(sr.flipX)
                return new Vector2(0.0f, transform.localScale.y * dashingPower); 
            else 
                return new Vector2(0.0f, transform.localScale.y * -dashingPower);
        }

        else if(grav == "Top") { 
            if(sr.flipX)
                return new Vector2(transform.localScale.x * -dashingPower, 0.0f); 
            else 
                return new Vector2(transform.localScale.x * dashingPower, 0.0f); 
        }

        else if(grav == "Left") { 
            if(sr.flipX)
                return new Vector2(0.0f, transform.localScale.y * -dashingPower); 
            else 
                return new Vector2(0.0f, transform.localScale.y * dashingPower);
        }
        
        else 
            return new Vector2(rb.velocity.x, rb.velocity.y);

    }
     
}
