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

    private bool isSubscribe;

    private void Awake()
    {
        isSubscribe = false;
    }

    public void Initialize(Director d)
    {
        director = d;

        gameModel = new();

        gameModel.Initialize(spreadSheetData.quizTemplates);
        gameView.Initialize(spreadSheetData.quizTemplates, gameModel.quizNum, gameModel.nowQuizCount);

        AddSubject();
    }

    private void AddSubject()
    {
        // �s��������
        gameModel.wrongSubject.Subscribe(_ => gameView.WrongAnswer()).AddTo(gameObject);

        // ��������
        gameModel.nextQuizSubject.Subscribe(model => {
            gameModel.CorrectAnswer();
            gameView.CorrectAnswer(model, spreadSheetData.quizTemplates[gameModel.quizNum[gameModel.nowQuizCount]]);
        }).AddTo(gameObject);

        // ���U���g�J��
        gameModel.resultTransitionSubject.Subscribe(model => {
            gameView.TransitionScene();
            director.ChangePresenter(director._ResultPresenter);
        }).AddTo(gameObject);

        if (isSubscribe)
            return;

        // ���딻��
        gameView._Judge.OnClickAsObservable().Subscribe(_ => gameModel.JudgeQuiz(spreadSheetData.quizTemplates)).AddTo(gameObject);

        for (int i = 0; i < gameView._QuizChoices.Length; i++)
        {
            int index = i;
            gameView._QuizChoices[index].OnClickAsObservable().Subscribe(_ => {
                gameView.ExchangeCheckmark(index).Forget();
                gameModel.SelectImages(index);
            }).AddTo(gameObject);
        }

        isSubscribe = true;
    }

    private void OnDestroy()
    {
        
    }
}
