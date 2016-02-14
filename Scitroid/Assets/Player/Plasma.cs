using UnityEngine;
using System.Collections;

public class Plasma : MonoBehaviour
{
    float counter = 0;
    float killTimer = 0;
    int SpriteNum = 0;
    public Sprite[] sprites;
    SpriteRenderer thisSprite;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        thisSprite = GetComponent<SpriteRenderer>();
        thisSprite.sprite = sprites[0];
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Rigidbody2D>().velocity = new Vector2(player.transform.localScale.x * 3, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        counter += 1f * Time.deltaTime;
        if (counter > 0.05f)
        {
            counter = 0;
            SpriteNum += 1;
            if (SpriteNum >= sprites.Length) SpriteNum = 0;
            thisSprite.sprite = sprites[SpriteNum];
        }

        killTimer += 2f * Time.deltaTime;
        if (killTimer >= 2)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);

            collision.gameObject.GetComponent<Enemy>().health -= 30.0f * this.transform.localScale.x;

            if (player.GetComponent<Player>().RightFacing)
            {
                collision.gameObject.GetComponent<Enemy>().GetComponent<Rigidbody2D>().velocity = Vector2.right * 1.3f;
            }

            else
            {
                collision.gameObject.GetComponent<Enemy>().GetComponent<Rigidbody2D>().velocity = Vector2.left * 1.3f;
            }
        }
    }
}