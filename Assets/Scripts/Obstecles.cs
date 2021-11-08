using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstecles : MonoBehaviour
{
	public GameObject staticEnemy;

	ObjectPool[] obsteclePool;

	public int amountPerTile = 4;

	int[,] spawningSpots;

	Transform player;
	PlayerController pc;
	GroundReplacer ground;

    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		
		pc = player.GetComponent<PlayerController>();

		ground = FindObjectOfType<GroundReplacer>();


		// Assign method to delegate
		ground.NewGround += NewObjectConfiguration;

		// Individual enemy pools for each ground tile
		obsteclePool = new ObjectPool[ground.Grounds.Length];

		// Initialize spawningSpots matrix
		spawningSpots = new int[pc.numberOfLanes, Mathf.FloorToInt(ground.groundZSize / 2)];

		// For each ground tile, execpt the first one (the player is there!)
		for (int i = 0; i < ground.Grounds.Length; i++)
		{
			// Generate spawning spots for the ground tile
			spawningSpots = GenerateSpawnMatrix(spawningSpots, amountPerTile);

			// Populate pool list
			obsteclePool[i] = new ObjectPool
			{
				// Set the amount to pool
				//amountToPool = Mathf.FloorToInt(ground.groundZSize / 2) * pc.numberOfLanes,
				amountToPool = spawningSpots.Length,
				objectToPool = staticEnemy
			};

			// Pool the objects
			obsteclePool[i].InstatiateObjects(ground.Grounds[i].transform);
			
			for (int j = 0; j < obsteclePool[i].pooledObjects.Length; j++)
			{
				// Reposition the pooled objects to a grid
				int x = j % spawningSpots.GetLength(0);
				int y = j / spawningSpots.GetLength(0);

				Vector3 pos = new Vector3(LaneToPos(spawningSpots.GetLength(0), x), 0, LaneToPos(spawningSpots.GetLength(1), y));

				obsteclePool[i].pooledObjects[j].transform.localPosition = pos;

				// Set active if not first tile and marked

				if (i != 0 && spawningSpots[x, y] == 1)
				{
					obsteclePool[i].pooledObjects[j].SetActive(true);
					spawningSpots[x, y] = 0; // Reset spot to zero
				}

			}
		}
    }

	public void NewObjectConfiguration(object sender, int objectPoolIndex)
	{
		//spawningSpots = new int[pc.numberOfLanes, Mathf.FloorToInt(ground.groundZSize / 2)];
		// Generate spawning spots for the ground tile
		spawningSpots = GenerateSpawnMatrix(spawningSpots, amountPerTile);

		for (int i = 0; i < obsteclePool[objectPoolIndex].pooledObjects.Length; i++)
		{
			int x = i % spawningSpots.GetLength(0);
			int y = i / spawningSpots.GetLength(0);

			// Set false
			obsteclePool[objectPoolIndex].pooledObjects[i].SetActive(false);
			if (spawningSpots[x, y] == 1)
			{
				obsteclePool[objectPoolIndex].pooledObjects[i].SetActive(true);
				spawningSpots[x, y] = 0; // Reset spot to zero, 
			}
		}
	}
	

	/// <summary>
	/// Generate a matrix of randomm spawning locations
	/// </summary>
	/// <param name="spawnMatrix">Matrix of spawning locations</param>
	/// <param name="spawnIterations">Number of spawns</param>
	/// <returns>A matrix with of spots to spawn an object</returns>
	int[,] GenerateSpawnMatrix(int[,] spawnMatrix, int spawnIterations)
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

		//Debug.Log("=====================================");
		//for (int y = 0; y < spawnMatrix.GetLength(1); y++) Debug.Log(string.Format("[{0}, {1}, {2}, {3}, {4}]", spawnMatrix[0,y], spawnMatrix[1, y], spawnMatrix[2, y], spawnMatrix[3, y], spawnMatrix[4, y]));
		//Debug.Log("=====================================");
		

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
	/// Returns the 
	/// </summary>
	/// <param name="laneAmount"></param>
	/// <param name="laneNumber"></param>
	/// <returns></returns>
	int LaneToPos(int laneAmount, int laneNumber)
	{
		return (laneNumber * 2) - (Mathf.FloorToInt(laneAmount / 2) * 2);
	}
	int LaneToPos(int laneAmount, int laneNumber, int spaceing)
	{
		if (spaceing == 0) spaceing = 1; Debug.LogWarning("Spacing was zero, setting to 1 to prevent dividing by 0"); // prevents deviding by zero
		return (laneNumber * spaceing) - (Mathf.FloorToInt(laneAmount / spaceing) * spaceing);
	}
}
