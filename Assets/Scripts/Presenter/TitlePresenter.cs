using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.IO;

public class TitlePresenter : MonoBehaviour, IPresenter
{
    [field : SerializeField]
    private SpreadSheetData spreadSheetData;

    private Director director;

    [SerializeField]
    private TitleView titleView;
    private TitleModel titleModel;

    public void Initialize(Director director)
    {
        this.director = director;

        titleModel = new();
        titleView.Initialize();

        titleView.startButton.OnClickAsObservable().Subscribe(_ => StartGame().Forget()).AddTo(gameObject);
    }

    private async UniTaskVoid StartGame()
    {
        StringReader reader = await titleModel.StartGame();
        if (reader == null)
        {
            Debug.Log("�X�v���b�h�V�[�g�̏��擾�Ɏ��s���܂���");
            return;
        }

        spreadSheetData.reader = reader;

        // �^�C�g����ʃt�F�[�h�A�E�g
        titleView.TransitionGameScreen();

        director.ChangePresenter(director.gamePresenter);
    }

    public void Dispose()
    {
        
    }
}
