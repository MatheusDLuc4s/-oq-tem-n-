using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting : MonoBehaviour
{
    [Header("References")]
    private PlayerMovement pm;
    public Transform cam;
    public LayerMask whatIsTeleportable;
    public GameObject personagem;
           
    [Header("Teleporting")]
    public float maxTeleportDistance;
    private Vector3 teleportPoint;

    // botao do mouse
    [Header("Input")]
    public KeyCode teleportKey = KeyCode.Mouse1;

    private bool teleporting;

    private void Start()
    {
       pm = GetComponent<PlayerMovement>();
       teleporting = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(teleportKey) && teleporting == false) StartTeleport();              
    }

    private void StartTeleport()
    {
        teleporting = true; 

        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, maxTeleportDistance, whatIsTeleportable))
        {
            pm.freeze = true;
            teleportPoint = hit.point;
            gameObject.transform.position = teleportPoint;
            //alterar cooldown do teleport
            Invoke(nameof(EndTeleport), .5f);
           
        }
        else
        {
            teleportPoint = cam.position + cam.forward * maxTeleportDistance;
            Invoke(nameof(EndTeleport), 0.025f);
        
        }
    }

    public void EndTeleport()
    {
        pm.freeze = false;
        teleporting = false;
        
    }
}

    
