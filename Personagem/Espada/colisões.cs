using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colisões : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
   {
        switch(collision.gameObject.tag)
        {
            case "Slime":
                gameObject.transform.position = gameObject.transform.position * 100;
                break;
            
            

        }
   }    
}
