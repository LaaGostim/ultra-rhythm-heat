using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelSelecter : MonoBehaviour
{

    public GameObject theLevelsPanel;
    public AudioClip[] songs;
    int currentSong = 0;

    private void Start()
    {
        GetComponentInChildren<AudioSource>().Stop();
        GetComponentInChildren<AudioSource>().clip = songs[currentSong];
        GetComponentInChildren<AudioSource>().Play();
    }

    private void Update()
    {
        if(currentSong != 16)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {


                Vector3 newPosition = new Vector3(22.224f, 0, 0);

                theLevelsPanel.transform.position -= newPosition;

                if (currentSong < songs.Length - 1)
                {
                    
                    GetComponentInChildren<AudioSource>().Stop();
                    currentSong++;
                    GetComponentInChildren<AudioSource>().clip = songs[currentSong];
                    GetComponentInChildren<AudioSource>().Play();
                }
            }
            
        }
        else if (currentSong == 16)
        {

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                theLevelsPanel.transform.localPosition = new Vector3(0, 0, 0);
                GetComponentInChildren<AudioSource>().Stop();
                currentSong = 0;
                GetComponentInChildren<AudioSource>().clip = songs[currentSong];
                GetComponentInChildren<AudioSource>().Play();
            }
  
        }
        
        if (currentSong != 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Vector3 newPosition = new Vector3(22.224f, 0, 0);

                theLevelsPanel.transform.position += newPosition;


                if (currentSong > 0)
                {
                    GetComponentInChildren<AudioSource>().Stop();
                    currentSong--;
                    GetComponentInChildren<AudioSource>().clip = songs[currentSong];
                    GetComponentInChildren<AudioSource>().Play();
                }
            }
            
        }
        else if (currentSong == 0)
        {

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                theLevelsPanel.transform.localPosition = new Vector3(-16000, 0, 0);
                GetComponentInChildren<AudioSource>().Stop();
                currentSong = 16;
                GetComponentInChildren<AudioSource>().clip = songs[currentSong];
                GetComponentInChildren<AudioSource>().Play();
            }
            
        }
    }

}
