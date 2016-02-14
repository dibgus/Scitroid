using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public Sprite[] enemyWalking;
    public SpriteRenderer basicEnemy;
    int spriteCounter = 0;
    float timer = 0;
    Vector3 scale;
    int groundLayer = 1 << 10;

    int health = 100;
    int damage = 10;
    bool right;
    bool isJumping;

	// Use this for initialization
	void Start () {
        scale = transform.lossyScale;
        right = false;
        basicEnemy.sprite = enemyWalking[0];
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        
        if (!isInAir())
        Jump();
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

        if ((hitRight.collider != null && hitRight.distance <= basicEnemy.sprite.bounds.size.x / 2 + .1) || (hitLeft.collider != null && hitLeft.distance <= basicEnemy.sprite.bounds.size.x / 2 + .1))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * 3;
        }
    }

    bool isInAir()
    {
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, groundLayer);

        if (hitDown.distance >= .105f)
        {
            return true;
        }

        return false;
    }

    void OnCollisionEnter2D(Collider2D collider)
    {
        if (collider.Equals(""))
        {

        }
    }
}
