using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{

	[Header("UI Elements")]
	public Text scoreText;
	public Text multiplierText;

	[Header("Score")]
	public float score = 0;
	public float scoreIncreaseSpeed = 1;

	[Header("Multiplier")]
	public float multiplier = 1;
	public float mulitplierCap = 4;
	public float multiplierIncreaseAmount = 0.5f;
	public float multiplierTimer = 5;

	
	void GameStart()
    {
		GameManager.StartEvent += GameStart;
		timerEnd = multiplierTimer;

		GameStart();
	}

	float timerEnd;
    // Update is called once per frame
    void Update()
    {
		if (!GameManager.IsPlaying) return;
		// Increase score over time 
		score += Time.deltaTime * scoreIncreaseSpeed * multiplier;

		// Increase multiplier after x seconds
		if (Time.time >= timerEnd)
		{
			timerEnd = Time.time + multiplierTimer;
			
			if (multiplier < mulitplierCap)
			{
				multiplier += multiplierIncreaseAmount;

				// Play visual effect

			}
		}
		
		// Update UI elements
		scoreText.text = Mathf.FloorToInt(score).ToString();
		multiplierText.text = "x" + multiplier.ToString();
    }

	public void Bonus(object sender, int bonus)
	{
		score += bonus;

		// Play visual effect
	}

	public void ResetMultiplier()
	{
		multiplier = 1;
		timerEnd = Time.time + multiplierTimer; // Reset timer aswell

		// Play visual effect
	}
}
