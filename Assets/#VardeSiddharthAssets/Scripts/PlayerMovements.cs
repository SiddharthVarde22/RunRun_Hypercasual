using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField]
    float speed = 3;
    [SerializeField]
    int minSpeed, maxSpeed;
    [SerializeField]
    float jumpForce = 5;
    [SerializeField]
    float raycstDistance = 0.5f;
    [SerializeField]
    Transform raycasterTransform;

    bool canJump;
    bool tutorialIsRunning = false;
    Touch touchInput;

    Rigidbody playerRigidbody;
    Animator playerAnimator;
    GameManager gameManagerRef;

    [SerializeField]
    AudioClip jumpClip, dieClip;
    AudioSource jadeAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        gameManagerRef = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        jadeAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        canJump = Physics.Raycast(raycasterTransform.position, Vector3.down, raycstDistance);

        transform.position += (Vector3.right * speed * Time.deltaTime);

        if(Input.touchCount > 0)
        {
            touchInput = Input.GetTouch(0);

            if(touchInput.phase == TouchPhase.Began)
            {
                if(Time.timeScale == 0 && tutorialIsRunning)
                {
                    Time.timeScale = 1;
                    tutorialIsRunning = false;
                    gameManagerRef.tutorialImage.SetActive(false);
                }
                Jump();
            }
        }
    }

    public void Jump()
    {
        if(canJump)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
            playerAnimator.SetTrigger("Jumping");
            jadeAudioSource.clip = jumpClip;
            jadeAudioSource.Play();
        }
    }

    public void SetTutorialIsRunning(bool value)
    {
        tutorialIsRunning = value;
        gameManagerRef.tutorialImage.SetActive(value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            gameManagerRef.OnGameOver();
            jadeAudioSource.clip = dieClip;
            jadeAudioSource.Play();
        }
    }
}
