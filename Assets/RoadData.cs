using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadData : MonoBehaviour
{
	public List<GameObject> spawnPoints;
	public List<RoadData> roads;
	public MeshCollider meshCollider;
	public RoadGenerator roadGenerator;
	public int id;

	private void Awake()
	{
		meshCollider = GetComponent<MeshCollider>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Car")
		{
			roadGenerator.CheckRoad(GetInstanceID());
		}
	}
}
