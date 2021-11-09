using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : MonoBehaviour
{
	public GameObject particle;
	public GameObject[] model;

	Transform player;

    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z - player.transform.position.z > 0)
		{
			for (int i = 0; i < model.Length; i++)
			{
				model[i].SetActive(true);
			}
			particle.SetActive(false);
		}
    }
}
