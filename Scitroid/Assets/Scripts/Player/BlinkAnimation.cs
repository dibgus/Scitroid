using UnityEngine;
using System.Collections;

public class BlinkAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (this.GetComponent<SpriteRenderer>().color.a <= 0)
        {
            Destroy(gameObject);
        }

        Color color = this.GetComponent<SpriteRenderer>().color;
        color.a -= 0.05f;
        this.GetComponent<SpriteRenderer>().color = color;

	}
}
