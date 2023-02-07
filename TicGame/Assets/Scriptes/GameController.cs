using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //public
    public GameObject[] interactionButtons = new GameObject[9];
    public GameObject crossButton = null;
    public GameObject circleButton = null;

    //private
    private bool isCross = false;
    private string buttonText = "O";
    private int count = 0;
   
    void Start()
    {
        SetupGame();
    }

    #region PrivateMethods
    private void SetupGame()
    {
        count = 0;
        isCross = false;
        buttonText = "O";

        foreach (GameObject button in interactionButtons)
        {
            button.GetComponent<ButtonController>().Initialize();
        }
    }

    private void SetTurnIndicator()
    {
        crossButton.transform.Find("HL").gameObject.SetActive(isCross);
        circleButton.transform.Find("HL").gameObject.SetActive(!isCross);
    }

    private void StartGame()
    {
        foreach (GameObject button in interactionButtons)
        {
            if (button.GetComponent<ButtonController>().IsClickable()){
                button.GetComponent<Button>().interactable = true;
            }
        }
    }

    private bool CheckGameOver()
    {
        if (count == (interactionButtons.Length - 1))
        {
            print("Game over");
            return true;
        }

        return false;
    }
    #endregion

    #region PublicMethods
    public void CrossSelected()
    {
        isCross = true;
        buttonText = "X";
        SetTurnIndicator();

        circleButton.GetComponent<Button>().interactable = false;
        crossButton.GetComponent<Button>().interactable = false;

        StartGame();
    }

    public void CircleSelected()
    {
        isCross = false;
        buttonText = "O";
        SetTurnIndicator();

        circleButton.GetComponent<Button>().interactable = false;
        crossButton.GetComponent<Button>().interactable = false;

        StartGame();
    }

    public void ChangeTurn()
    {
        if (CheckGameOver())
        {

        }
        else
        {
            count++;
            isCross = !isCross;
            if (isCross)
                buttonText = "X";
            else
                buttonText = "O";

            SetTurnIndicator();
        }
    }

    public string GetButtonText()
    {
        return buttonText;
    }
    #endregion

}
