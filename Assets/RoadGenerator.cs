using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private List<RoadData> roads;

    private List<RoadData> roadsList = new List<RoadData>();
    private List<RoadData> roadsListNotActive = new List<RoadData>();
    private RoadData lastRoad;

    private void Awake()
    {
        
    }

    void Start()
    {
		lastRoad = Instantiate(roads[Random.Range(0, roads.Count)], transform.position, Quaternion.identity);
        lastRoad.transform.SetParent(transform);
        lastRoad.roadGenerator = this;
		roadsList.Add(lastRoad);
		int count = 0;
        while(count < 4)
		{
            RoadData selRoad = roads[Random.Range(0, roads.Count)];
			int rand = Random.Range(0, lastRoad.spawnPoints.Count - 1);
			lastRoad = Instantiate(selRoad, lastRoad.spawnPoints[rand].transform.position, lastRoad.spawnPoints[rand].transform.rotation);
			lastRoad.transform.SetParent(transform);
			lastRoad.roadGenerator = this;
			roadsList.Add(lastRoad);
            count++;
		}
    }

    public void CheckRoad(int instanceId) {
        if (GetMiddleRoad().GetInstanceID() == instanceId)
		{
			RoadData selRoad = roads[Random.Range(0, roads.Count)];
			int rand = Random.Range(0, lastRoad.spawnPoints.Count - 1);
            if (roadsListNotActive.Any(r => r.id == selRoad.id))
            {
                var road = roadsListNotActive.FirstOrDefault(r => r.id == selRoad.id);
                road.transform.position = lastRoad.spawnPoints[rand].transform.position;
                road.transform.rotation = lastRoad.spawnPoints[rand].transform.rotation;
                road.SetActive(true);
				lastRoad = road;
				roadsList.Add(lastRoad);
                roadsListNotActive.Remove(road);
			}
            else
			{
				lastRoad = Instantiate(selRoad, lastRoad.spawnPoints[rand].transform.position, lastRoad.spawnPoints[rand].transform.rotation);
				lastRoad.transform.SetParent(transform);
				lastRoad.roadGenerator = this;
				roadsList.Add(lastRoad);
			}
            DeleteLastRoad();
		}
    }

    private void DeleteLastRoad() {
        var t = roadsList[0];
        roadsList.Remove(t);
        roadsListNotActive.Add(t);
        t.gameObject.SetActive(false);
    }

    private RoadData GetMiddleRoad() {
        return roadsList[(roadsList.Count % 2 + roadsList.Count / 2) - 1];
	}
}
