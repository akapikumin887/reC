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
    // 画像は9枚の3問分用意したいので読み込み方を考える
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
        // 問題が格納されているスプレッドシートの情報を取得
        spreadsheetReader = new();
        reader = new(await spreadsheetReader.LoadSpreadSheet());

        if (reader == null)
        {
            Debug.Log("エラーが発生しました");
            return false;
        }

        return true;
    }

    private void SetQuiz(ref List<QuizTemplate> quizTemplates)
    {
        for (int i = 0; i < 25; i++)
        {
            // 問題文の数だけループを回し、初期化する
            string line = reader.ReadLine().Replace("\"", "");
            if (!Regex.IsMatch(line, "選択|筆記|パズル"))
                continue;

            quizTemplates.Add(new QuizTemplate(line));
        }
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
