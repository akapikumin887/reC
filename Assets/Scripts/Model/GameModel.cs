using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class GameModel : IModel
{
    private readonly int QUIZ_COUNT = 3;
    private SpreadsheetReader spreadsheetReader;
    private StringReader reader;

    public List<int> quizNum { get; private set; } = new();
    // �摜��9����3�╪�p�ӂ������̂œǂݍ��ݕ����l����
    private bool[] selected = new bool[9];

    public void Initialize() { }

    public void Initialize(ref List<QuizTemplate> quizTemplates)
    {
        for (int i = 0; i < selected.Length; i++)
        {
            selected[i] = false;
        }


        SetQuiz(ref quizTemplates);
        ChoiceQuiz(quizTemplates);
    }

    public async UniTask<bool> StartGame()
    {
        // ��肪�i�[����Ă���X�v���b�h�V�[�g�̏����擾
        spreadsheetReader = new();
        reader = new(await spreadsheetReader.LoadSpreadSheet());

        if (reader == null)
        {
            Debug.Log("�G���[���������܂���");
            return false;
        }

        return true;
    }

    private void SetQuiz(ref List<QuizTemplate> quizTemplates)
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
}
