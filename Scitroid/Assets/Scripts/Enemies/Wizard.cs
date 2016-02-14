using UnityEngine;
using System.Collections;

public class Wizard : MonoBehaviour {

    public float health = 100;
    int damage = 10;
    int cooldown = 100;
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
        if (cooldown <= 0)
        {
            float theta = Mathf.Atan((target.transform.position.y - transform.position.y) / (target.transform.position.x - transform.position.x));
            missileClone = (GameObject)Instantiate(missile, this.transform.position, Quaternion.identity);
            if(target.transform.position.x > transform.position.x)
                missileClone.GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Cos(theta) * 3, Mathf.Sin(theta) * 3, 0);
            else
                missileClone.GetComponent<Rigidbody2D>().velocity = new Vector3(-Mathf.Cos(theta) * 3, -Mathf.Sin(theta) * 3, 0);
            cooldown = 100;
        }
        else
            cooldown--;
	}
}
