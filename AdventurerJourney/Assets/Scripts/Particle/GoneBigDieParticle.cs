using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoneBigDieParticle : MonoBehaviour
{
    Vector3 DefaultLocalScale;
    [SerializeField] float Speed = 10f;
    
    void Start()
    {
        DefaultLocalScale = transform.localScale;
    }

    
    void FixedUpdate()
    {
        transform.Translate(Vector2.up*Speed / 200);
        StartCoroutine(Splash());

    }

    IEnumerator Splash()
    {

        transform.localScale += DefaultLocalScale/600f;
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
           

    }

}
