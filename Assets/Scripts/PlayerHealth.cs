using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	//public int maxHealth;
	int health;

	public GameObject[] shields;

	PlayerController pc;
	GameManager gm;

	
	private void Start()
	{
		GameManager.StartEvent += GameStart;
		pc = GetComponent<PlayerController>();
		gm = FindObjectOfType<GameManager>();

		GameStart();
	}
	private void GameStart()
	{
		health = shields.Length;
		for (int i = 0; i < health; i++)
		{
			shields[i].SetActive(true);
		}
	}

	public void AddHealth(int amount)
	{
		health += amount;
		health = health > shields.Length ? shields.Length : health; // Check if it goes over max health
		
		if (amount > 0)
		{
			if (health < shields.Length)
			{
				// Add shield
				shields[health].SetActive(true);

				// Play animation

			}
		}
		else if (health >= 0)
		{
			Debug.Log("DAMAGE");
			// Remove Shield
			shields[health].SetActive(false);

			// Reduce Speed
			pc.speed = 0;

			// Play animation

		}
		else
		{
			// plaeyer dead
			Debug.Log("Player DEad");
			gm.GameOver();
		}
	}
	public void AddHealth(object sender, int amount)
	{
		AddHealth(amount);
	}
}
