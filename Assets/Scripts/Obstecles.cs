using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstecles : MonoBehaviour
{

	public float ObstacleSpawnDistance = 15;
	public float minSpawnTime = 0.5f;
	public float maxSpawnTime = 3.0f;

	public Transform[] obsteclesList;

	int[,] spawningSpots = new int[5,10];

	Transform player;
	PlayerController pc;
	GroundReplacer ground;

    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		
		pc = player.GetComponent<PlayerController>();

		ground = FindObjectOfType<GroundReplacer>();


		// For each ground tile, execpt the first one (the player is there!)
		for (int i = 1; i < ground.Grounds.Length; i++)
		{
			// Generate spawning spots for the ground tile
			spawningSpots = SpawnObjectOnGround(spawningSpots, 4);

			// Place object on the marked spot


		}

    }
	
	
	

	/// <summary>
	/// Generate a matrix of randomm spawning locations
	/// </summary>
	/// <param name="spawnMatrix">Matrix of spawning locations</param>
	/// <param name="spawnIterations">Number of spawns</param>
	/// <returns>A matrix with of spots to spawn an object</returns>
	int[,] SpawnObjectOnGround(int[,] spawnMatrix, int spawnIterations)
	{

		// Choose x amount of indicies
		int[] indicies = new int[spawnIterations];
		for (int i = 0; i < spawnIterations; i++)
			indicies[i] = Random.Range(0, spawnMatrix.Length);

		// FOR DEBUGGING //
		// Fills the matrix
		//indicies = new int[spawnMatrix.Length];
		//for (int i = 0; i < indicies.Length; i++) indicies[i] = i;
		
		// ------------- //

		// Mark those indicies for spawning an item
		for (int i = 0; i < indicies.Length; i++)
		{
			// Calulate where the selcected indicy is
			int x = indicies[i] % spawnMatrix.GetLength(0);
			int y = indicies[i] / spawnMatrix.GetLength(0);

			
			// Mark it for spawning an object
			spawnMatrix[x, y] = 1;
		}


		//for (int y = 0; y < spawnMatrix.GetLength(1); y++) Debug.Log(string.Format("[{0}, {1}, {2}, {3}, {4}]", spawnMatrix[0,y], spawnMatrix[1, y], spawnMatrix[2, y], spawnMatrix[3, y], spawnMatrix[4, y]));
		

		return spawnMatrix;
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
