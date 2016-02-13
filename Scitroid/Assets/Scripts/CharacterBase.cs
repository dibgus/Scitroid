using UnityEngine;
using System.Collections;

public abstract class CharacterBase : MonoBehaviour {

    abstract int characterHealth;
    abstract int characterEnergy;
    abstract int characterSpeed;
    abstract int baseAttackDamage;

	// Use this for initialization
	abstract void Start() {}
	
	// Update is called once per frame
	abstract void Update() {}

    abstract void Move() {}

    int getHealth() { return characterHealth; }

    int getEnergy() { return characterEnergy; }

    int getSpeed() { return characterSpeed; }

    int getBaseAttackDamage() { return baseAttackDamage; }


}
