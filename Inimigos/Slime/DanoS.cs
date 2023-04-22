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
    

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
       if(slimeHP == 0)
       {
            Destroy(gameObject);
       }
    
    }
    
   private void OnCollisionEnter(Collision collision)
   {
        switch(collision.gameObject.tag)
        {
            case "Espada":
                print("espada colidiu");
                break;
            
            

        }
   }
   private void OnCollisionExit(Collision collision)
   {
        switch(collision.gameObject.tag)
        {
            case "Espada":
                print("espada desapareceu");
                Vector3 forceToApply = orientation.forward * impulsoE + orientation.up * impulsoUp;
                rb.AddForce(forceToApply, ForceMode.Impulse);
                slimeHP = slimeHP -1;
                break;
            
            

        }
   }           
}
