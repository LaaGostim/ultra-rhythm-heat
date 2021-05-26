using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    GameObject lastSelected;
    public AudioSource moveSFX;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        lastSelected = new GameObject();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            var oldScale = new Vector3(0.8f, 0.8f, 0.8f);
            lastSelected.transform.localScale = Vector3.Lerp(lastSelected.transform.localScale, oldScale, 200 * Time.deltaTime);

            moveSFX.Play();
        }

        if(EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelected);
        }
        else
        {
            lastSelected = EventSystem.current.currentSelectedGameObject;
        }

        var newScale = new Vector3(1f, 1f, 1f);
        lastSelected.transform.localScale = Vector3.Lerp(lastSelected.transform.localScale, newScale, 12 * Time.deltaTime);

    }

    


}
