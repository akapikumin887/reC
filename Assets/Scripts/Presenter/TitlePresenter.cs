using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public class TitlePresenter : MonoBehaviour, IPresenter
{
    [SerializeField]
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

        titleView._StartButton.OnClickAsObservable().Subscribe(_ => StartGame().Forget()).AddTo(gameObject);
    }

    private async UniTaskVoid StartGame()
    {
        spreadSheetData.quizTemplates = await titleModel.LoadSpreadsheetDataAsync();
        if (spreadSheetData.quizTemplates == null)
        {
            Debug.Log("スプレッドシートの情報取得に失敗しました");
            return;
        }

        // タイトル画面フェードアウト
        titleView.TransitionScene(false);

        director.ChangePresenter(director._GamePresenter);
    }

    public void Dispose()
    {
        
    }
}
