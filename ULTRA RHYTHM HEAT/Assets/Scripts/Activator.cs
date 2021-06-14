using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public KeyCode keyToPress;
    public bool createMode;
    public GameObject n;
    bool active;

    public bool isLongNote;
    public bool isPressable;
    public bool isPressing;
    public bool readyForScore;

    GameObject note;
    SpriteRenderer sr;

    public Sprite pressedSprite;
    public Sprite defaultSprite;

    public Note noteInstance;


    void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        isLongNote = false;
        isPressing = false;
        isPressable = false;
        readyForScore = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(createMode == true)
        {
            if(Input.GetKeyDown(keyToPress))
            {
                Instantiate(n, transform.position, Quaternion.identity);
            }
        }
        else if(createMode == false && GameManager.instance.isPaused == false && GameManager.instance.isDead == false)
        {
            //IS NORMAL NOTE
            if (Input.GetKeyDown(keyToPress) && active == true && isLongNote == false)
            {
                Destroy(note);
                GameManager.instance.NoteHit();
            }
            else if (Input.GetKeyDown(keyToPress) && active == false && isLongNote == false)
            {
                Debug.Log("SHORT | Wrong Key");
                Destroy(note);
                GameManager.instance.NoteMiss();
            }

            //IS LONG NOTE
            if (Input.GetKeyDown(keyToPress) && active == true && isLongNote == true && isPressable == true)
            {
                Debug.Log("LONG | Pressing");
                isPressing = true;

                readyForScore = true;

                SpriteRenderer[] spritesInNote = note.gameObject.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer i in spritesInNote)
                {
                    if (i.gameObject.transform.parent != null)
                    {
                        i.color = new Color32(255, 109, 109, 255);
                    }
                }
            }
            else if (Input.GetKeyDown(keyToPress) && active == false && isLongNote == true && isPressable == true)
            {
                Debug.Log("LONG | Wrong Key");
                GameManager.instance.NoteMiss();
            }

            if (isPressing == true)
            {
                if (Input.GetKeyUp(keyToPress) && isLongNote == true)    //Release early, make color red
                {
                    Debug.Log("LONG | Released Early");
                    isPressable = false;
                    readyForScore = false;

                    SpriteRenderer[] spritesInNote = note.gameObject.GetComponentsInChildren<SpriteRenderer>();
                    foreach (SpriteRenderer i in spritesInNote)
                    {
                        if (i.gameObject.transform.parent != null)
                        {
                            i.color = new Color32(24, 25, 29, 255);
                        }
                    }

                }
            }

            //CHANGE ACTIVATOR SPRITE ON CHANGE
            if (Input.GetKeyDown(keyToPress))
            {
                sr.sprite = pressedSprite;
            }

            if (Input.GetKeyUp(keyToPress))
            {
                sr.sprite = defaultSprite;
            }

        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "longnote")
        {
            note = col.gameObject;
            isLongNote = true;
            isPressable = true;
        }


        active = true;
        if(col.gameObject.tag == "note")
        {
            note = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "FirstNote")
        {
            Debug.Log("First Note");
            isPressable = false;

            if(isPressing == false)
            {
                SpriteRenderer[] spritesInNote = note.gameObject.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer i in spritesInNote)
                {
                    if (i.gameObject.transform.parent != null)
                    {
                        i.color = new Color32(24, 25, 29, 255);
                    }
                }
            }

        }

        if (col.gameObject.tag == "longnote")
        {
            isLongNote = false;
            isPressable = false;
            if(readyForScore == true)   //Add score and remove object
            {
                GameManager.instance.NoteHit();
                Destroy(note);
                Debug.Log("DESTROY");
                readyForScore = false;
            }
            else
            {
                GameManager.instance.NoteMiss();
            }
        }
        active = false;
    }

    void OnTriggerStay2D(Collider2D col)
    {
    }

}
