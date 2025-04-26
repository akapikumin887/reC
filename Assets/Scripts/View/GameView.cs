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
    [SerializeField]
    private Button[] quizChoices = new Button[9];
    public Button[] _QuizChoices => quizChoices;
    [SerializeField]
    private Image[] checkImages = new Image[9];
    public Image[] _CheckImages => checkImages;
    [SerializeField]
    private TextMeshProUGUI quizSentence;
    public TextMeshProUGUI _QuizSentence => quizSentence;
    [SerializeField]
    private Button judge;
    public Button _Judge => judge;
    [SerializeField]
    private TextMeshProUGUI wrongText;
    public TextMeshProUGUI _WrongText => wrongText;

    private Sprite[] quizImageStrage = new Sprite[27];

    private Vector2 selectedImageScale= new Vector2(0.75f, 0.75f);
    private Vector2 nonSelectedImageScale = new Vector2(1.0f, 1.0f);

    private CancellationTokenSource cancellationTokenSource = new();

    public void Initialize(List<QuizTemplate> quizTemplates, List<int> quizNum, int nowQuizCount)
    {
        if(quizNum.Count == 0)
        {
            Debug.Log("問題が設定されていません");
            return;
        }

        // AssetBundleを使って画像の読み込みを行う
        ConvertQuiz(quizNum, quizTemplates).Forget();

        quizSentence.text = quizTemplates[quizNum[nowQuizCount]].text;
        TransitionScene(true);
    }

    public async UniTaskVoid ExchangeCheckmark(int num, CancellationToken ct = default)
    {
        // チェックマークの付与
        checkImages[num].enabled = !checkImages[num].enabled;

        bool enable = checkImages[num].enabled;
        for (int i = 0; i < ChangeButtonFrame; i++)
        {
            if (enable)
                quizChoices[num].gameObject.transform.localScale = 
                    new Vector2(quizChoices[num].gameObject.transform.localScale.x - ChangeButtonSizeSpeed, 
                                quizChoices[num].gameObject.transform.localScale.y - ChangeButtonSizeSpeed);
            else
                quizChoices[num].gameObject.transform.localScale = 
                    new Vector2(quizChoices[num].gameObject.transform.localScale.x + ChangeButtonSizeSpeed, 
                                quizChoices[num].gameObject.transform.localScale.y + ChangeButtonSizeSpeed);
            
            await UniTask.DelayFrame(1, cancellationToken: ct);
        }

        if (enable)
            quizChoices[num].gameObject.transform.localScale = selectedImageScale;
        else
            quizChoices[num].gameObject.transform.localScale = nonSelectedImageScale;
    }

    public void TransitionScene(bool flag)
    {
        gameObject.SetActive(flag);
    }

    private async UniTask ConvertQuiz(List<int> quizNum, List<QuizTemplate> quizTemplates)
    {
        List<string> imageNames = new();

        foreach (int num in quizNum)
        {
            for (int i = 0; i < 9; i++)
            {
                imageNames.Add(ConvertDoubleDigitNum(num) + "_" + ConvertDoubleDigitNum(quizTemplates[num].usedImageList[i].useImageNum));
            }
        }
        imageNames.Add("checkmark");

        List<Sprite> sprites = await LoadFromFileManager.LoadAssets(imageNames);

        for (int i = 0; i < quizImageStrage.Length; i++)
        {
            quizImageStrage[i] = sprites[i];
        }
        LoadFromFileManager.Unload();

        for (int i = 0; i < quizChoices.Length; i++)
        {
            quizChoices[i].image.sprite = quizImageStrage[i];

            // 最後尾に格納したチェックマークの画像をここにすべて格納する
            checkImages[i].sprite = sprites[^1];
        }
    }

    public void WrongAnswer()
    {
        wrongText.enabled = true;
    }

    public void CorrectAnswer(GameModel model, QuizTemplate quizTemplate)
    {
        wrongText.enabled = false;

        for (int i = 0; i < 9; i++)
        {
            int num = (model.nowQuizCount) * 9 + i;
            quizChoices[i].image.sprite = quizImageStrage[num];
            quizChoices[i].gameObject.transform.localScale = nonSelectedImageScale;
            checkImages[i].enabled = false;
        }
        quizSentence.text = quizTemplate.text;
    }

    private string ConvertDoubleDigitNum(int num)
    {
        string str = string.Empty;

        if (num / 10 <= 0)
            str += "0";

        return str + num;
    }

    private void OnDestroy()
    {

    }
}
