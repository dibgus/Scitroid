using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    //Walking Animation
    float counter = 0;
    int SpriteNum = 0;
    bool RightFacing;
    public Sprite[] walking;
    public Sprite[] ghostSprites;
    public Sprite[] punchingSprites;
    SpriteRenderer thisSprite;
    
    //Movement
    bool HasJumped = false;
    bool IsMoving = false;
    public float speed;

    //Stats
    public int damage;
    public int health = 100;
    public float jumpHeight;
    public int energy = 0;
    public int maxEnergy = 100;
    public int maxHealth = 100;

    //Plasma
    public float plasmaDelay;
    private float shotDelay;
    public GameObject plasma;
    private GameObject instantiatedPlasma;
    public bool plasmaUpgrade = false;
    public int plasmaBlastCost;

    //Blink
    public float blinkCooldown;
    public float blinkCounter = 0;
    bool blinkGhost = false;
    public GameObject ghost;
    private GameObject instantiatedGhost;
    private GameObject instantiatedBurst;
    public GameObject blinkBurst;

    //Attacks
    bool IsPunching = false;

    //Camera
    Camera cam;
    float minY = 0, minX = 0, maxY = 0, maxX = 0;

    // Use this for initializations

    void Start () {
        thisSprite = GetComponent<SpriteRenderer>();
        thisSprite.sprite = walking[0];
        CamSetup();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * 0.01f;
            RightFacing = true;
            if (!IsPunching) thisSprite.sprite = walking[SpriteNum];
            Vector3 newScale = this.transform.localScale;
            newScale.x = 1;
            this.transform.localScale = newScale;
            IsMoving = true;
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= Vector3.right * 0.01f;
            RightFacing = false;
            if (!IsPunching) thisSprite.sprite = walking[SpriteNum];
            Vector3 newScale = this.transform.localScale;
            newScale.x = -1;
            this.transform.localScale = newScale;
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
            if (!IsPunching)
            {
                if (RightFacing) thisSprite.sprite = walking[1];
                else thisSprite.sprite = walking[1];
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if(!HasJumped)GetComponent<Rigidbody2D>().velocity = Vector2.up*3;
            HasJumped = true;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (!IsPunching)
            {
                counter = 0;
                SpriteNum = 0;
                thisSprite.sprite = punchingSprites[0];
            }
            IsPunching = true;
        }
        counter += Time.deltaTime;
        if (IsPunching)
        {
            if (counter > 0.1f)
            {
                counter = 0;
                SpriteNum++;
                if (SpriteNum >= punchingSprites.Length)
                {
                    IsPunching = false;
                    SpriteNum = 0;
                }
                thisSprite.sprite = punchingSprites[SpriteNum];
            }
        }
        else
        {
            if (counter > 0.2f && IsMoving)
            {
                counter = 0;
                SpriteNum += 1;
                if (SpriteNum >= walking.Length) SpriteNum = 0;
                thisSprite.sprite = walking[SpriteNum];
            }
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


                instantiatedGhost = (GameObject)Instantiate(ghost, newPos, this.transform.rotation);
                instantiatedGhost.transform.localScale = this.transform.localScale;

                blinkGhost = true;
            }
            CamFollow();
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
            if (!instantiatedGhost.GetComponent<Ghost>().inWall&&blinkCounter<=0)
            {
                instantiatedBurst = Instantiate(blinkBurst);
                instantiatedBurst.transform.position = this.transform.position;
                this.transform.position = instantiatedGhost.transform.position;
                blinkCounter = blinkCooldown;
            }
            Destroy(instantiatedGhost);

        }

        if (blinkCounter > 0)
        {
            blinkCounter -= Time.deltaTime;
        }


        if (shotDelay > 0)
        {
            shotDelay -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W) && shotDelay<=0)
        {
            instantiatedPlasma = (GameObject) Instantiate(plasma, this.transform.position, Quaternion.identity);
            shotDelay = plasmaDelay;
        }

        if (Input.GetKey(KeyCode.R) && shotDelay <= 0 && plasmaUpgrade && energy>=plasmaBlastCost)
        {
            instantiatedPlasma = (GameObject)Instantiate(plasma, this.transform.position, Quaternion.identity);
            shotDelay = plasmaDelay;
            instantiatedPlasma.transform.localScale = new Vector3(2, 2);
            energy -= plasmaBlastCost;
        }

        CamFollow();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        HasJumped = false;
    }

    void CamSetup()
    {
        cam = Camera.main;
        GameObject[] allObjects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (GameObject g in allObjects)
        {
            if (g.GetComponent<SpriteRenderer>() == null) continue;
            float right = g.transform.position.x + g.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
            float left = g.transform.position.x - g.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
            float top = g.transform.position.y + g.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
            float bottom = g.transform.position.y - g.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
            maxX = right > maxX ? right : maxX;
            minX = left < minX ? left : minX;
            maxY = top > maxY ? top : maxY;
            minY = bottom < minY ? bottom : minY;
        }
        //cam = GetComponent<Camera>();
    }

    void CamFollow()
    {
        float boundsY = cam.orthographicSize;
        float boundsX = cam.orthographicSize * Screen.width / Screen.height;
        //float SetX = player.transform.position.x + boundsX < maxX && player.transform.position.x-boundsX > minX ? player.transform.position.x : transform.position.x;
        //float SetY = player.transform.position.y + boundsY < maxY && player.transform.position.y-boundsY > minY ? player.transform.position.y : transform.position.y;
        float SetX;
        float SetY;
        if (transform.position.x + boundsX < maxX && transform.position.x - boundsX > minX) SetX = transform.position.x;
        else
        {
            if (transform.position.x + boundsX >= maxX) SetX = maxX - boundsX;
            else SetX = minX + boundsX;
        }
        if (transform.position.y + boundsY < maxY && transform.position.y - boundsY > minY) SetY = transform.position.y;
        else
        {
            if (transform.position.y + boundsY >= maxY) SetY = maxY - boundsY;
            else SetY = minY + boundsY;
        }
        //print(boundsX + "  ::  " + maxX +" , " + minX + "  ::  " + (player.transform.position.x + boundsX) + " , " + (player.transform.position.x-boundsX));
        cam.transform.position = new Vector3(SetX, SetY, -10);
    }
}
