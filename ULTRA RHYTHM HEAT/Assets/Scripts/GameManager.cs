using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float gameSpeed;
    public AudioSource theMusic;
    public Text scoreText;
    public Text streakText;
    public Text hpText;
    public Image healthBar;
    public Text gameOverScoreText;
    public Text finalScore;
    public Text notesHitText;
    public Text notesMissedText;

    public Text textSS;
    public Text textS;
    public Text textA;
    public Text textB;
    public Text textC;
    public Text textD;

    public SpriteRenderer spriteSS;
    public SpriteRenderer spriteS;
    public SpriteRenderer spriteA;
    public SpriteRenderer spriteB;
    public SpriteRenderer spriteC;
    public SpriteRenderer spriteD;

    public GameObject deathScreen;
    public GameObject mainScreen;
    public GameObject pauseScreen;
    public GameObject scoreScreen;
    public static GameManager instance;

    public int maxScoreForS;
    public int maxScoreForA;
    public int maxScoreForB;
    public int maxScoreForC;

    public int highscore;

    public int currentScore;
    public int scorePerNote = 100;
    public int notesMissed;
    public int notesHit;

    public float healthPoints = 100;
    public float hpPerNote = 5;
    public float maxHealth = 100;
    public float lerpSpeed;

    public bool isPaused;
    public bool isDead;
    public bool gameHasEnded;

    int streak = 0;
    int multiplier = 1;


    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("TestSceneHighScore", 0);
        //PlayerPrefs.DeleteKey("TestSceneHighScore");              DELETE HIGHSCORE

        gameHasEnded = false;
        notesMissed = 0;
        notesHit = 0;
        Cursor.visible = false;
        isPaused = false;
        instance = this;
        Time.timeScale = gameSpeed;
        theMusic.pitch = gameSpeed;
        isDead = false;
        
        healthPoints = 100;
        hpPerNote = 5;
        hpText.text = "HP: " + healthPoints;

        gameOverScoreText.text = "SCORE: 00000";

        scoreText.text = "SCORE: 0";
        streakText.text = "x1";
        currentScore = 0;
        streak = 0;
        multiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = "HP: " + healthPoints;

        lerpSpeed = 3f * Time.deltaTime;
        HealthBarFiller();

        if(healthPoints <= 0)
        {
            gameOverScoreText.text = "SCORE: " + currentScore;
            Time.timeScale = 0;
            deathScreen.SetActive(true);
            Cursor.visible = true;
            theMusic.Stop();
            isDead = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape) && isDead == false && gameHasEnded == false)
        {
            if(isPaused == false)
            {
                Debug.Log("PAUSED");
                Time.timeScale = 0.0f;
                pauseScreen.SetActive(true);
                Cursor.visible = true;
                theMusic.Pause();
                isPaused = true;
            }
            else
            {
                Debug.Log("UNPAUSED");
                Time.timeScale = 1.0f;
                pauseScreen.SetActive(false);
                Cursor.visible = false;
                theMusic.UnPause();
                isPaused = false;
            }
        }
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, healthPoints / maxHealth, lerpSpeed);
    }

    public void NoteHit()
    {
        notesHit++;

        streak++;
        if(streak >= 24)
        {
            multiplier = 5;
            currentScore += scorePerNote * multiplier;
        }
        else if (streak >= 16)
        {
            multiplier = 4;
            currentScore += scorePerNote * multiplier;
        }
        else if (streak >= 8)
        {
            multiplier = 3;
            currentScore += scorePerNote * multiplier;
        }
        else if (streak >= 4)
        {
            multiplier = 2;
            currentScore += scorePerNote * multiplier;
        }
        else
        {
            multiplier = 1;
            currentScore += scorePerNote * multiplier;
        }

        if (healthPoints < 100)
        {
            healthPoints = healthPoints + hpPerNote;
        }

        scoreText.text = "SCORE: " + currentScore;
        streakText.text = "x" + multiplier;
    }

    public void NoteMiss()
    {
        healthPoints = healthPoints - hpPerNote;

        streak = 0;
        multiplier = 1;
        scoreText.text = "SCORE: " + currentScore;
        streakText.text = "x" + multiplier;

        notesMissed++;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "longnote")
        {
            if(collision.gameObject.name != "FirstNote")
            {
                if(collision.gameObject.tag != "endmap")
                {
                    Destroy(collision.gameObject);
                    NoteMiss();
                }
            }
            
        }

        if(collision.gameObject.tag == "endmap")
        {
            gameHasEnded = true;

            if(currentScore > PlayerPrefs.GetInt("TestSceneHighScore", 0))
            {
                PlayerPrefs.SetInt("TestSceneHighScore", currentScore);
                Debug.Log("NEW HIGHSCORE CONGRATS!!");
            }
            Debug.Log("HIGHSCORE: " + highscore);

            finalScore.text = currentScore.ToString();
            notesHitText.text = notesHit.ToString();
            notesMissedText.text = notesMissed.ToString();

            if(currentScore >= maxScoreForS && notesMissed <= 0)
            {
                textSS.gameObject.SetActive(true);
                spriteSS.gameObject.SetActive(true);
            }
            else if (currentScore >= maxScoreForS)
            {
                textS.gameObject.SetActive(true);
                spriteS.gameObject.SetActive(true);
            }
            else if (currentScore >= maxScoreForA)
            {
                textA.gameObject.SetActive(true);
                spriteA.gameObject.SetActive(true);
            }
            else if (currentScore >= maxScoreForB)
            {
                textB.gameObject.SetActive(true);
                spriteB.gameObject.SetActive(true);
            }
            else if (currentScore >= maxScoreForC)
            {
                textC.gameObject.SetActive(true);
                spriteC.gameObject.SetActive(true);
            }
            else
            {
                textD.gameObject.SetActive(true);
                spriteD.gameObject.SetActive(true);
            }

            Debug.Log("GAME ENDED");
            scoreScreen.SetActive(true);
            mainScreen.SetActive(false);
            Cursor.visible = true;
            theMusic.Stop();

        }
    }
}
