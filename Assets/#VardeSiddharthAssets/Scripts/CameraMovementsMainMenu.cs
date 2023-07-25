using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementsMainMenu : MonoBehaviour
{
    [SerializeField]
    float speed = 2, maxRightDistance = 4, maxLeftDistance = 4;
    [SerializeField]
    bool movingRight = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(movingRight)
        {
            transform.position += transform.right * speed * Time.deltaTime;

            if(transform.position.x > maxRightDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position -= transform.right * speed * Time.deltaTime;

            if(transform.position.x < -maxLeftDistance)
            {
                movingRight = true;
            }
        }
    }
}
