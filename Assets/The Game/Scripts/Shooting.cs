using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public enum Weapon {LASER,BULLET};

    private int laserMagazineSize = 10;


    public Weapon currentWeapon = Weapon.LASER;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 50f;
    public float laserDemage = 50f;
    public float bulletDemage = 30f;
    
    float maxBulletDemage = 100f;
    float maxLaserDemage = 150f;
    public LayerMask mask;
    public LineRenderer lineRenderer;

    public int getLaserMagazineSize(){
        return laserMagazineSize;
    }

    public void setLaserMagazineSize(){
        laserMagazineSize = 10;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentWeapon == Weapon.BULLET || laserMagazineSize <= 0)
            {
                shootBullet();
            }
            else
            {
                StartCoroutine(shootLaser());
                laserMagazineSize--;
            }
        }
    }

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(11, 11, true);
        Physics2D.IgnoreLayerCollision(11, 9, true);
    }

    void shootBullet(){
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<Bullet>().demage = bulletDemage;
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    IEnumerator shootLaser()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.up, 100f, mask);

        if (hitInfo)
        {
            Ghost ghost = hitInfo.transform.GetComponent<Ghost>();
            if (ghost != null)
            {
                ghost.TakeDamage(laserDemage);
            }

            Slime slime = hitInfo.transform.GetComponent<Slime>();
            if (slime != null)
            {
                slime.TakeDamage(laserDemage);
            }
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.up * 100);
        }

        lineRenderer.enabled = true;

        yield return 0.1;

        lineRenderer.enabled = false;
    }
    public void changeWeapon(Weapon weapon)
    {
        this.currentWeapon = weapon;
    }

    public void upgradeBullet()
    {
        if (bulletDemage + 10f <= maxBulletDemage)
        {
            bulletDemage += 10f;
        }
        else {
            bulletDemage = maxBulletDemage;
        }
    }

    public void upgradeLaser()
    {
        if (laserDemage + 10f <= maxLaserDemage)
        {
            laserDemage += 10f;
        }
        else
        {
            laserDemage = maxLaserDemage;
        }
    }
}
