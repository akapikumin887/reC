using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UniRx;
using UnityEngine;

public class GameModel : IModel
{
    private static readonly int QUIZ_COUNT = 3;
    public int nowQuizCount;

    public List<int> quizNum { get; private set; }
    private bool[] selected = new bool[9];

    public Subject<Unit> wrongSubject = new();
    public Subject<GameModel> nextQuizSubject = new();
    public Subject<Unit> resultTransitionSubject = new();

    public void Initialize(List<QuizTemplate> quizTemplates)
    {
        quizNum = new();
        nowQuizCount = 0;

        for (int i = 0; i < selected.Length; i++)
        {
            selected[i] = false;
        }

        ChoiceQuiz(quizTemplates);
    }

    private void ChoiceQuiz(List<QuizTemplate> quizTemplates)
    {
        // 問題が3問ない場合はゲームが開始できないのでreturnする
        if (quizTemplates.Count < 3)
        {
            Debug.Log("問題数が足りません");
            return;
        }

        // 取得した問題からランダムで3問を抽出する
        System.Random rnd = new();
        while(quizNum.Count < QUIZ_COUNT)
        {
            int num = rnd.Next(0, quizTemplates.Count - 1);
            if (!quizNum.Contains(quizTemplates[num].number) && quizTemplates[num].used)
            {
                quizNum.Add(quizTemplates[num].number);
            }
        }
    }

    public void SelectImages(int num)
    {
        selected[num] = !selected[num];
    }

    public void JudgeQuiz(List<QuizTemplate> quizTemplates)
    {
        for(int i = 0;i < selected.Length;i++)
        {
            if (selected[i] != quizTemplates[quizNum[nowQuizCount]].usedImageList[i].isCorrect)
            {
                // 不正解なので赤字で警告を出す
                wrongSubject.OnNext(Unit.Default);
                return;
            }
        }

        if (nowQuizCount >= 2)
        {
            // 最後の問題なのでリザルト画面へ移行する
            resultTransitionSubject.OnNext(Unit.Default);
        }
        else
        {
            // 正解なので次の問題を出す
            nextQuizSubject.OnNext(this);
        }

    }

    public void CorrectAnswer()
    {
        nowQuizCount++;
        for (int i = 0; i < selected.Length; i++)
        {
            selected[i] = false;
        }
    }

    public void Dispose()
    {
        
    }

    public IObservable<Unit> WrongSubject => wrongSubject;

    public IObservable<GameModel> NextQuizSubject => nextQuizSubject;

    public IObservable<Unit> ResultTransitionSubject => resultTransitionSubject;
}
