using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RowUpgrade : MonoBehaviour
{

    public Button btnUpgrade;
    public TextMeshProUGUI txtInfo;
    public Type type;

    public enum Type { Speed, Power, SpeedAds, PowerAds };

    private int[] speedCoin, powerGem;

    private void Start()
    {
        speedCoin = new int[] { 100, 200, 300, 400 };
        powerGem = new int[] { 5, 10, 15, 20 };
        btnUpgrade.onClick.AddListener(() => { OnUpgrade(); });
        UpdateUI();
    }

    private void UpdateSpeedUI()
    {
        if (GameData.playerState.speedMultiplier >= speedCoin.Length)
        {
            txtInfo.text = "x" + GameData.playerState.speedMultiplier + "\nSpeed";
            btnUpgrade.gameObject.SetActive(false);
        }
        else
        {

            btnUpgrade.gameObject.SetActive(true);

            txtInfo.text = "x" + (GameData.playerState.speedMultiplier + 1) + "\nSpeed";
            int coin = speedCoin[GameData.playerState.speedMultiplier - 1];
            btnUpgrade.GetComponentInChildren<TextMeshProUGUI>().text = coin.ToString();
            btnUpgrade.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void UpdateSpeedAds()
    {
        if (GameData.playerState.speedMultiplier >= speedCoin.Length)
        {
            txtInfo.text = "x" + GameData.playerState.speedMultiplier + "\nSpeed";
            btnUpgrade.gameObject.SetActive(false);
        }
        else
        {

            btnUpgrade.gameObject.SetActive(true);
            txtInfo.text = "x" + (GameData.playerState.speedMultiplier + 1) + "\nSpeed";

        }
    }

    private void UpdatePower()
    {
        if (GameData.playerState.powerMultiplier >= powerGem.Length)
        {
            txtInfo.text = "x" + GameData.playerState.powerMultiplier + "\nPower";
            btnUpgrade.gameObject.SetActive(false);
        }
        else
        {

            btnUpgrade.gameObject.SetActive(true);

            txtInfo.text = "x" + (GameData.playerState.powerMultiplier + 1) + "\nPower";
            int coin = powerGem[GameData.playerState.powerMultiplier - 1];
            btnUpgrade.GetComponentInChildren<TextMeshProUGUI>().text = coin.ToString();
            btnUpgrade.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void UpdatePowerAds()
    {
        if (GameData.playerState.powerMultiplier >= powerGem.Length)
        {
            txtInfo.text = "x" + GameData.playerState.powerMultiplier + "\nPower";
            btnUpgrade.gameObject.SetActive(false);
        }
        else
        {

            btnUpgrade.gameObject.SetActive(true);
            txtInfo.text = "x" + (GameData.playerState.powerMultiplier + 1) + "\nPower";

        }
    }

    private void UpdateUI()
    {

        
        switch (type)
        {
            case Type.Speed:
                UpdateSpeedUI();
                //UpdateSpeedAds();
                break;
            case Type.SpeedAds:
                //UpdateSpeedUI();
                UpdateSpeedAds();
                break;
            case Type.Power:
                UpdatePower();
                //UpdatePowerAds();
                break;
            case Type.PowerAds:
                //UpdatePower();
                UpdatePowerAds();
                break;
        }

        UIController.Instance.UpdateUI();
    }
    private void OnUpgrade()
    {
        StartCoroutine(AnimationHelper.IButtonClick(btnUpgrade, () => { Callback(); }));
    }
    private void Callback()
    {
        switch (type)
        {
            case Type.Speed:
                int coin = GameData.playerState.score;
                int mult = GameData.playerState.speedMultiplier + 1;
                if (coin >= speedCoin[mult - 2])
                {
                    // available coin
                    GameData.playerState.speedMultiplier = GameData.playerState.speedMultiplier + 1;
                    GameData.playerState.score = GameData.playerState.score - speedCoin[mult - 2];

                    GameData.SaveData();

                    MessageBox.Instance.Show("You have successfully upgraded speed");
                    ScreenTransController.Instance.ChangeStage(ScreenTransController.STAGE.UPGRADE_MSG);
                    UpdateUI();

                }
                else
                {
                    ScreenTransController.Instance.ChangeStage(ScreenTransController.STAGE.UPGRADE_MSG);
                    MessageBox.Instance.Show("You don't have enough coin !!!");
                }
                break;
            case Type.SpeedAds:

            case Type.Power:
                coin = GameData.playerState.gem;
                mult = GameData.playerState.powerMultiplier + 1;
                if (coin >= powerGem[mult - 2])
                {
                    // available coin
                    GameData.playerState.powerMultiplier = GameData.playerState.powerMultiplier + 1;
                    GameData.playerState.gem = GameData.playerState.gem - powerGem[mult - 2];

                    GameData.SaveData();

                    MessageBox.Instance.Show("You have successfully upgraded power");
                    ScreenTransController.Instance.ChangeStage(ScreenTransController.STAGE.UPGRADE_MSG);
                    UpdateUI();
                }
                else
                {
                    ScreenTransController.Instance.ChangeStage(ScreenTransController.STAGE.UPGRADE_MSG);
                    MessageBox.Instance.Show("You don't have enough gem !!!");
                }
                break;
            case Type.PowerAds:
                break;
        }
    }

    private void AdWatched(bool isForSpeed)
    {
        if (isForSpeed)
        {
            GameData.playerState.speedMultiplier = GameData.playerState.speedMultiplier + 1;
        }
        else
        {
            GameData.playerState.powerMultiplier = GameData.playerState.powerMultiplier + 1;
        }
        GameData.SaveData();
        UpdateUI();
    }

}
