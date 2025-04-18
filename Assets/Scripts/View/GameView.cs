using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour, IView
{
    [SerializeField]
    private float ChangeButtonSizeSpeed;
    [SerializeField]
    private float ChangeButtonFrame;
    [field: SerializeField]
    public GameObject self { get; private set; }
    [field: SerializeField]
    public Button[] quizChoices { get; private set; } = new Button[9];
    [field: SerializeField]
    public Image[] images { get; private set; } = new Image[9];
    [field: SerializeField]
    public TextMeshProUGUI quizSentence { get; private set; }
    [field: SerializeField]
    public Button judge { get; private set; }
    [field: SerializeField]
    public TextMeshProUGUI wrongText { get; private set; }


    private CancellationTokenSource cancellationTokenSource = new();

    public void Initialize() { }

    public void Initialize(GameModel gameModel, List<QuizTemplate> quizTemplates)
    {
        if(gameModel.quizNum.Count == 0)
        {
            Debug.Log("問題が設定されていません");
            return;
        }

        // AssetBundleを使って画像の読み込みを行う
#pragma warning disable CS4014 // この呼び出しは待機されなかったため、現在のメソッドの実行は呼び出しの完了を待たずに続行されます
        ConvertQuiz(gameModel, quizTemplates);
#pragma warning restore CS4014 // この呼び出しは待機されなかったため、現在のメソッドの実行は呼び出しの完了を待たずに続行されます

        quizSentence.text = quizTemplates[gameModel.quizNum[0] - 1].text;
        TransitionGameScreen(self);
    }

    public async UniTask ExchangeCheckmark(int num, CancellationToken ct = default)
    {
        // チェックマークの付与
        images[num].enabled = !images[num].enabled;

        bool enable = images[num].enabled;
        for (int i = 0; i < 10; i++)
        {
            if (enable)
                quizChoices[num].gameObject.transform.localScale = 
                    new Vector2(quizChoices[num].gameObject.transform.localScale.x - 0.025f, quizChoices[num].gameObject.transform.localScale.y - 0.025f);
            else
                quizChoices[num].gameObject.transform.localScale = 
                    new Vector2(quizChoices[num].gameObject.transform.localScale.x + 0.025f, quizChoices[num].gameObject.transform.localScale.y + 0.025f);
            
            await UniTask.DelayFrame(1, cancellationToken: ct);

        }
    }

    public void TransitionGameScreen(GameObject obj)
    {
        obj.SetActive(true);
    }

    private async UniTask ConvertQuiz(GameModel gameModel, List<QuizTemplate> quizTemplates)
    {
        List<string> imageNames = new();

        foreach (int num in gameModel.quizNum)
        {
            for (int i = 0; i < 9; i++)
            {
                imageNames.Add(ConvertDoubleDigitNum(num) + "_" + ConvertDoubleDigitNum(quizTemplates[num - 1].usedImageList[i].useImageNum));
            }
        }
        imageNames.Add("checkmark");

        List<Sprite> sprites = await LoadFromFileManager.LoadAssets(imageNames);

        for (int i = 0; i < quizChoices.Length; i++)
        {
            quizChoices[i].image.sprite = sprites[i];

            // 最後尾に格納したチェックマークの画像をここにすべて格納する、もう少し良いやり方模索中
            images[i].sprite = sprites[sprites.Count - 1];
        }

        LoadFromFileManager.Unload();
    }

    public void WrongAnswer()
    {
        // 間違えた際に赤文字を表示
        wrongText.enabled = true;
    }

    private string ConvertDoubleDigitNum(int num)
    {
        string str = string.Empty;

        if (num / 10 <= 0)
            str += "0";

        return str + num;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            LoadFromFileManager.Dispose();
        }
    }
}
