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
    public int upgrade;
    public float baseDamage;
    public float basePlasmaBlastDamage;
    public float damageMultiplier;

    // Use this for initialization
    void Start()
    {
        thisSprite = GetComponent<SpriteRenderer>();
        thisSprite.sprite = sprites[0];
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Rigidbody2D>().velocity = new Vector2(player.transform.localScale.x * 3, 0.0f);
        upgrade = player.GetComponent<Player>().plasmaUpgrade;
        baseDamage = 30.0f;
        basePlasmaBlastDamage = 60.0f;
        if (upgrade == 4) damageMultiplier = 1.25f;
        else damageMultiplier = 1.0f;
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

            if (this.transform.localScale.x == 1) collision.gameObject.GetComponent<Enemy>().health -= baseDamage;
            else collision.gameObject.GetComponent<Enemy>().health -= basePlasmaBlastDamage * damageMultiplier;

            if (upgrade == 3 && this.transform.localScale.x==2)
            {
                collision.gameObject.GetComponent<Enemy>().stunTime = 2;
            }

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