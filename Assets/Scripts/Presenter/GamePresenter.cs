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

        // 2�T�ڂ̍ۂ͉������̏������{�œ����Ă��܂��̂ő΍􂪕K�v
        for (int i = 0; i < gameView._QuizChoices.Length; i++)
        {
            int index = i;
            gameView._QuizChoices[index].OnClickAsObservable().Subscribe(_ => {
                gameView.ExchangeCheckmark(index).Forget();
                gameModel.SelectImages(index);
            }).AddTo(com);
        }

        // ���딻��
        gameView._Judge.OnClickAsObservable().Subscribe(_ => gameModel.JudgeQuiz(spreadSheetData.quizTemplates)).AddTo(com);

        // �s��������
        gameModel.wrongSubject.Subscribe(_ => gameView.WrongAnswer()).AddTo(com);

        // ��������
        gameModel.nextQuizSubject.Subscribe(model => {
            gameModel.CorrectAnswer();
            gameView.CorrectAnswer(model, spreadSheetData.quizTemplates[gameModel.quizNum[gameModel.nowQuizCount]]);
        }).AddTo(com);

        // ���U���g�J��
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
