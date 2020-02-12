using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform AttackPoint;
    public float AttackRange = 0.0f;
    public LayerMask EnemyLayers;

    private AudioSource audio;
    public AudioClip attacksound;
    public AudioClip Attacknothing;

    public int AttackDmg = 2;
    public float Attackrate = 2f;
    float NextAttackTime = 0f;


    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= NextAttackTime)
        {

            if (Input.GetKeyDown(KeyCode.C))
            {
                Attack();
                audio.PlayOneShot(Attacknothing);
                NextAttackTime = Time.time + 1f / AttackRange;
            }
        }
    }

    void Attack()
    {
        //Animação
        animator.SetTrigger("Attack");

        //Detetarr o range do ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);

        //Dar dano ao inimigo
        foreach(Collider2D enemy in hitEnemies)
        {
            audio.PlayOneShot(attacksound);
            enemy.GetComponent<EnemyHealthAndAnimation>().TakeDmg(AttackDmg);
        }
    }

    //Desenhar uma area para ver na scene
    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
