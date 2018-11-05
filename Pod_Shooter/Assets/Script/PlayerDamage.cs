using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerDamage : NetworkBehaviour, Damagable {
    PlayerLife playerLife;

    public void DealDamage(int damage)
    {
        playerLife.takeDamage(damage);
    }
	// Use this for initialization
	void Start () {
        playerLife = GetComponent<PlayerLife>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
