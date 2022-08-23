﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class martialBoxxEnemy : MonoBehaviour
{

    [SerializeField] float Speed;
    [SerializeField] float SaldırıMenzili;
    [SerializeField] float farketmemenzili;
    AdventurerHealth Adventurer;
    Rigidbody2D Rb;
    Animator Anime;

    Vector3 LocalScale;

    bool CanRun = true;
    bool CanAttack = true;
    [SerializeField] Transform AttackPoint;
  [SerializeField]   bool İsAttacking = false;

    [SerializeField] int attacCounter = 0;

    void Start()
    {
        LocalScale = gameObject.transform.localScale;
        Rb = GetComponent<Rigidbody2D>();
        Adventurer = FindObjectOfType<AdventurerHealth>();
        Anime = GetComponent<Animator>();
        İsAttacking = false;
    }

    private void Update()
    {
        if (Adventurer.transform.position.x - transform.position.x > 0)
        {
            FlipRight();
        }
        else
        {
            FlipLeft();
        }

    }
    void FixedUpdate()
    {
        if (Vector2.Distance(Adventurer.transform.position, transform.position) < farketmemenzili && Vector2.Distance(Adventurer.transform.position, transform.position) >= SaldırıMenzili && CanRun && Mathf.Abs((100 - Adventurer.transform.position.y) - (100 - transform.position.y)) <= 3f)
        {
         
            if (!İsAttacking)
            {
                Vector2 target = new Vector2(Adventurer.transform.position.x, Rb.position.y);
                Vector2 MovePos = Vector2.MoveTowards(Rb.position, target, Speed * Time.fixedDeltaTime);
                Rb.MovePosition(MovePos);
                Anime.SetBool("Running", true);
            }

        }
        else
        {
            if (CanRun)
            {
                Anime.SetBool("Running", false);
            }
        }

        if (Vector2.Distance(Adventurer.transform.position, transform.position) <= SaldırıMenzili && CanAttack)
        {
            CanRun = false;
            StandartAttacks();

            if (attacCounter == 3)
            {
                attacCounter = 0;
                SpecialAttacks();
            }
        }
        else
        {
            CanRun = true;
        }

    }

    void StandartAttacks()
    {
        attacCounter++;
        Anime.Play("attackstandart1");
        İsAttacking = true;
        CanAttack = false;
    }

    void SpecialAttacks()
    {
        attacCounter++;
        İsAttacking = true;
        Anime.Play("SpecialAttack");
        CanAttack = false;
    }

    private void FlipRight()
    {
        transform.localScale = new Vector3(LocalScale.x, LocalScale.y, LocalScale.z);
    }

    private void FlipLeft()
    {
        transform.localScale = new Vector3(-LocalScale.x, LocalScale.y, LocalScale.z);
    }

    IEnumerator AttackWaitTime()
    {
        var rand = Random.Range(0.5f, 2.5f);
        yield return new WaitForSeconds(rand);
        CanAttack = true;
        İsAttacking = false;
    }

    public void AllertObserver(string Komut)
    {

        if (Komut == "AttackEnd")
        {
            İsAttacking = false;
            CanAttack = true;
            if ((Vector2.Distance(Adventurer.transform.position, AttackPoint.transform.position) <= SaldırıMenzili))
            {
                if (attacCounter == 0)
                {
                    Adventurer.TakeDamage(30);
                    
                    StartCoroutine(AttackWaitTime());
                }
                else
                {
                    Adventurer.TakeDamage(10);
                    
                    StartCoroutine(AttackWaitTime());
                }
            }
        }


        if (Komut == "Attack1End")
        {
            İsAttacking = false;
            CanAttack = true;
            if ((Vector2.Distance(Adventurer.transform.position, AttackPoint.transform.position) <= SaldırıMenzili))
            {
                Adventurer.TakeDamage(7);
                İsAttacking = false;
                StartCoroutine(AttackWaitTime());
            }

        }


        if (Komut == "TakeDamageEnd")
        {
            İsAttacking = false;
            CanAttack = true;

        }
                
         }


}
