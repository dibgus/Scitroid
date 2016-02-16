using UnityEngine;
using System.Collections;

public class Upgrade : MonoBehaviour {
    public int upgradeID;

	// Use this for initialization
	void Start () {
        switch (upgradeID)
        {
            case (0): GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Upgrades/plasmaicon"); break;
            case (1): GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Upgrades/upgradedplasmaicon"); break;
            case (2): GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Upgrades/plasmafreezeicon"); break;
            case (3): GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Upgrades/bigupgradedplasmaicon"); break;
            case (4): GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Upgrades/blinkicon"); break;
        }
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
                collision.GetComponent<Player>().plasmaUpgrade = 2;
            }

            if (upgradeID == 2)
            {
                collision.GetComponent<Player>().plasmaUpgrade = 3;
            }

            if (upgradeID == 3)
            {
                collision.GetComponent<Player>().plasmaUpgrade = 4;
            }

            if (upgradeID == 4)
            {
                collision.GetComponent<Player>().blinkUpgrade = true;
            }

            if (upgradeID == 5)
            {
                collision.GetComponent<Player>().superBlink = true;
            }

            if (upgradeID == 6)
            {
                collision.GetComponent<Player>().punchUpgrade = true;
            }

            Destroy(gameObject);
        }
    }
}
