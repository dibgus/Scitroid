using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public Sprite[] enemyWalking;
    public SpriteRenderer basicEnemy;
    int spriteCounter;
    float timer;
    Vector3 scale;
    int groundLayer;

    public float health;
    public int damage;
    public bool right;
    bool isJumping;

	// Use this for initialization
	void Start () {
        spriteCounter = 0;
        timer = 0;
        scale = transform.lossyScale;
        right = false;
        basicEnemy.sprite = enemyWalking[0];
        health = 100;
        damage = 10;
        groundLayer = 1 << 10;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        Jump();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Move()
    {
        Vector2 originLeft = new Vector2(transform.position.x - .09f, transform.position.y);
        Vector2 originRight = new Vector2(transform.position.x + .09f, transform.position.y);

        RaycastHit2D hitLeft = Physics2D.Raycast(originLeft, Vector2.down, Mathf.Infinity, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(originRight, Vector2.down, Mathf.Infinity, groundLayer);

        if (hitRight.collider == null && right)
        {
            right = false;
        }

        if (hitLeft.collider == null && !right)
        {
            right = true;
        }

        if (!right)
        {
            transform.position += Vector3.left * .011f;
        }

        if (right)
        {
            transform.position += Vector3.right * .011f;
        }

        timer += Time.deltaTime;

        if (timer > 0.2f)
        {
            spriteCounter++;
            timer = 0;

            if (spriteCounter >= enemyWalking.Length)
            {
                spriteCounter = 0;
            }

            basicEnemy.sprite = enemyWalking[spriteCounter];

            if (right)
            {
                basicEnemy.transform.localScale = scale;
            }

            else
            {
                basicEnemy.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            }
        }
    }
    
    void Jump()
    {
        Vector2 originUp = new Vector2(transform.position.x, transform.position.y);

        RaycastHit2D hitLeft = Physics2D.Raycast(originUp, Vector2.left, Mathf.Infinity, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(originUp, Vector2.right, Mathf.Infinity, groundLayer);

        if (!isInAir() && ((hitRight.collider != null && hitRight.distance <= .2) || (hitLeft.collider != null && hitLeft.distance <= .2)))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * 3;
        }
    }

    bool isInAir()
    {
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, groundLayer);

        if (hitDown.distance >= .11f)
        {
            return true;
        }

        return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().health -= 5;

            if (collision.gameObject.GetComponent<Player>().RightFacing && right || collision.gameObject.GetComponent<Player>().RightFacing && !right)
            {
                collision.gameObject.GetComponent<Player>().GetComponent<Rigidbody2D>().velocity = new Vector3(-1, 1) * 1.3f;
                this.GetComponent<Rigidbody2D>().velocity = new Vector3(1, 1) * 1.3f;
            }

            else if (!collision.gameObject.GetComponent<Player>().RightFacing && right || !collision.gameObject.GetComponent<Player>().RightFacing && !right)
            {
                collision.gameObject.GetComponent<Player>().GetComponent<Rigidbody2D>().velocity = new Vector3(1, 1) * 1.3f;
                this.GetComponent<Rigidbody2D>().velocity = new Vector3(-1, 1) * 1.3f;
            }
        }
    }
}
