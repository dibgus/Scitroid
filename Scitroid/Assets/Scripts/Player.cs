using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    float Time = 0;
    int SpriteNum = 0;
    bool RightFacing;
    bool HasJumped = false;
    public Sprite[] rightSprites;
    public Sprite[] leftSprites;
    public SpriteRenderer thisSprite;
    public float speed;
    public int damage;
    public int health;
    public float jumpHeight;
    public int energy;

    public int maxEnergy;
    public int maxHealth;

	// Use this for initializations
	void Start () {
        thisSprite.sprite = leftSprites[0];
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right*0.01f;
            RightFacing = true;
            thisSprite.sprite = rightSprites[SpriteNum];
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= Vector3.right * 0.01f;
            RightFacing = false;
            thisSprite.sprite = leftSprites[SpriteNum];
        }
        else
        {
            Time = 0;
            if (RightFacing) thisSprite.sprite = rightSprites[1];
            else thisSprite.sprite = leftSprites[1];
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if(!HasJumped)GetComponent<Rigidbody2D>().velocity = Vector2.up*3;
            HasJumped = true;
        }
        Time += 0.05f;
        if(Time > 0.5f)
        {
            Time = 0;
            SpriteNum += 1;
            if (SpriteNum >= leftSprites.Length) SpriteNum = 0;
            thisSprite.sprite = leftSprites[SpriteNum];
            if (RightFacing) thisSprite.sprite = rightSprites[SpriteNum];
            else thisSprite.sprite = leftSprites[SpriteNum];
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        HasJumped = false;
    }
}
