using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField]
    float maxForwardDistance = 20.0f;

    [SerializeField]
    GameObject[] platformPrefabs;

    [SerializeField]
    GameObject LastPlatform, playerRef;

    [SerializeField]
    Vector3 distanceBetweenPlatforms = new Vector3(8.85f, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GeneratePlatform();
    }

    void GeneratePlatform()
    {
        GameObject gameObject;
        if((LastPlatform.transform.position - playerRef.transform.position).magnitude < maxForwardDistance)
        {
            gameObject = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)], 
                LastPlatform.transform.position + distanceBetweenPlatforms, Quaternion.identity);
            LastPlatform = gameObject;
        }
    }
}
