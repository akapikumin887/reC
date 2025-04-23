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
            Debug.Log("スプレッドシートの情報取得に失敗しました");
            return;
        }

        spreadSheetData.reader = reader;

        // タイトル画面フェードアウト
        titleView.TransitionGameScreen();

        director.ChangePresenter(director.gamePresenter);
    }

    public void Dispose()
    {
        
    }
}
