using UnityEngine;
using System.Collections;

public abstract class Melee : MonoBehaviour {
    public string weapon;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public abstract void damage();
}
