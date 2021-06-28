using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAgain : MonoBehaviour
{
    public void PlayAgain()
    {
        GameManager.Instance.TryAgainBoardtoGreen();
        GameManager.Instance.popupPanel.SetActive(false);       
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

