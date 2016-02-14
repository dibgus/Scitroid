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
        healthBar.value = (float)player.health / player.maxHealth;
        if(player.energy != 0)
        energyBar.value = player.energy / (float)player.maxEnergy;
    }
}
