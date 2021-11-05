using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstecles : MonoBehaviour
{

	public float playerToObstacleSpawnDistance = 30.0f;
	public float minSpawnTime = 0.5f;
	public float maxSpawnTime = 3.0f;

	public Transform[] obsteclesList;

	Transform player;
	PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		
		pc = player.GetComponent<PlayerController>();

		for (int i = 0; i < obsteclesList.Length; i++)
		{
			SpawnObstecle(obsteclesList[i], playerToObstacleSpawnDistance + Random.Range(i + 5.0f, i + 15.0f));
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void SpawnObstecle(Transform obstecle, float distanceFromPlayer)
	{
		
		obstecle.position = new Vector3(
			Random.Range(0, pc.numberOfLanes),					// Randomize its lane position
			0,
			player.position.z + distanceFromPlayer	// Set position infront of player
		);

		
	}

}
