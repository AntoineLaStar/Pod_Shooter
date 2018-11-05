using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerLife : NetworkBehaviour {
    public RectTransform healthbar;
    public const int maxlife = 100;
    private NetworkStartPosition[] spawnPoints;

    [SyncVar(hook = "updateLife")]
    int currentLife = maxlife;

	// Use this for initialization
	void Start () {
		if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void takeDamage(int damage)
    {
        if (isServer)
        {
            currentLife -= damage;
            if (currentLife <= 0)
            {
                currentLife = 0;
                Die();
            }
        }
    }

    private void updateLife(int newLife)
    {
        healthbar.sizeDelta = new Vector2(newLife,healthbar.sizeDelta.y);
    }

    private void Die()
    {
        RpcReSpawn();
    }

    [ClientRpc]
    void RpcReSpawn()
    {
        if (isLocalPlayer)
        {
            currentLife = maxlife;
            Vector3 spawnPoint = Vector3.zero;
            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }
            transform.position = spawnPoint;
        }
    }
}
