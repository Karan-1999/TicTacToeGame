using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameController gamePlay = null;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClicked);  
    }

    public void Initialize()
    {
        gameObject.GetComponent<Button>().interactable = false;
        gameObject.transform.GetChild(0).GetComponent<Text>().text = "";
    }

    public void ButtonClicked()
    {
        gameObject.GetComponent<Button>().interactable = false;
        gameObject.transform.GetChild(0).GetComponent<Text>().text = gamePlay.GetButtonText();
        gamePlay.ChangeTurn();
    }
}
