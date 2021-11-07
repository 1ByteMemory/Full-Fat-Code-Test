using System;
using UnityEngine;

[Serializable]
public class ObjectPool
{
	public GameObject[] pooledObjects;
	public GameObject objectToPool;
	public int amountToPool;
	
	public void InstatiateObjects(Transform parent)
	{
		pooledObjects = new GameObject[amountToPool];
		for (int i = 0; i < amountToPool; i++)
		{
			pooledObjects[i] = GameObject.Instantiate(objectToPool, parent);
			pooledObjects[i].SetActive(false);
		}
	}
	public void InstatiateObjects(Vector3 position)
	{
		pooledObjects = new GameObject[amountToPool];
		for (int i = 0; i < amountToPool; i++)
		{
			pooledObjects[i] = GameObject.Instantiate(objectToPool, position, new Quaternion());
			pooledObjects[i].SetActive(false);
		}
	}

	public GameObject GetObjectFromPool()
	{
		for (int i = 0; i < amountToPool; i++)
		{
			if (!pooledObjects[i].activeInHierarchy)
			{
				return pooledObjects[i];
			}
		}
		return null;
	}
}
