using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItem : MonoBehaviour
{
    private GameObject player;

    public void initialValue(GameObject player)
    {
        this.player = player;
    }

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        Physics2D.IgnoreLayerCollision(9, 9, true);
        StartCoroutine(liveTime());
    }

    IEnumerator liveTime()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }



    public void addItem()
    {
        if (player.GetComponent<Shooting>().currentWeapon == Shooting.Weapon.BULLET)
        {
            player.GetComponent<Shooting>().upgradeBullet();
        }
        player.GetComponent<Shooting>().changeWeapon(Shooting.Weapon.BULLET);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.gameObject.tag == "Player")
        {
            addItem();
            Destroy(gameObject);
        }

    }
}
