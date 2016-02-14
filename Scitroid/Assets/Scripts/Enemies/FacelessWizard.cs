using UnityEngine;
using System.Collections;

public class FacelessWizard : MonoBehaviour {

    public float health = 100;
    int damage = 10;
    int superCoolDown = 150;
    int superCoolCount = 4;
    int cooldown = 50;
    public GameObject missile;
    private GameObject missileClone;
    public Player target;
    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
            Destroy(gameObject);
        if (target.transform.position.x > transform.position.x)
            GetComponent<SpriteRenderer>().flipX = false;
        else
            GetComponent<SpriteRenderer>().flipX = true;
        if (superCoolCount >= 4)
        {
            superCoolDown--;
            if (superCoolDown <= 0)
            {
                superCoolDown = 1000;
                superCoolCount = 0;
            }
        }
        else
        {
            if (cooldown <= 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    float theta = Mathf.Atan((target.transform.position.y - transform.position.y) / (target.transform.position.x - transform.position.x));
                    missileClone = (GameObject)Instantiate(missile, this.transform.position, Random.rotation);
                    float randomsign = Random.Range(0f, 1f);
                    if (target.transform.position.x > transform.position.x)
                        missileClone.GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Cos(theta + (randomsign < .5f ? Random.Range(0.1f, 1f) : -Random.Range(0.1f, 1f) * 2)), Mathf.Sin(theta) * 2, 0);
                    else
                        missileClone.GetComponent<Rigidbody2D>().velocity = new Vector3(-Mathf.Cos(theta + (randomsign < .5f ? Random.Range(0.1f, 1f) : -Random.Range(0.1f, 1f) * 2)) * 2, -Mathf.Sin(theta + (randomsign < .5f ? Random.Range(0.1f, 1f) : -Random.Range(0.1f, 1f) * 2)) * 2, 0);
                    missileClone.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(0.0f, 0.9f);
                    cooldown = 100;
                }
                superCoolCount++;
            }
            else
                cooldown--;
        }
    }
}
