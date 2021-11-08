using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundReplacer : MonoBehaviour
{
	public float maxDistance = 1000;
	public float groundZSize = 15;
	public GameObject[] Grounds;

	public event EventHandler<int> NewGround;

	Vector3 startPosition;

	Vector3 nextMovePosition;

	int index;
	public int GroundsIndex
	{
		get { return index; }
		private set
		{
			index = value;
			// Loop the index if it goes above the array length or below zero
			if (index >= Grounds.Length) index = 0;
			else if (index < 0) index = Grounds.Length;
		}
	}

	// Start is called before the first frame update
	private void Start()
	{
		GameManager.StartEvent += GameStart;
		GameStart();
		startPosition = transform.position;
	}
	void GameStart()
    {
		nextMovePosition = new Vector3(0, 0, groundZSize);
		ResetTiles();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if (!GameManager.IsPlaying) return;

		// If player is ahead of next position
		if (Vector3.Dot(transform.forward, nextMovePosition - transform.position) <= 0)
		{
			//Debug.Log("Ground has moved");
			Grounds[GroundsIndex].transform.Translate(Vector3.forward * groundZSize * Grounds.Length, Space.Self);

			nextMovePosition.z += groundZSize;


			OnNewGroundPlaced(GroundsIndex);
			GroundsIndex++;
		} 
		

		// When the player reaches a maxiem distance, send everything back to avoid float errors
		if (transform.position.z >= maxDistance)
		{
			ResetTiles();
		}

    }
	
	void ResetTiles()
	{
		transform.position = startPosition;

		for (int i = 0; i < Grounds.Length; i++)
		{
			Grounds[i].transform.position = startPosition + new Vector3(0, 0, i * groundZSize);
		}

		GroundsIndex = 0;   // reset index
		nextMovePosition = new Vector3(0, 0, groundZSize);

	}

	protected virtual void OnNewGroundPlaced(int value)
	{
		NewGround?.Invoke(this, value);
	}

}
