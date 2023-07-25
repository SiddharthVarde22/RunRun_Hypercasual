using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    float scoreFloat;
    [SerializeField]
    int actualScore;
    int highScore;

    [SerializeField]
    Text scoreTextRef;
    [SerializeField]
    GameObject hudPanelRef, gameOverPanelRef;
    [SerializeField]
    Text scoreTextGameOverRef, highScoreTextGameOverRef;
    
    public GameObject tutorialImage;
    // Start is called before the first frame update
    void Start()
    {
        scoreFloat = 0;
        actualScore = 0;

        hudPanelRef.SetActive(true);
        gameOverPanelRef.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        scoreFloat += Time.deltaTime;
        actualScore = (int)scoreFloat;

        scoreTextRef.text = "Score : " + actualScore.ToString();
    }

    public int GetActualScore()
    {
        return actualScore;
    }

    public void OnPausePressed()
    {
        SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
    }

    public void OnGameOver()
    {
        Time.timeScale = 0;
        hudPanelRef.SetActive(false);
        gameOverPanelRef.SetActive(true);

        if(PlayerPrefs.HasKey("PlayerHighScore"))
        {
            highScore = PlayerPrefs.GetInt("PlayerHighScore");

            if(actualScore >= highScore)
            {
                highScore = actualScore;
                PlayerPrefs.SetInt("PlayerHighScore", highScore);
            }
        }
        else
        {
            PlayerPrefs.SetInt("PlayerHighScore", actualScore);
            highScore = actualScore;
        }

        scoreTextGameOverRef.text = "Score : " + actualScore;
        highScoreTextGameOverRef.text = "High Score : " + highScore;
    }

    public void OnGameOverRestartPerssed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnGameOverMainMenuPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void OnShareButtonClicked()
    {
        StartCoroutine(TakeScreenshotAndShare());
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetSubject("My Score in RunRun").SetText("I have Scored " + actualScore + " Points in RunRun").SetUrl("")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }
}
