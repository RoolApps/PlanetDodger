using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Text.RegularExpressions;

public class MenuController : MonoBehaviour
{
    AsyncOperation operation;
    public UnityEngine.UI.Slider Progress;
    public TMPro.TextMeshProUGUI Scores;
    public SpriteRenderer ShipRenderer;
    public UnityEngine.UI.Button SubmitNucknameButton;
    public UnityEngine.UI.InputField NicknameField;
    public GameObject NicknamePanel;
    public GameObject MainMenuPanel;
    public UnityEngine.UI.Button AdvancedShipButton;
    public UnityEngine.UI.Button PowerfulShipButton;

    private Purchaser purchaser;

    private void Awake()
    {
        purchaser = GetComponent<Purchaser>();
        if (purchaser.IsInitialized())
        {
            PurchaserInitialized();
        }
        else
        {
            purchaser.Initialized += PurchaserInitialized;
        }

        if (GameSettings.Current.Nickname == null)
        {
            MainMenuPanel.SetActive(false);
            NicknamePanel.SetActive(true);
            return;
        }
        else
        {
            Debug.Log(String.Format("Current nickname: {0}", GameSettings.Current.Nickname));
        }
        if (GameSettings.Current.AdvancedShipUnlocked)
        {
            AdvancedShipButton.interactable = true;
        }
        if (GameSettings.Current.SelectedShip != null && GameSettings.Current.SelectedShip.Any())
        {
            LoadShip(GameSettings.Current.SelectedShip);
        }
    }

    private void PurchaserInitialized(object sender, EventArgs e)
    {
        purchaser.Initialized -= PurchaserInitialized;
        PurchaserInitialized();
    }

    private void PurchaserInitialized()
    {
        if (purchaser.PurchasedProducts.Contains(Purchaser.NoAds))
        {
            GameSettings.Current.AdsDisabled = true;
        }
        PowerfulShipButton.interactable = true;
    }

    private GameDifficulty.Difficulty GetDifficulty(string difficultyName)
    {
        switch (difficultyName)
        {
            case "Easy":
                return GameDifficulty.Difficulty.Easy;
            case "Medium":
                return GameDifficulty.Difficulty.Medium;
            case "Hard":
                return GameDifficulty.Difficulty.Hard;
            default:
                return GameDifficulty.Difficulty.Unknown;
        }
    }

    public void StartGame(string difficultyName)
    {
        GameDifficulty.Difficulty difficulty = GetDifficulty(difficultyName);
        GameDifficulty.SetDifficulty(difficulty);
        operation = SceneManager.LoadSceneAsync("Scenes/GameplayScene");
    }

    public void LoadScores(string difficultyName)
    {
        Scores.text = "Loading...";
        ScoreManager.GetScores(this, difficultyName, ScoresLoaded);
    }

    private void ScoresLoaded(ScoreManager.Scores scores)
    {
        Scores.text = String.Join(Environment.NewLine, scores.Items.Select(score => String.Format("{0}: {1}", score.name, score.score)).ToArray());
    }

    public void SelectShip(String name)
    {
        if (name == "Powerful")
        {
            var purchaser = GetComponent<Purchaser>();
            if (!purchaser.PurchasedProducts.Contains(Purchaser.PowerfulSpaceship))
            {
                purchaser.BuyPowerfulSpaceship(() => LoadShip(name));
                return;
            }
        }
        LoadShip(name);
    }

    public void BuyNoAds()
    {
        purchaser.BuyNoAds(() =>
        {
            GameSettings.Current.AdsDisabled = true;
        });
    }

    private void LoadShip(String name)
    {
        GameSettings.Current.SelectedShip = name;
    }

    private void LateUpdate()
    {
        if (operation != null)
        {
            Progress.value = operation.progress;
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void NicknameTextChanged(String notUsed)
    {
        var originalText = NicknameField.text;
        var text = Regex.Replace(originalText, @"[^a-zA-Z0-9 ]", "");
        if (text != originalText)
        {
            NicknameField.text = text;
        }
        else
        {
            SubmitNucknameButton.interactable = NicknameField.text.Any();
        }
    }

    public void SubmitNicknameClicked()
    {
        GameSettings.Current.Nickname = NicknameField.text;
        GameSettings.Current.SelectedShip = "Basic";
        NicknamePanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }
}
