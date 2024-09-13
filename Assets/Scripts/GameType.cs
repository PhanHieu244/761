using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameType : MonoBehaviour
{
    [SerializeField] private Button btnFriend, btnComputer,btnOnline;
    [SerializeField] RectTransform rect;
   

    public static GameType Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        btnFriend.onClick.AddListener(() => { OnFriend(); });
        btnComputer.onClick.AddListener(() => { OnComputer(); });

       

    }
   
    private void OnFriend()
    {
        StartCoroutine(AnimationHelper.IButtonClick(btnFriend, () => { FriendCallback(); }));


    }
    private void FriendCallback()
    {

        

        GameData.isPlayWithAI = false;

        ScreenTransController.Instance.ChangeStage(ScreenTransController.STAGE.GAME_CHOSE);

    }
    private void OnComputer()
    {
        StartCoroutine(AnimationHelper.IButtonClick(btnComputer, () => { ComputerCallback(); }));
    }

    private void ComputerCallback()
    {
        
        GameData.isPlayWithAI = true;
        ScreenTransController.Instance.ChangeStage(ScreenTransController.STAGE.GAME_CHOSE);

    }
    public void Hide()
    {
        rect.anchoredPosition = new Vector2(2000,0);
    }
    public void Show()
    {
        rect.anchoredPosition = Vector2.zero;
    }
}
