using UniRx;
using UnityEngine;

public class ResultPresenter : MonoBehaviour, IPresenter
{
    [SerializeField]
    private SpreadSheetData spreadSheetData;

    [SerializeField]
    private Director director;

    [SerializeField]
    private ResultView resultView;
    private ResultModel resultModel;

    public void Initialize(Director d)
    {
        resultModel = new();
        resultModel.Initialize();
        resultView.Initialize();

        resultView._RetryButton.OnClickAsObservable().Subscribe(_ => {
            resultModel.ResetImages(spreadSheetData.quizTemplates);
            resultView.TransitionScene(false);
            director.ChangePresenter(director._GamePresenter);
        }).AddTo(gameObject);

        resultView._TitleButton.OnClickAsObservable().Subscribe(_ => {
            resultView.TransitionScene(false);
            director.ChangePresenter(director._TitlePresenter);
        }).AddTo(gameObject);
    }

    public void Dispose()
    {

    }
}
