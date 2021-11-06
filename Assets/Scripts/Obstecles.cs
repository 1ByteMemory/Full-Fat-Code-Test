using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstecles : MonoBehaviour
{

	public float ObstacleSpawnDistance = 15;
	public float minSpawnTime = 0.5f;
	public float maxSpawnTime = 3.0f;

	public Transform[] obsteclesList;

	int[,] spawningSpots;

	Transform player;
	PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		
		pc = player.GetComponent<PlayerController>();

		spawningSpots = new int[pc.numberOfLanes, Mathf.FloorToInt(GetComponent<GroundReplacer>().groundZSize)];

		for (int i = 0; i < obsteclesList.Length; i++)
		{
			SpawnObject(obsteclesList[i], 10 + i * 4);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	

	void SpawnObject(Transform objectToSpawn, float distanceFromPlayer)
	{
		
		Debug.Log("Object: " + objectToSpawn);
		objectToSpawn.position = new Vector3(
			RandLane(pc.numberOfLanes),					// Randomize its lane position
			0,
			Mathf.Floor(player.position.z + distanceFromPlayer)	// Set position infront of player, floored to make it more uniform
		);
		
	}

	int RandYpos(int spaces)
	{
		
		return 0;
	}

	/// <summary>
	/// Returns the x position of a random lane
	/// </summary>
	/// <param name="laneAmount">The total number of lanes</param>
	/// <returns></returns>
	int RandLane(int laneAmount)
	{
		int randomLane = Random.Range(1, laneAmount + 1) * 2 - 2;
		int offset = Mathf.FloorToInt(laneAmount / 2) * 2;

		return randomLane - offset;
	}

	/// <summary>
	/// Returns the x position of a given lane
	/// </summary>
	/// <param name="laneAmount"></param>
	/// <param name="laneNumber"></param>
	/// <returns></returns>
	int LaneToXPos(int laneAmount, int laneNumber)
	{
		return (laneNumber * 2) - (Mathf.FloorToInt(laneAmount / 2) * 2);
	}

}
