using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    Transform playerTransformRef;
    [SerializeField]
    float destroyOnDistance = 10.0f;
    [SerializeField]
    bool canCreateObstaclesBool = true;

    [SerializeField]
    GameObject[] obstaclesArray;
    [SerializeField]
    Transform[] obstaclePotantialPositionsArray;

    // Start is called before the first frame update
    void Start()
    {
        playerTransformRef = GameObject.FindGameObjectWithTag("Player").transform;

        if(canCreateObstaclesBool)
        {
            CreateObstacles();
        }
    }

    // Update is called once per frame
    void Update()
    {
        DestroyPlatform();
    }

    void DestroyPlatform()
    {
        if(transform.position.x - playerTransformRef.position.x < -destroyOnDistance)
        {
            Destroy(gameObject);
        }
    }

    void CreateObstacles()
    {
        int numberOfObstacles = Random.Range(1, 3);

        if (numberOfObstacles == 1)
        {
            Instantiate(obstaclesArray[Random.Range(0, 3)], obstaclePotantialPositionsArray[Random.Range(0, 2)].position, Quaternion.identity, this.transform);
        }
        else
        {
            Instantiate(obstaclesArray[Random.Range(0, 3)], obstaclePotantialPositionsArray[0].position, Quaternion.identity, this.transform);
            Instantiate(obstaclesArray[Random.Range(0, 3)], obstaclePotantialPositionsArray[1].position, Quaternion.identity, this.transform);
        }
    }
}