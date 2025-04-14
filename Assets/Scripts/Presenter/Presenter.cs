using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class Presenter : MonoBehaviour
{
    // Title
    [SerializeField]
    private GameObject titleObj;
    [SerializeField]
    private TitleView titleView;
    private TitleModel titleModel;

    // Game
    [SerializeField]
    private GameObject gameObj;
    [SerializeField]
    private GameView gameView;
    private GameModel gameModel;
    private List<QuizTemplate> quizTemplates;


    private void Awake()
    {
        titleModel = new();
        titleModel.Initialize();
        titleView.Initialize();

        gameModel = new();
        quizTemplates = new();
    }

    private void Start()
    {
        // �{�^���������̏�����o�^
        var disposable = titleView.startButton.OnClickAsObservable().Subscribe(_ => GameStart().Forget()).AddTo(gameObject);


        // �l�̕ύX�����m�����ۂ̏�����o�^
        //this.model.PushCounter.Subscribe(countNum => this.counterText.text = countNum.ToString());
    }

    private async UniTaskVoid GameStart()
    {
        // �X�v�V�̎擾��ɉ�ʑJ�ڂ����Ă��邪�擾���̓��[�h��ʂ�\��������
        bool res = await gameModel.StartGame();
        if (res)
        {
            // �^�C�g����ʃt�F�[�h�A�E�g
            titleView.TransitionGameScreen(titleObj);

            gameModel.Initialize(ref quizTemplates);
            gameView.Initialize(gameModel, quizTemplates);

            for (int i = 0; i < gameView.buttons.Length; i++)
            {
                int index = i;
                gameView.buttons[index].OnClickAsObservable().Subscribe(_ => 
                {
#pragma warning disable CS4014 // ���̌Ăяo���͑ҋ@����Ȃ��������߁A���݂̃��\�b�h�̎��s�͌Ăяo���̊�����҂����ɑ��s����܂�
                    gameView.ExchangeCheckmark(index);
#pragma warning restore CS4014 // ���̌Ăяo���͑ҋ@����Ȃ��������߁A���݂̃��\�b�h�̎��s�͌Ăяo���̊�����҂����ɑ��s����܂�
                    gameModel.SelectImages(index);
                }).AddTo(gameObject);
            }
        }
    }
}
