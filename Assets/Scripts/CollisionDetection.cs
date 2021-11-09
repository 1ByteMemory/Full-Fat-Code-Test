using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionType
{
	Bonus,
	Enemy,
	HealthUp,
	PickUp
}
public class CollisionDetection : MonoBehaviour
{
	public CollisionType collisionType;
	public int collisionValue;

	public GameObject particleEffect;

	public event EventHandler<int> BonusEvent;
	public event EventHandler<int> HealthEvent;
	public event EventHandler<int> EnemyEvent;
	public event Event MultiplierEvent;
	public event Event PickUpEvent;

	ScoreTracker score;
	PlayerHealth health;

	private void Start()
	{
		GameManager.StartEvent += GameStart;
		score = FindObjectOfType<ScoreTracker>();
		health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

		GameStart();
	}
	private void GameStart()
	{

		if (score != null && health != null)
		{
			switch (collisionType)
			{
				case CollisionType.Bonus:
					BonusEvent += score.Bonus;
					break;
				case CollisionType.Enemy:
					EnemyEvent += health.AddHealth;
					MultiplierEvent += score.ResetMultiplier;
					break;
				case CollisionType.HealthUp:
					HealthEvent += health.AddHealth;
					break;
				case CollisionType.PickUp:
					break;
				default:
					break;
			}
		}
	}

	private void Update()
	{

	}

	private void OnTriggerEnter(Collider other)
	{
		
		if (other.CompareTag("Player"))
		{
			OnCollision(collisionValue, collisionType);
			
			gameObject.SetActive(false);

			if (particleEffect != null)
			{
				particleEffect.SetActive(true);
			}
		}
	}
	

	protected virtual void OnCollision(int value, CollisionType type)
	{
		switch (type)
		{
			case CollisionType.Bonus:
				BonusEvent?.Invoke(this, value);
				break;
			case CollisionType.Enemy:
				EnemyEvent?.Invoke(this, -value);
				MultiplierEvent?.Invoke();
				break;
			case CollisionType.HealthUp:
				HealthEvent?.Invoke(this, value);
				break;
			case CollisionType.PickUp:
				PickUpEvent?.Invoke();
				break;
			default:
				break;
		}
	}

}
