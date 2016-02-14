using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour {

    public Sprite[] ghostSprites;
    public bool inWall = false;
    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 dir = new Vector2(this.transform.localScale.x, 0.0f);

        RaycastHit2D hitWall = Physics2D.Raycast(this.transform.position, dir);

        if (hitWall.collider != null && hitWall.collider.gameObject.tag == "Wall" && hitWall.distance <= 0.1)
        {
            inWall = true;
            Color c = GetComponent<SpriteRenderer>().color;
            c.g -= 127;
            c.b -= 127;
            GetComponent<SpriteRenderer>().color = c;
        }
        else if (player.GetComponent<Player>().blinkCounter > 0)
        {
            inWall = false;
            Color c = GetComponent<SpriteRenderer>().color;
            c.g -= 127;
            c.b -= 127;
            GetComponent<SpriteRenderer>().color = c;
        }
        else
        {
            inWall = false;
            Color c = GetComponent<SpriteRenderer>().color;
            c.g = 255;
            c.b = 255;
            GetComponent<SpriteRenderer>().color = c;
        }
    }
}
