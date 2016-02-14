using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerBars : MonoBehaviour {

    public Slider healthBar;
    public Slider energyBar;
    public Player player;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(player.health != 0)
        healthBar.value = player.health / (float)player.maxHealth;
        if(player.energy != 0)
        energyBar.value = player.energy / (float)player.maxEnergy;
    }
}
