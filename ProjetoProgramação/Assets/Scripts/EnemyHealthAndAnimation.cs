 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class EnemyHealthAndAnimation : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip die;

    public Animator animator;

    public int maxHealth = 1;

    public MonoBehaviour enemyController;

    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    public void TakeDmg(int damage)
    {
        currentHealth -= damage;

        if(currentHealth<=0)
        {
            Die();
        }
    }

    void Die()
    {
        enemyController.enabled = false;

        audio.PlayOneShot(die);
        //Animação de morrer
        animator.SetBool("IsDead", true);

        //Desativar o inimigo

        Collider2D[] list = GetComponents<Collider2D>();

        foreach(Collider2D co in list)
        {
            co.enabled = false;
        }

        GetComponent<Rigidbody2D>().gravityScale = -1;

    }

    void desaparecer()
    {
        Destroy(gameObject);
    }
}
