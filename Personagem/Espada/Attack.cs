using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator anim;
    [Header("Setup")]
    public GameObject espada;
    public GameObject dano;
    public GameObject danoAr;
    public GameObject danoDash;
    public GameObject AuraAir;
    public GameObject AuraDash;
    public float cooldown = 2f; 
    private float nextFireTime = 0f;
    public static int nClics = 0;
    float lastclickedTime = 0;
    float maxComboDelay = 1;
    private PlayerMovement pm;
    Rigidbody rb;
    

    [Header("Input")]
    public KeyCode atkKey = KeyCode.Mouse0;

    [Header("Special attk")]
    public float airForceUp = 10f;
    public float airForceDown = 10f;
    public float AirDelay = 10f;
    public float ExlposionDelay = 10f;
    public float ResetAirDelay = 10f;
    public float dashForceUp = 10f;
    public float dashDelay = 10f;
    public float freezeDelay = 10f;
    public float resetDashDelay = 10f;
    


    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        espada.SetActive(false);
        danoAr.SetActive(false);
        danoDash.SetActive(false);
        AuraAir.SetActive(false);
        AuraDash.SetActive(false);
        dano.SetActive(false);
        pm = GetComponent<PlayerMovement>();
        nClics = 0;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    void Update()
    {
        if(pm.jumping == true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                rb.AddForce(new Vector3(0, airForceUp, 0), ForceMode.Impulse);
                espada.SetActive(true);
                AuraAir.SetActive(true);
                Invoke("AirFreeze", AirDelay);
            }   
        }

        else if(pm.dashing == true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                espada.SetActive(true);
                AuraDash.SetActive(true);
                danoDash.SetActive(true);
                pm.freeze = true;
                anim.SetBool("dashatk", true);
                Invoke("DashAttack", dashDelay);
               
            }

        }
        else
        {
            if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldown && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
            {
                anim.SetBool("hit1", false);
            }
            //if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldown && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
            //{
            //    anim.SetBool("hit2", false);
            //}
            //if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldown && anim.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
            //{
            //    anim.SetBool("hit3", false);
            //    nClics = 0;
            //}

            if(Time.time - lastclickedTime > maxComboDelay)
            {
                espada.SetActive(false);
                dano.SetActive(false);
                pm.freeze = false;
                nClics = 0;
            }
            if(Time.time > nextFireTime)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    OnClic();
                }
            }
        }
    }

    void OnClic()
    {
        lastclickedTime = Time.time;
        nClics++;
        if(nClics == 1)
        {
            espada.SetActive(true);
            dano.SetActive(true);
            pm.freeze = true;
            anim.SetBool("hit1", true);   
        }
        nClics = Mathf.Clamp(nClics, 0, 3);

        //if(nClics == 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldown && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        //{
        //    anim.SetBool("hit1", false);
        //    anim.SetBool("hit2", true);
        //}
        //if(nClics >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldown && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        //{
        //    anim.SetBool("hit2", false);
        //    anim.SetBool("hit3", true);
        //}
    }

    void AirAttack()
    {
        pm.freeze = false;
        rb.AddForce(new Vector3(0, airForceDown, 0), ForceMode.Impulse);
        Invoke("Explosion", ExlposionDelay);
    }

    void Explosion()
    {
        danoAr.SetActive(true);
        AuraAir.SetActive(false);
        Invoke("ResetAirAttack", ResetAirDelay);
    }

    void ResetAirAttack()
    {
        espada.SetActive(false);
        danoAr.SetActive(false);
        anim.SetBool("airatk", false);   
    }

    void AirFreeze()
    {
        pm.freeze = true;
        anim.SetBool("airatk", true);
        Invoke("AirAttack", AirDelay);
    }

    void DashAttack()
    {
        pm.freeze = false;
        pm.dashing = true;
        rb.AddForce(new Vector3(dashForceUp, 0, 0), ForceMode.Impulse);
        Invoke("DashFreeze", freezeDelay);
        

    }

    void DashFreeze()
    {
        pm.freeze = true;
        pm.dashing = false;
        anim.SetBool("dashatk", false);
        danoDash.SetActive(false);
        Invoke("DashReset", resetDashDelay);
        
    }

    void DashReset()
    {
        pm.freeze = false;
        AuraDash.SetActive(false);
        espada.SetActive(false);
    }

}
