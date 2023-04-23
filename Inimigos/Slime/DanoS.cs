using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanoS : MonoBehaviour
{
    [Header("Referencias")]
    public Transform orientation;
    public float impulsoE;
    public float impulsoUp;
    public int slimeHP = 3;
    public GameObject hit;
    public float resetDelay = 10f;
    

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        hit.SetActive(false);
    }

    void Update()
    {
       if(slimeHP < 1)
       {
            Destroy(gameObject);
       }
    
    }
    
   private void OnCollisionEnter(Collision collision)
   {
        switch(collision.gameObject.tag)
        {
            case "Espada":
                hit.SetActive(true);
                //Vector3 forceToApply = orientation.forward * impulsoE + orientation.up * impulsoUp;
                //rb.AddForce(forceToApply, ForceMode.Impulse);
                slimeHP = slimeHP -1;
                Invoke("resetHit", resetDelay);
                break; 
        }
   }  

   void resetHit()
   {
        hit.SetActive(false);
   }    
}
