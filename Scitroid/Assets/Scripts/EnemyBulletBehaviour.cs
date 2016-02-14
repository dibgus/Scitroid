using UnityEngine;
using System.Collections;

public class EnemyBulletBehaviour : MonoBehaviour
{

    // Use this for initialization
    private Player player;
    public int bulletLifeTime = 10000;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        /*bulletLifeTime -= 1;
        if (bulletLifeTime < 0)
        {
            Destroy(gameObject);
            print("Bullet eaten by satan");
        }*/
    }

    /*
    void setBulletLifeTime(int life)
    {
        bulletLifeTime = life;
    }*/

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.health -= 40;
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Wall")
            Destroy(gameObject);
    }
}
