using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public GameObject hitEffect;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(11, 11, true);
        Physics2D.IgnoreLayerCollision(11, 9, true);
        if (demage == 0f) {
            demage = 50f;
        }
      
        StartCoroutine(liveTime());

    }

    public float demage;

    IEnumerator liveTime()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        Ghost ghost = collision.collider.GetComponent<Ghost>();
        if (ghost != null)
        {
            ghost.TakeDamage(demage);
        }

        Slime slime = collision.collider.GetComponent<Slime>();
        if (slime != null)
        {
            slime.TakeDamage(demage);
        }

        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject);
    }

}
