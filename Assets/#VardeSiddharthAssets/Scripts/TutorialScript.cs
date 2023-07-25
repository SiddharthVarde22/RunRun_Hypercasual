using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && gameObject.CompareTag("TutorialTrigger"))
        {
            other.gameObject.GetComponent<PlayerMovements>().SetTutorialIsRunning(true);
            Time.timeScale = 0;
        }
    }
}
