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

   private void Start()
   {
    rb = GetComponent<Rigidbody>();
    pm = GetComponent<PlayerMovement>();
   }

   private void Update()
   {
        if(Input.GetKeyDown(dashKey))
            Dash();
        
        if (dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;
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
}
