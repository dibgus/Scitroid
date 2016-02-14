using UnityEngine;
using System.Collections;

public class Kanga : MonoBehaviour {
    private Enemy enemy;

	// Use this for initialization
	void Start () {
        enemy = new Enemy();
	}
	
	// Update is called once per frame
	void Update () {
        enemy.Jump();
        enemy.Move();
	}
}
