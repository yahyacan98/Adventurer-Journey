using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilbossWizard : MonoBehaviour
{
    [SerializeField] GameObject kalkan, BossWall;
    [SerializeField] Transform insPoint;
    [SerializeField] GameObject Fireball;
    [SerializeField] Transform FireballPos;

    AventurerMove Adventurer;

    int stageOfEnemies = 0;

    [SerializeField] GameObject stage1Enemie;
    [SerializeField] GameObject stage2Enemie;
    [SerializeField] GameObject stage3Enemie;


    bool isVunuriable = false;
    bool isShieldOn = false;
    bool PattarnRecall = true;
    bool BossWallactive = false;

    Animator wizardAnimator;
    Vector2 Vectorplus = new Vector2(5f, 0);

    void Start()
    {
        BossWall = GameObject.Find("BossWall");
        Adventurer = FindObjectOfType<AventurerMove>();
        wizardAnimator = GetComponent<Animator>();
        BossWall.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(Adventurer.transform.position, transform.position) < 30f)
        {
            if (Vector2.Distance(Adventurer.transform.position, transform.position) < 24f && !BossWallactive)
            {
                BossWallactive = true;
                BossWall.SetActive(true);
            }
            if (PattarnRecall)
            {
                StartCoroutine(pattern());
            }
        }
    }


    void FireballAttack()
    {
        wizardAnimator.SetBool("Attack", true);
    }

    void Shield()
    {
        if (!isShieldOn)
        {

            kalkan.SetActive(true);
            isShieldOn = true;
        }
        else
        {
            kalkan.SetActive(false);
            isShieldOn = false;
        }

    }

    void CrateEnemie()
    {
        wizardAnimator.SetBool("Attack", false);
        if (stageOfEnemies == 0)
        {
            Instantiate(stage1Enemie, insPoint.position, Quaternion.identity);
            stageOfEnemies++;
        }
        else if (stageOfEnemies == 1)
        {
            Instantiate(stage2Enemie, insPoint.position, Quaternion.identity);
            stageOfEnemies++;

        }
        else if (stageOfEnemies == 2)
        {
            Instantiate(stage3Enemie, insPoint.position, Quaternion.identity);
            Instantiate(stage3Enemie, insPoint.position+Vector3.left*2, Quaternion.identity);
            stageOfEnemies = 0;
        }
    }

    IEnumerator pattern()
    {
        PattarnRecall = false;

        FireballAttack();
        Shield();
        yield return new WaitForSeconds(10f);

        CrateEnemie();

        yield return new WaitForSeconds(10f);

        Shield();



        yield return new WaitForSeconds(10f);

        PattarnRecall = true;

    }

    public void FireBalll()
    {


        Instantiate(Fireball, FireballPos.position, Quaternion.identity);

    }

    void PatternCaller()
    {

        StartCoroutine(pattern());
    }

}
