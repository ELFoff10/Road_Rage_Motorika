using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private Transform startRoad;


    private Vector3 lastEndPosition;
    private float offset; // constant for single prefab
    private float playerDistanceSpawn = 200f;
    private Transform carTransform;
    private CarController carController;

    private void Awake()
    {
        lastEndPosition = startRoad.transform.Find("EndPosition").position;
        offset = startRoad.transform.Find("View").GetComponent<SpriteRenderer>().bounds.size.y / 2;

        SpawnPart();
    }

    private void Start()
    {
        carTransform = GameObject.FindGameObjectWithTag("Player").transform;
        carTransform.GetComponent<CarAIHandler>().enabled = false;
        carController= carTransform.GetComponent<CarController>();
        carController.IsEndlessMap = true;

    }

    private void Update()
    {
        if (Vector3.Distance(carTransform.position, lastEndPosition) < playerDistanceSpawn)
        {
            SpawnPart();
        }


        float time = GameManager.Instance.GetRaceTime();
        if (time > 0 && (int)time % 5 == 0)
        {
            carController.MaxSpeed += 0.001f;
        }
    }

    private void SpawnPart()
    {
        GameObject part = Instantiate(roadPrefab, new Vector3(0, lastEndPosition.y + offset, 0), Quaternion.identity);
        lastEndPosition = part.transform.Find("EndPosition").position;
    }


}
