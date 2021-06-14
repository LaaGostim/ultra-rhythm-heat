using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour
{
    public AudioSource moveSFX;
    public Animator transiction;

    public Texture rankSS;
    public Texture rankS;
    public Texture rankA;
    public Texture rankB;
    public Texture rankC;
    public Texture rankD;
    public Texture rankNA;

    //Whistle Snake
    public int highscoreWhistleSnake;
    public string rankLetterWhistleSnake;
    public Text highscoreTextWhistleSnake;
    public RawImage rankSpriteWhistleSnake;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveSFX.Play();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(LoadLevel(0));
        }

    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transiction.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);
    }

    void Start()
    {
        //WhistleSnake
        highscoreWhistleSnake = PlayerPrefs.GetInt("TestSceneHighScore", 0);
        rankLetterWhistleSnake = PlayerPrefs.GetString("TestSceneRank", "NA");
        highscoreTextWhistleSnake.text = highscoreWhistleSnake.ToString();

        if (rankLetterWhistleSnake == "SS")
        {
            rankSpriteWhistleSnake.texture = rankSS;
        }
        if (rankLetterWhistleSnake == "S")
        {
            rankSpriteWhistleSnake.texture = rankS;
        }
        if (rankLetterWhistleSnake == "A")
        {
            rankSpriteWhistleSnake.texture = rankA;
        }
        if (rankLetterWhistleSnake == "B")
        {
            rankSpriteWhistleSnake.texture = rankB;
        }
        if (rankLetterWhistleSnake == "C")
        {
            rankSpriteWhistleSnake.texture = rankC;
        }
        if (rankLetterWhistleSnake == "D")
        {
            rankSpriteWhistleSnake.texture = rankD;
        }
        if (rankLetterWhistleSnake == "NA")
        {
            rankSpriteWhistleSnake.texture = rankNA;
        }
    }
}
