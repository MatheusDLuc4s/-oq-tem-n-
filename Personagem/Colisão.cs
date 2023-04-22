using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Colisão : MonoBehaviour
{
    [Header("Referencias")]
    public Transform orientation;
    public float impulsoI;
    public float impulsoP;
    public float impulsoCima;
    public int playerHP = 100;
    public Slider healthBar;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        healthBar.value = playerHP;
        if (playerHP == 0)
        {
           SceneManager.LoadScene("Title Screen");
        }
    }
    
   private void OnCollisionEnter(Collision collision)
   {
        switch(collision.gameObject.tag)
        {
            case "Slime":
                Vector3 forceToApply = orientation.forward * impulsoI + orientation.up * impulsoCima;
                //rb.AddForce(forceToApply, ForceMode.Impulse);
                //playerHP = playerHP -10;
                break;
            
            

        }
   }       
}
