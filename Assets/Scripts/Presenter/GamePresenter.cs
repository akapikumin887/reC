using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GamePresenter : MonoBehaviour, IPresenter
{
    [SerializeField]
    private SpreadSheetData spreadSheetData;

    private Director director;

    [SerializeField]
    private GameView gameView;
    public GameView _GameView => gameView;

    private GameModel gameModel;

    private CompositeDisposable com;

    public GamePresenter()
    {
    }

    public void Initialize(Director d)
    {
        com = new CompositeDisposable();
        director = d;

        gameModel = new();

        gameModel.Initialize(spreadSheetData.quizTemplates);
        gameView.Initialize(spreadSheetData.quizTemplates, gameModel.quizNum, gameModel.nowQuizCount);

        // 2週目の際は押下時の処理が倍で入ってしまうので対策が必要
        for (int i = 0; i < gameView._QuizChoices.Length; i++)
        {
            int index = i;
            gameView._QuizChoices[index].OnClickAsObservable().Subscribe(_ => {
                gameView.ExchangeCheckmark(index).Forget();
                gameModel.SelectImages(index);
            }).AddTo(com);
        }

        // 正誤判定
        gameView._Judge.OnClickAsObservable().Subscribe(_ => gameModel.JudgeQuiz(spreadSheetData.quizTemplates)).AddTo(com);

        // 不正解処理
        gameModel.wrongSubject.Subscribe(_ => gameView.WrongAnswer()).AddTo(com);

        // 正解処理
        gameModel.nextQuizSubject.Subscribe(model => {
            gameModel.CorrectAnswer();
            gameView.CorrectAnswer(model, spreadSheetData.quizTemplates[gameModel.quizNum[gameModel.nowQuizCount]]);
        }).AddTo(com);

        // リザルト遷移
        gameModel.resultTransitionSubject.Subscribe(model => {
            gameView.TransitionScene(false);
            director.ChangePresenter(director._ResultPresenter);
            Dispose();
        }).AddTo(com);
    }

    public void Dispose()
    {
        com.Clear();
    }
}
