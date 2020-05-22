using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class GameUI : MonoBehaviour
{

    public PlayerUIContainer[] playerUIContainters;

    //Displayed text announcing the winner
    public TextMeshProUGUI winText;

    // Simple Singleton
    public static GameUI instance;

    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        InitializePlayerUI();
    }

    void InitializePlayerUI()
    {
        // Loops through the player containers displayed in the UI.
        for (int i = 0; i < playerUIContainters.Length; i++)
        {
            PlayerUIContainer container = playerUIContainters[i];

            // Only for the current amount of connected players
            if (i < PhotonNetwork.PlayerList.Length)
            {
                container.obj.SetActive(true);
                container.nameText.text = PhotonNetwork.PlayerList[i].NickName;
                container.hatTimeSlider.maxValue = GameManager.instance.timeToWin;
            }

            else
            {
                container.obj.SetActive(false);
            }
        }
    }

    void Update()
    {
        UpdatePlayerUI();
    }


    void UpdatePlayerUI()
    {
        // Updates the UI slider with the hat time for each player
        for (int i = 0; i < GameManager.instance.players.Length; i++)
        {
            if (GameManager.instance.players[i] != null)
            {
                playerUIContainters[i].hatTimeSlider.value = GameManager.instance.players[i].curHatTime;
            }
        }
    }

   public void SetWinText(string winnerName)
    {
        winText.gameObject.SetActive(true);
        winText.text = winnerName + " wins!";
    }

}

// Class that holds info for each player's UI elements
[System.Serializable]
public class PlayerUIContainer
{
    public GameObject obj;
    public TextMeshProUGUI nameText;
    public Slider hatTimeSlider;
}