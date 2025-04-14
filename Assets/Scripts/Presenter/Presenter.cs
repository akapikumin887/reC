using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class Presenter : MonoBehaviour
{
    // Title
    [SerializeField]
    private GameObject titleObj;
    [SerializeField]
    private TitleView titleView;
    private TitleModel titleModel;

    // Game
    [SerializeField]
    private GameObject gameObj;
    [SerializeField]
    private GameView gameView;
    private GameModel gameModel;
    private List<QuizTemplate> quizTemplates;


    private void Awake()
    {
        titleModel = new();
        titleModel.Initialize();
        titleView.Initialize();

        gameModel = new();
        quizTemplates = new();
    }

    private void Start()
    {
        // ボタン押下時の処理を登録
        var disposable = titleView.startButton.OnClickAsObservable().Subscribe(_ => GameStart().Forget()).AddTo(gameObject);


        // 値の変更を検知した際の処理を登録
        //this.model.PushCounter.Subscribe(countNum => this.counterText.text = countNum.ToString());
    }

    private async UniTaskVoid GameStart()
    {
        // スプシの取得後に画面遷移をしているが取得時はロード画面を表示したい
        bool res = await gameModel.StartGame();
        if (res)
        {
            // タイトル画面フェードアウト
            titleView.TransitionGameScreen(titleObj);

            gameModel.Initialize(ref quizTemplates);
            gameView.Initialize(gameModel, quizTemplates);

            for (int i = 0; i < gameView.buttons.Length; i++)
            {
                int index = i;
                gameView.buttons[index].OnClickAsObservable().Subscribe(_ => 
                {
#pragma warning disable CS4014 // この呼び出しは待機されなかったため、現在のメソッドの実行は呼び出しの完了を待たずに続行されます
                    gameView.ExchangeCheckmark(index);
#pragma warning restore CS4014 // この呼び出しは待機されなかったため、現在のメソッドの実行は呼び出しの完了を待たずに続行されます
                    gameModel.SelectImages(index);
                }).AddTo(gameObject);
            }
        }
    }
}
