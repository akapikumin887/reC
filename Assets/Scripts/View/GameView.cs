using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour, IView
{
    [SerializeField]
    private float ChangeButtonSizeSpeed;
    [SerializeField]
    private float ChangeButtonFrame;
    [field: SerializeField]
    public GameObject self {  get; private set; }
    [field: SerializeField]
    public Button[] buttons { get; private set; } = new Button[9];
    [field: SerializeField]
    public Image[] images { get; private set; } = new Image[9];

    private CancellationTokenSource cancellationTokenSource = new();

    public void Initialize() { }

    public void Initialize(GameModel gameModel, List<QuizTemplate> quizTemplates)
    {
        if(gameModel.quizNum.Count == 0)
        {
            Debug.Log("��肪�ݒ肳��Ă��܂���");
            return;
        }

        // AssetBundle���g���ĉ摜�̓ǂݍ��݂��s��
#pragma warning disable CS4014 // ���̌Ăяo���͑ҋ@����Ȃ��������߁A���݂̃��\�b�h�̎��s�͌Ăяo���̊�����҂����ɑ��s����܂�
        ConvertQuiz(gameModel, quizTemplates);
#pragma warning restore CS4014 // ���̌Ăяo���͑ҋ@����Ȃ��������߁A���݂̃��\�b�h�̎��s�͌Ăяo���̊�����҂����ɑ��s����܂�

        TransitionGameScreen(self);
    }

    public async UniTask ExchangeCheckmark(int num, CancellationToken ct = default)
    {
        // �`�F�b�N�}�[�N�̕t�^
        images[num].enabled = !images[num].enabled;

        bool enable = images[num].enabled;
        // �{�^���̃T�C�Y������������R���[�`�����쐬(UniTask)
        for (int i = 0; i < 10; i++)
        {
            if (enable)
                buttons[num].gameObject.transform.localScale = new Vector2(buttons[num].gameObject.transform.localScale.x - 0.025f, buttons[num].gameObject.transform.localScale.y - 0.025f);
            else
                buttons[num].gameObject.transform.localScale = new Vector2(buttons[num].gameObject.transform.localScale.x + 0.025f, buttons[num].gameObject.transform.localScale.y + 0.025f);
            
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
                imageNames.Add(ConvertDoubleDigitNum(num) + "_" + ConvertDoubleDigitNum(quizTemplates[num - 1].usedImageList[i]));
            }
        }
        imageNames.Add("checkmark");

        List<Sprite> sprites = await LoadFromFileManager.LoadAssets(imageNames);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.sprite = sprites[i];

            // �Ō���Ɋi�[�����`�F�b�N�}�[�N�̉摜�������ɂ��ׂĊi�[����A���������ǂ������͍���
            images[i].sprite = sprites[sprites.Count - 1];
        }

        LoadFromFileManager.Unload();
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
