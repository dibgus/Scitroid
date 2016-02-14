using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public Sprite[] enemyRight;
    public Sprite[] enemyLeft;
    public SpriteRenderer basicEnemy;

    int health = 100;
    int damage = 10;
    int jumpLayer = 1 << 8;
    int edgeLayer = 1 << 9;
    public bool right;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        Jump();
    }

    void Move()
    {
        Vector2 originLeft = new Vector2(transform.position.x - 0.1f, transform.position.y);
        Vector2 originRight = new Vector2(transform.position.x + 0.1f, transform.position.y);

        RaycastHit2D hitLeft = Physics2D.Raycast(originLeft, Vector2.down, Mathf.Infinity);
        RaycastHit2D hitRight = Physics2D.Raycast(originRight, Vector2.down, Mathf.Infinity);

        if (hitRight.collider == null)
        {
            transform.position += Vector3.left * .025f;
            right = false;
        }

        if (hitLeft.collider == null)
        {
            transform.position += Vector3.right * .025f;
            right = true;
        }

        if (!right)
        {
            transform.position += Vector3.left * .025f;
        }

        if (right)
        {
            transform.position += Vector3.right * .025f;
        }
    }
    
    void Jump()
    {
        Vector2 originUp = new Vector2(transform.position.x, transform.position.y + 0.1f);

        RaycastHit2D hitLeft = Physics2D.Raycast(originUp, Vector2.left);
        RaycastHit2D hitRight = Physics2D.Raycast(originUp, Vector2.right);

        if (hitRight.collider != null && hitRight.distance <= 0.25f || hitLeft.collider != null && hitLeft.distance <= 0.25f)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * 3;
        }
    }
}
