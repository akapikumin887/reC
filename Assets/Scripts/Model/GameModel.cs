using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UniRx;
using UnityEngine;

public class GameModel : IModel
{
    private readonly int QUIZ_COUNT = 3;
    public int nowQuizCount;

    public List<int> quizNum { get; private set; } = new();
    private bool[] selected = new bool[9];

    public Subject<Unit> wrongSubject = new();
    public Subject<GameModel> nextQuizSubject = new();

    public void Initialize(ref List<QuizTemplate> quizTemplates, StringReader reader)
    {
        nowQuizCount = 0;

        for (int i = 0; i < selected.Length; i++)
        {
            selected[i] = false;
        }

        SetQuiz(ref quizTemplates, reader);
        ChoiceQuiz(quizTemplates);
    }

    private void SetQuiz(ref List<QuizTemplate> quizTemplates, StringReader reader)
    {
        for (int i = 0; i < 25; i++)
        {
            // ��蕶�̐��������[�v���񂵁A����������
            string line = reader.ReadLine().Replace("\"", "");
            if (!Regex.IsMatch(line, "�I��|�M�L|�p�Y��"))
                continue;

            quizTemplates.Add(new QuizTemplate(line));
        }
    }

    private void ChoiceQuiz(List<QuizTemplate> quizTemplates)
    {
        // ��肪3��Ȃ��ꍇ�̓Q�[�����J�n�ł��Ȃ��̂�return����
        if (quizTemplates.Count < 3)
        {
            Debug.Log("��萔������܂���");
            return;
        }

        // �擾������肩�烉���_����3��𒊏o����
        System.Random rnd = new();
        while(quizNum.Count < QUIZ_COUNT)
        {
            int num = rnd.Next(1, quizTemplates.Count);
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
                // �s�����Ȃ̂ŐԎ��Ōx�����o��
                wrongSubject.OnNext(Unit.Default);
                return;
            }
        }

        // �����Ȃ̂Ŏ��̖����o��
        nextQuizSubject.OnNext(this);
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

    public IObservable<Unit> WrongSubject
    {
        get { return wrongSubject; }
    }

    public IObservable<GameModel> NextQuizSubject
    {
        get{ return nextQuizSubject; }
    }


}
