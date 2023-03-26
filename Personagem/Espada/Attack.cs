using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("References")]
    public KeyCode attackKey = KeyCode.Mouse0;
    public GameObject sword;
    public GameObject personagem;
    public float timeBetweenAttacks;
    

    private bool attacking;

    private void Update()
    {
       if(Input.GetKeyDown(attackKey) && attacking == false) StartAttack();              
    }

    private void StartAttack()
    {
        
        attacking = true;
        print("atacou");
        
        Rigidbody rb = Instantiate(sword, personagem.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        Destroy(gameObject);
        print("destruiu original");
                
        Invoke(nameof(ResetAttack), timeBetweenAttacks);

    }

    private void ResetAttack()
   {
     Destroy(gameObject);
     print("destruiu copia");
     attacking =false;  
   }

}
