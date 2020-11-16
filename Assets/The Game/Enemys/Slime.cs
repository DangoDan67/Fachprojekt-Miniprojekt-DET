using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Slime : MonoBehaviour
{
    private const float Y = 1f;
    public float health = 200f;
    public float hitForce = 20f; 
    //public AIPath aiPath;
    GameObject score;
    GameObject itemsContainer;
   


 

    private void Start()
    {
        this.score = GetComponent<EnemyBehavior>().score;
        this.itemsContainer = GetComponent<EnemyBehavior>().itemsContainer;
        this.health = health + GetComponent<EnemyBehavior>().addHealth;
        this.hitForce = hitForce + GetComponent<EnemyBehavior>().addHitForce;
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

    void Update()
    {
        //if(aiPath.desiredVelocity.x >= 0.01f){
        //    transform.localScale = new Vector3(-1f, 1f, 1f);
        //}
        //else if(this.aiPath.desiredVelocity.x <= -0.01f){
        //    transform.localScale = new Vector3(1f, 1f, 1f);
        //}
       
    }
}

