using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //public
    public GameObject[] interactionButtons = new GameObject[9];
    [Header("UI elements")]
    public GameObject crossButton = null;
    public GameObject circleButton = null;
    public GameObject resultUI = null;
    public Button playAgain = null;
    public Button undo = null;

    //private
    private bool isCross = false;
    private string buttonText = "O";
    private int count = 0;
    private GameObject lastButtonRef = null;
   
    void Start()
    {
        SetupGame();
        playAgain.onClick.AddListener(SetupGame);
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

        resultUI.SetActive(false);

        playAgain.interactable = false;
        undo.interactable = false;
      
        circleButton.GetComponent<Button>().interactable = true;
        crossButton.GetComponent<Button>().interactable = true;
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
            button.GetComponent<Button>().interactable = true; 
        }
    }

    private void GameOver()
    {
        foreach (GameObject button in interactionButtons)
        {
            button.GetComponent<Button>().interactable = false;
        }

        playAgain.interactable = true;
        undo.interactable = false;

        crossButton.transform.Find("HL").gameObject.SetActive(false);
        circleButton.transform.Find("HL").gameObject.SetActive(false);

        resultUI.SetActive(true);
        if (buttonText == "Draw")
            resultUI.transform.Find("Image").Find("Text").GetComponent<Text>().text = buttonText;
        else
            resultUI.transform.Find("Image").Find("Text").GetComponent<Text>().text = buttonText + " Wins";
    }

    private bool CheckGameOver()
    {
        //Game draw condition
        if (count == (interactionButtons.Length - 1))
        {
            print("Game Draw");
            buttonText = "Draw";
            return true;
        }

        //Row_1 win condition
        if(interactionButtons[0].transform.GetChild(0).GetComponent<Text>().text == buttonText && interactionButtons[1].transform.GetChild(0).GetComponent<Text>().text == buttonText && interactionButtons[2].transform.GetChild(0).GetComponent<Text>().text == buttonText)
        {
            print("Row_1");
            return true;
        }
        //Row_2 win condition
        if (interactionButtons[3].transform.GetChild(0).GetComponent<Text>().text == buttonText && interactionButtons[4].transform.GetChild(0).GetComponent<Text>().text == buttonText && interactionButtons[5].transform.GetChild(0).GetComponent<Text>().text == buttonText)
        {
            print("Row_2");
            return true;
        }
        //Row_3 win condition
        if (interactionButtons[6].transform.GetChild(0).GetComponent<Text>().text == buttonText && interactionButtons[7].transform.GetChild(0).GetComponent<Text>().text == buttonText && interactionButtons[8].transform.GetChild(0).GetComponent<Text>().text == buttonText)
        {
            print("Row_3");
            return true;
        }
        //Column_1 win condition
        if (interactionButtons[0].transform.GetChild(0).GetComponent<Text>().text == buttonText && interactionButtons[3].transform.GetChild(0).GetComponent<Text>().text == buttonText && interactionButtons[6].transform.GetChild(0).GetComponent<Text>().text == buttonText)
        {
            print("Column_1");
            return true;
        }
        //Column_2 win condition
        if (interactionButtons[1].transform.GetChild(0).GetComponent<Text>().text == buttonText && interactionButtons[4].transform.GetChild(0).GetComponent<Text>().text == buttonText && interactionButtons[7].transform.GetChild(0).GetComponent<Text>().text == buttonText)
        {
            print("Column_2");
            return true;
        }
        //Column_3 win condition
        if (interactionButtons[2].transform.GetChild(0).GetComponent<Text>().text == buttonText && interactionButtons[5].transform.GetChild(0).GetComponent<Text>().text == buttonText && interactionButtons[8].transform.GetChild(0).GetComponent<Text>().text == buttonText)
        {
            print("Column_3");
            return true;
        }
        //diagonal_1 win condition
        if (interactionButtons[0].transform.GetChild(0).GetComponent<Text>().text == buttonText && interactionButtons[4].transform.GetChild(0).GetComponent<Text>().text == buttonText && interactionButtons[8].transform.GetChild(0).GetComponent<Text>().text == buttonText)
        {
            print("diagonal_1");
            return true;
        }
        //diagonal_2 win condition
        if (interactionButtons[2].transform.GetChild(0).GetComponent<Text>().text == buttonText && interactionButtons[4].transform.GetChild(0).GetComponent<Text>().text == buttonText && interactionButtons[6].transform.GetChild(0).GetComponent<Text>().text == buttonText)
        {
            print("diagonal_2");
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

    public void ChangeTurn(GameObject _button)
    {
        undo.interactable = true;
        lastButtonRef = _button;

        //Check if game is over
        if (CheckGameOver())
        {
            print("Game over");
            GameOver();
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

    public void UndoClicked()
    {
        if (lastButtonRef == null) { return; }

        undo.interactable = false;
        count--;
        lastButtonRef.GetComponent<ButtonController>().Initialize();
        lastButtonRef.GetComponent<Button>().interactable = true;

        isCross = !isCross;
        if (isCross)
            buttonText = "X";
        else
            buttonText = "O";

        SetTurnIndicator();
    }

    public string GetButtonText()
    {
        return buttonText;
    }
    #endregion

}
