using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    float timeToLive;
    public GameObject explosion;
    public GameObject player;

	// Use this for initialization
	void Start () {

        timeToLive = 100;
        Vector2 direction = Vector2.one.normalized;
        float magnitude;

        if (player.GetComponent<Player>().RightFacing)
        {
            magnitude = -0.5f;
            direction = new Vector2(1, -1);
        }
        else
        {
            magnitude = -0.5f;
        }

        // Add the stuff
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Add force
        rb.AddRelativeForce(direction * magnitude, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {

        print(timeToLive);
        timeToLive -= 1;

        if (timeToLive <= 0)
        {
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
        }

	
	}
}
