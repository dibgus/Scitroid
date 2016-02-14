using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    float counter = 0;
    int SpriteNum = 0;
    bool RightFacing;
    bool HasJumped = false;
    public Sprite[] walking;
    public Sprite[] ghostSprites;
    public SpriteRenderer thisSprite;
    public float speed;
    public int damage;
    public int health;
    public float jumpHeight;
    public int energy;
    public float plasmaDelay;
    private float shotDelay;
    public GameObject plasma;
    private GameObject instantiatedPlasma;


    bool blinkGhost = false;

    public GameObject ghost;
    private GameObject instantiatedGhost;


	// Use this for initialization

    public int maxEnergy;
    public int maxHealth;

	// Use this for initializations

	void Start () {
        thisSprite.sprite = walking[0];
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right*0.01f;
            RightFacing = true;
            thisSprite.sprite = walking[SpriteNum];
            Vector3 newScale = this.transform.localScale;
            newScale.x = 1;
            this.transform.localScale = newScale;
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= Vector3.right * 0.01f;
            RightFacing = false;
            thisSprite.sprite = walking[SpriteNum];
            Vector3 newScale = this.transform.localScale;
            newScale.x = -1;
            this.transform.localScale = newScale;
            
        }
        else
        {
            counter = 0;
            if (RightFacing) thisSprite.sprite = walking[1];
            else thisSprite.sprite = walking[1];
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if(!HasJumped)GetComponent<Rigidbody2D>().velocity = Vector2.up*3;
            HasJumped = true;
        }
        counter += 0.05f;
        if(counter > 0.5f)
        {
            counter = 0;
            SpriteNum += 1;
            if (SpriteNum >= walking.Length) SpriteNum = 0;
            thisSprite.sprite = walking[SpriteNum];
        }

        if (Input.GetKeyDown(KeyCode.E))
        {

            if (blinkGhost == false)
            {
                Vector3 newPos = this.transform.position;

                if (RightFacing)
                {
                    newPos.x += 0.5f;
                }
                else
                {
                    newPos.x -= 0.5f;
                }


                instantiatedGhost = (GameObject) Instantiate(ghost, newPos, this.transform.rotation);
                instantiatedGhost.transform.localScale = this.transform.localScale;

                blinkGhost = true;
            }    
        }

        if (instantiatedGhost != null)
        {
            Vector3 newPos = this.transform.position;

            if (RightFacing)
            {
                newPos.x += 0.5f;
            }
            else
            {
                newPos.x -= 0.5f;
            }
            instantiatedGhost.transform.position = newPos;
            instantiatedGhost.transform.localScale = this.transform.localScale;
            instantiatedGhost.GetComponent<SpriteRenderer>().sprite = ghostSprites[SpriteNum];
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            blinkGhost = false;

            this.transform.position = instantiatedGhost.transform.position;
            Destroy(instantiatedGhost);
        }

        if (shotDelay > 0)
        {
            shotDelay -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W)&&shotDelay<=0)
        {
            instantiatedPlasma = (GameObject) Instantiate(plasma, this.transform.position, Quaternion.identity);
            shotDelay = plasmaDelay;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        HasJumped = false;
    }
}
