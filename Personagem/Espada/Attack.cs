using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator anim;
    [Header("Setup")]
    public GameObject espada;
    public GameObject dano;
    public float cooldown = 2f; 
    private float nextFireTime = 0f;
    public static int nClics = 0;
    float lastclickedTime = 0;
    float maxComboDelay = 1;
    private PlayerMovement pm;

    [Header("Input")]
    public KeyCode atkKey = KeyCode.Mouse0;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        espada.SetActive(false);
        dano.SetActive(false);
        pm = GetComponent<PlayerMovement>();
        nClics = 0;
    }

    // Update is called once per frame

    void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldown && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            anim.SetBool("hit1", false);
        }
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldown && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            anim.SetBool("hit2", false);
        }
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldown && anim.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        {
            anim.SetBool("hit3", false);
            nClics = 0;
        }

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

        if(nClics == 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldown && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            anim.SetBool("hit1", false);
            anim.SetBool("hit2", true);
        }
        if(nClics >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldown && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            anim.SetBool("hit2", false);
            anim.SetBool("hit3", true);
        }
    }
}
