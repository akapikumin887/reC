using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using UniRx;
using UnityEngine;

public class GamePresenter : MonoBehaviour, IPresenter
{
    [field : SerializeField]
    private SpreadSheetData spreadSheetData;

    private Director director;

    [SerializeField]
    private GameView gameView;
    private GameModel gameModel;
    private List<QuizTemplate> quizTemplates;

    public void Initialize(Director d)
    {
        director = d;

        gameModel = new();
        quizTemplates = new();

        gameModel.Initialize(ref quizTemplates, spreadSheetData.reader);
        gameView.Initialize(gameModel, quizTemplates);

        for (int i = 0; i < gameView.quizChoices.Length; i++)
        {
            int index = i;
            gameView.quizChoices[index].OnClickAsObservable().Subscribe(_ =>
            {
                gameView.ExchangeCheckmark(index).Forget();
                gameModel.SelectImages(index);
            }).AddTo(gameObject);
        }

        // ���딻��
        gameView.judge.OnClickAsObservable().Subscribe(_ =>
        {
            gameModel.JudgeQuiz(quizTemplates);
        }).AddTo(gameObject);

        // �s��������
        gameModel.wrongSubject.Subscribe(_ => gameView.WrongAnswer()).AddTo(gameObject);

        // ��������
        gameModel.nextQuizSubject.Subscribe(model =>
        {
            gameModel.CorrectAnswer();
            gameView.CorrectAnswer(model, quizTemplates[gameModel.quizNum[gameModel.nowQuizCount]]);
        }).AddTo(gameObject);
    }

    public void Dispose()
    {

    }
}
