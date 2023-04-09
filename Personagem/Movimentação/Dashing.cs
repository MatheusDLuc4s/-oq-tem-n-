using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
   [Header("References")]
   public Transform orientation;
   public Transform playerCam;
   private Rigidbody rb;
   private PlayerMovement pm;

   [Header("Dashing")]
   public float dashForce;
   public float dashUpwardForce;
   public float dashDuration;

   [Header("Cooldown")]
   public float dashCd;
   private float dashCdTimer;

   [Header("Input")]
   public KeyCode dashKey = KeyCode.E;
   public KeyCode jumpKey = KeyCode.Space;

   [Header("Jump")]
   public bool isOnGround = true;
   public float jumpForce = 10f;

   private void Start()
   {
    rb = GetComponent<Rigidbody>();
    pm = GetComponent<PlayerMovement>();
   }

   private void Update()
   {
        if(Input.GetKeyDown(dashKey))
        {
          Dash();
        }
        else if (dashCdTimer > 0)
        {
          dashCdTimer -= Time.deltaTime;
        }
        else if(Input.GetKeyDown(jumpKey) && isOnGround)
        {
          rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
          pm.jumping = true;
          pm.dashing = false;
          isOnGround = false;
        }
   }

   private void Dash()
   {
        if(dashCdTimer > 0) return;
        else dashCdTimer = dashCd;

        pm.dashing = true;
        
        Vector3 forceToApply = orientation.forward * dashForce + orientation.up * dashUpwardForce;

        delayForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);
        
        Invoke(nameof(ResetDash), dashDuration);   
   }

   private Vector3 delayForceToApply;

   private void DelayedDashForce()
   {
        rb.AddForce(delayForceToApply, ForceMode.Impulse);
   }

   private void ResetDash()
   {
        pm.dashing = false;
   }
   
   private void OnCollisionEnter(Collision collision)
   {
     if(collision.gameObject.tag == "Ground")
     {
          isOnGround = true;
          pm.jumping = false;
     }
   }
}
