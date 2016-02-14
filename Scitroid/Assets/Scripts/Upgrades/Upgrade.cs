using UnityEngine;
using System.Collections;

public class Upgrade : MonoBehaviour {
    public int upgradeID;
    public Sprite[] sprites;


	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = sprites[upgradeID];
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (upgradeID == 0)
            {
                collision.GetComponent<Player>().plasmaUpgrade = 1;
            }

            if (upgradeID == 1)
            {
                Destroy(collision.gameObject);
            }

            Destroy(gameObject);
        }
    }
}
