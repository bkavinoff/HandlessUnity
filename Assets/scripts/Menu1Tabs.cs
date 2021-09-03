using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu1Tabs : MonoBehaviour
{
    public TMP_InputField inputNombre, inputEmail;
    private EventSystem system;
    // Start is called before the first frame update
    void Start()
    {
        system = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputNombre.isFocused)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                system.SetSelectedGameObject(inputEmail.gameObject, null);
                inputEmail.OnPointerClick(new PointerEventData(EventSystem.current));
            }
        }
        if (inputEmail.isFocused)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                system.SetSelectedGameObject(inputNombre.gameObject, null);
                inputNombre.OnPointerClick(new PointerEventData(EventSystem.current));
            }
        }
    }
}
