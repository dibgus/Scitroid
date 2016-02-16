using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    //Walking Animation
    float counter = 0;
    int SpriteNum = 0;
    public bool RightFacing;
    public Sprite[] walking;
    public Sprite[] ghostSprites;
    public Sprite[] punchingSprites;
    SpriteRenderer thisSprite;

    //Movement
    bool HasJumped;
    bool HasDoubleJumped;
    int ladderCount = 0;
    bool finishedJump;
    bool canDoubleJump;
    bool canJump;
    bool IsMoving = false;
    bool OnLadder = false;
    public float speed;

    //Stats
    public int damage;
    public int health;
    public float jumpHeight;
    public int energy;
    public int maxEnergy;
    public int maxHealth;

    //Plasma
    public float plasmaDelay;
    private float shotDelay;
    public GameObject plasma;
    private GameObject instantiatedPlasma;
    public int plasmaUpgrade;
    public int plasmaBlastCost;

    //Blink
    public float blinkCooldown;
    public float blinkCounter = 0;
    bool blinkGhost;
    public bool blinkUpgrade;
    public bool superBlink;
    public GameObject ghost;
    private GameObject instantiatedGhost;
    private GameObject instantiatedBurst;
    private GameObject instantiatedFire;
    private GameObject instantiatedBomb;
    public GameObject blinkBurst;
    public GameObject fireBurst;
    public GameObject bomb;

    //Attacks
    bool isPunching;
    public bool punchUpgrade;
    public GameObject sword;
    GameObject instantiatedSword;
    public bool hasSword = false;

    //Camera
    Camera cam;
    float minY = 0, minX = 0, maxY = 0, maxX = 0;
    public bool MustReload = true, SceneSwitch = false;
    public float SwitchTimer;

    //Layer
    int ignoreLayer = 1 << 10;

    // Use this for initializations

    void Start () {
        thisSprite = GetComponent<SpriteRenderer>();
        thisSprite.sprite = walking[0];
        //CamSetup();
        cam = Camera.main;
        health = 100;
        maxHealth = 100;
        energy = 100;
        maxEnergy = 100;
        plasmaDelay = 0.5f;
        plasmaBlastCost = 10;
        blinkCooldown = 1;
        isPunching = false;
        blinkGhost = false;
        blinkUpgrade = false;
        superBlink = false;
        plasmaUpgrade = 0;
        punchUpgrade = false;
        canDoubleJump = false;
        canJump = false;
        HasJumped = false;
        SwitchTimer = 0.2f;
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0, thisSprite.bounds.size.y / 2 + 0.1f), Vector2.down);
        float distance = hit.distance;
        if (hit.collider != null && distance < 0.1f && finishedJump && !hit.collider.isTrigger)
        {
            HasJumped = false;
            HasDoubleJumped = false;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * 0.01f;
            RightFacing = true;
            if (!isPunching) thisSprite.sprite = walking[SpriteNum];
            Vector3 newScale = this.transform.localScale;
            newScale.x = 1;
            this.transform.localScale = newScale;
            IsMoving = true;
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= Vector3.right * 0.01f;
            RightFacing = false;
            if (!isPunching) thisSprite.sprite = walking[SpriteNum];
            Vector3 newScale = this.transform.localScale;
            newScale.x = -1;
            this.transform.localScale = newScale;
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
            if (!isPunching)
            {
                if (RightFacing) thisSprite.sprite = walking[1];
                else thisSprite.sprite = walking[1];
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (OnLadder)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.up * 1.5f;
                HasJumped = false;
                HasDoubleJumped = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            finishedJump = false;
            //if (OnLadder) GetComponent<Rigidbody2D>().velocity = Vector2.up * 1.5f;

            if (!OnLadder)
            {
                if (!HasJumped)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.up * 3;
                    HasJumped = true;
                }
                else if (!HasDoubleJumped)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.up * 3;
                    HasDoubleJumped = true;
                }
            }

            if (canJump)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.up * 3;
                canDoubleJump = true;
            }

            if (canDoubleJump)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.up * 3;
                canDoubleJump = false;
                canJump = true;
            }
            //HasDoubleJumped = true;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            finishedJump = true;
        }
        if (Input.GetKey(KeyCode.Q) && punchUpgrade)
        {
            if (!isPunching)
            {
                counter = 0;
                SpriteNum = 0;
                thisSprite.sprite = punchingSprites[0];
                if (instantiatedSword == null && hasSword) instantiatedSword = (GameObject)Instantiate(sword, this.transform.position, Quaternion.identity);
            }
            isPunching = true;
        }
        counter += Time.deltaTime;
        if (isPunching)
        {
            if (counter > 0.1f)
            {
                counter = 0;
                SpriteNum++;
                if (SpriteNum >= punchingSprites.Length)
                {
                    isPunching = false;
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

        if (Input.GetKeyDown(KeyCode.E) && blinkUpgrade)
        {

            if (blinkGhost == false || instantiatedGhost == null)
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

        if (Input.GetKeyDown(KeyCode.C)) CamSetup();

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
            if (instantiatedGhost!=null && !instantiatedGhost.GetComponent<Ghost>().inWall && blinkCounter <= 0)
            {
                instantiatedBurst = Instantiate(blinkBurst);
                instantiatedBurst.transform.position = this.transform.position;
                this.transform.position = instantiatedGhost.transform.position;

                if (superBlink)
                {
                    instantiatedFire = Instantiate(fireBurst);
                    instantiatedFire.transform.position = this.transform.position;

                    blinkCounter = blinkCooldown;
                }


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

        if (Input.GetKey(KeyCode.W) && plasmaUpgrade > 0 && shotDelay<=0)
        {
            instantiatedPlasma = (GameObject) Instantiate(plasma, this.transform.position, Quaternion.identity);
            shotDelay = plasmaDelay;
        }

        if (Input.GetKey(KeyCode.R) && shotDelay <= 0 && plasmaUpgrade > 1 && energy>=plasmaBlastCost)
        {
            instantiatedPlasma = (GameObject)Instantiate(plasma, this.transform.position, Quaternion.identity);
            shotDelay = plasmaDelay;
            instantiatedPlasma.transform.localScale = new Vector3(2, 2);
            energy -= plasmaBlastCost;
            instantiatedPlasma.GetComponent<Plasma>().upgrade = plasmaUpgrade;
        }

        if (Input.GetKey(KeyCode.B))
        {
            instantiatedBomb = Instantiate(bomb);
            Vector3 newPos = this.transform.position;

            if (RightFacing)
            {
                newPos.x += 0.2f;
            }
            else
            {
                newPos.x -= 0.2f;
            }
            newPos.y += 0.15f;


            instantiatedBomb.transform.position = newPos;
        }

        if (MustReload) CamSetup();
        if(!MustReload)CamFollow();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //HasDoubleJumped = false;
    }

    void CamSetup()
    {
        GameObject[] allObjects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (GameObject g in allObjects)
        {
            if (g.GetComponent<BoxCollider2D>() == null) continue;
            float right = g.transform.position.x + g.GetComponent<BoxCollider2D>().bounds.size.x / 2;
            float left = g.transform.position.x - g.GetComponent<BoxCollider2D>().bounds.size.x / 2;
            float top = g.transform.position.y + g.GetComponent<BoxCollider2D>().bounds.size.y / 2;
            float bottom = g.transform.position.y - g.GetComponent<BoxCollider2D>().bounds.size.y / 2;
            maxX = right > maxX ? right : maxX;
            minX = left < minX ? left : minX;
            maxY = top > maxY ? top : maxY;
            minY = bottom < minY ? bottom : minY;
        }
        MustReload = false;
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
        if (SceneSwitch)
        {
            if (Vector3.Distance(cam.transform.position, new Vector3(SetX, SetY, -10)) > 0.05f || SwitchTimer > 0)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(SetX, SetY, -10), Time.deltaTime * 15);
                SwitchTimer -= Time.deltaTime;
            }
            else {
                SceneSwitch = false;
                SwitchTimer = 0.2f;
            }
        }
        else cam.transform.position = new Vector3(SetX, SetY, -10);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        TiledCollider ladderCheck = col.gameObject.GetComponent<TiledCollider>();
        if (ladderCheck != null && ladderCheck.GetLadder())
        {
            OnLadder = true;
            ladderCount++;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        TiledCollider ladderCheck = col.gameObject.GetComponent<TiledCollider>();
        if (ladderCheck != null && ladderCheck.GetLadder())
        {
            ladderCount--;
            if(ladderCount == 0)OnLadder = false;
        }
    }
}
