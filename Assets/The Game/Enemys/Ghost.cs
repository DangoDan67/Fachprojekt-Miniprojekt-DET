using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float health = 300f;
    public float hitForce = 30f;
    GameObject score;
    GameObject itemsContainer;
    float hideTimer;
    float timeInHideModus;
    public SpriteRenderer sprite;
    public BoxCollider2D boxCollider;


    private void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 12, true);
        Physics2D.IgnoreLayerCollision(9, 12, true);
        Physics2D.IgnoreLayerCollision(8, 13, true);
        Physics2D.IgnoreLayerCollision(9, 13, true);
        Physics2D.IgnoreLayerCollision(11, 13, true);
        this.score = GetComponent<EnemyBehavior>().score;
        this.itemsContainer = GetComponent<EnemyBehavior>().itemsContainer;
        this.health = health + GetComponent<EnemyBehavior>().addHealth;
        this.hitForce = hitForce + GetComponent<EnemyBehavior>().addHitForce;
        hideTimer = 3;
        timeInHideModus = 2;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        score.GetComponent<score>().addScore();
        itemsContainer.GetComponent<ItemsContainer>().createItem(transform);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.gameObject.tag == "Player")
        {
            collision.collider.GetComponent<playerHealth>().subtractHealth(hitForce);
        }
    }
    private void Update()
    {
       StartCoroutine (hideGhost());
    }

    IEnumerator hideGhost() {
        if (hideTimer <= 0)
        {     
            sprite.color = new Color(1f, 1f, 1f, .2f); 
            hideTimer = Random.Range(3, 10);
            timeInHideModus = Random.Range(1, 4);
            gameObject.layer = 13;
            yield return new WaitForSeconds(timeInHideModus); 
            sprite.color = new Color(1f, 1f, 1f, 1f);   
            gameObject.layer = 12;

        }
        else
        {

            hideTimer -= Time.deltaTime;
     

        }
        
    }
}
