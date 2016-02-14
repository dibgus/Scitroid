using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {
    GameObject player;
    Vector3 pos;


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        pos = player.transform.position;
        transform.localScale = player.transform.localScale;
        if (player.GetComponent<SpriteRenderer>().sprite.name == "Punch0")
        {
            pos.x += 0.14f * player.transform.localScale.x;
            pos.y -= 0.04f;
        }
        else if (player.GetComponent<SpriteRenderer>().sprite.name == "Punch1")
        {
            pos.x += 0.17f * player.transform.localScale.x;
            pos.y -= 0.03f;
        }
        else if (player.GetComponent<SpriteRenderer>().sprite.name == "Punch2")
        {
            pos.x += 0.19f * player.transform.localScale.x;
            pos.y -= 0.04f;
        }
        else
        {
            Destroy(gameObject);
        }
        transform.position = pos;
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().health -= 60.0f;
            if (player.GetComponent<Player>().RightFacing)
            {
                collision.gameObject.GetComponent<Enemy>().GetComponent<Rigidbody2D>().velocity = Vector2.right * 2;
            }
            else
            {
                collision.gameObject.GetComponent<Enemy>().GetComponent<Rigidbody2D>().velocity = Vector2.left * 2;
            }
        }
    }
}
