using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    float xOffsetFromPlayer = 0.95f;

    Transform playerTransformRef;
    // Start is called before the first frame update
    void Start()
    {
        playerTransformRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransformRef.position.x + xOffsetFromPlayer, transform.position.y, transform.position.z);
    }
}
