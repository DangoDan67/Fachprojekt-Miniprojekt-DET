using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsContainer : MonoBehaviour
{
    public GameObject player;

    public GameObject speedItem;
    public GameObject healthItem;
    public GameObject bulletItem;
    public GameObject laserItem;
   


 
    public void createItem(Transform enemyPosition)
    {
        int value = GetRandomValue();
        if (value == 0)
        {
            GameObject Speed = Instantiate(speedItem, enemyPosition.position, Quaternion.identity);
            Speed.AddComponent<SpeedItem>().initialValue(player);
        }
        else if (value == 1)
        {
            GameObject health = Instantiate(healthItem, enemyPosition.position, Quaternion.identity);
            health.AddComponent<HealthItem>().initialValue(player);
        }
        else if (value == 2)
        {
            GameObject bullet = Instantiate(bulletItem, enemyPosition.position, Quaternion.identity);
            bullet.AddComponent<BulletItem>().initialValue(player);
        }
        else if (value == 3)
        {
            GameObject laser = Instantiate(laserItem, enemyPosition.position, Quaternion.identity);
            laser.AddComponent<LaserItem>().initialValue(player);
        }
        else 
        { 
        }
      
    }

    int GetRandomValue()
    {
       int[] values = {0,0,0,0,0, 1,1,1,1, 2,2,2 , 3,3,3,3,3, 4,4,4, 5,5,5,5,5, 6,6,6,6,6,6, 6,6,6,6,6,6,6,6,6};
        return values[Random.Range(0, values.Length)];
}
}
