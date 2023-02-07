using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameController gamePlay = null;


    private bool buttonClickeable = false;

    public void Initialize()
    {
        buttonClickeable = true;
        gameObject.GetComponent<Button>().interactable = false;
        gameObject.transform.GetChild(0).GetComponent<Text>().text = "";
    }

    public bool IsClickable()
    {
        return buttonClickeable;
    }

    public void ButtonClicked()
    {
        buttonClickeable = false;
        gameObject.GetComponent<Button>().interactable = false;
        gameObject.transform.GetChild(0).GetComponent<Text>().text = gamePlay.GetButtonText();
        gamePlay.ChangeTurn();
    }
}
