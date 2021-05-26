using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsBackButton : MonoBehaviour
{
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    void OnMouseOver()
    {
        var newScale = new Vector3(1f, 1f, 1f);
        button.transform.localScale = Vector3.Lerp(button.transform.localScale, newScale, 50 * Time.deltaTime);

        Debug.Log("ENTER");
    }

    void OnMouseExit()
    {
        var oldScale = new Vector3(0.8f, 0.8f, 0.8f);
        transform.localScale = Vector3.Lerp(transform.localScale, oldScale, 100 * Time.deltaTime);

        Debug.Log("EXIT");
    }
}
