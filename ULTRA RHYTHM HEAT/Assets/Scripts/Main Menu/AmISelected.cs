using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AmISelected : MonoBehaviour
{
    GameObject thisObject;

    private void Start()
    {
        thisObject = GetComponent<GameObject>();
    }
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == thisObject.gameObject)
        {
            var newScale = new Vector3(1f, 1f, 1f);
            thisObject.transform.localScale = Vector3.Lerp(thisObject.transform.localScale, newScale, 12 * Time.deltaTime);
        }
    }
}
