using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class GuardaDoRei : MonoBehaviour
{
    [SerializeField]
    Transform Player;

    [SerializeField]
    float moveSpeed = 0.0f;

    [SerializeField]
    float agroRange = 0.0f;

    Rigidbody2D RB;

    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distToplayer = Vector2.Distance(transform.position, Player.position);

        if (distToplayer < agroRange)
        {
            chasePlayer();
        }
        else
        {
            stopChasingPlayer();
        }
        void chasePlayer()
        {
            if (transform.position.x < Player.position.x)
            {
                //O inimigo está á esquerda do jogador então segue para a direita
                RB.velocity = new Vector2(moveSpeed, 0);
            }
            else
            {
                //O inimigo está á direita do jogador então segue para a esquerda
                RB.velocity = new Vector2(-moveSpeed, 0);
                
                RB.isKinematic = false;
            }
        }


        void stopChasingPlayer()
        {
            RB.velocity = new Vector2(0, 0);
        }
    }
}