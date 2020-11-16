using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject score;
    public GameObject itemsContainer;
    public float addHealth = 0;
    public float addHitForce = 0;


    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(10, 9, true);
        Physics2D.IgnoreLayerCollision(10, 10, true);
    }
    public void initialValue(GameObject score, GameObject itemsContainer, float addHealth, float addHitForce)
    {
        this.score = score;
        this.itemsContainer = itemsContainer;
        this.addHealth = addHealth;
        this.addHitForce = addHitForce;
    }
}
