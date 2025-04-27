using UnityEngine;
using UnityEngine.UI;

public class ResultView : MonoBehaviour, IView
{
    [SerializeField]
    private Button retryButton;
    public Button _RetryButton => retryButton;

    [SerializeField]
    private Button titleButton;
    public Button _TitleButton => titleButton;

    public void Initialize()
    {
        TransitionScene(true);
    }

    public void TransitionScene(bool flag)
    {
        gameObject.SetActive(flag);
    }

    private void OnDestroy()
    {
        
    }
}
