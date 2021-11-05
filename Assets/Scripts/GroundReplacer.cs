using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundReplacer : MonoBehaviour
{
	public float maxDistance = 1000;
	public float groundZSize = 15;
	public GameObject[] Grounds;

	bool movedGround = true;

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
    void Start()
    {
		startPosition = transform.position;

		nextMovePosition = new Vector3(0, 0, groundZSize);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		/* //Doesn't work properly at high speeds
		
		// Every x amount of meters, place ground infront of the player
		// We floor the player position to ensure the ground isn't skipped.
		if (!movedGround && Mathf.FloorToInt(transform.position.z) % groundSize == 0)
		{
			Debug.Log("Ground has moved");
			Grounds[GroundsIndex].transform.Translate(Vector3.forward * groundZSize * Grounds.Length, Space.Self);
			GroundsIndex++;
			movedGround = true;
		}
		// This prevents the ground from moving ahead of the player
		else if (movedGround && Mathf.FloorToInt(transform.position.z - 1) % groundSize == 0)
		{
			movedGround = false;
		}
		*/

		// If player is ahead of next position
		if (Vector3.Dot(transform.forward, nextMovePosition - transform.position) <= 0)
		{
			//Debug.Log("Ground has moved");
			Grounds[GroundsIndex].transform.Translate(Vector3.forward * groundZSize * Grounds.Length, Space.Self);
			GroundsIndex++;

			nextMovePosition.z += groundZSize; 
		} 
		

		// When the player reaches a maxiem distance, send everything back to avoid float errors
		if (transform.position.z >= maxDistance)
		{
			transform.position = startPosition;

			for (int i = 0; i < Grounds.Length; i++)
			{
				Grounds[i].transform.position = startPosition  + new Vector3(0, 0, i * groundZSize);
			}
			
			GroundsIndex = 0;   // reset index
			nextMovePosition = new Vector3(0, 0, groundZSize);
		}

    }
	
}
