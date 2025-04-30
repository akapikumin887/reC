using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ResultView : MonoBehaviour, IView
{
    [SerializeField]
    private GameObject scene;

    [SerializeField]
    private Button retryButton;
    public Button _RetryButton => retryButton;

    [SerializeField]
    private Button titleButton;
    public Button _TitleButton => titleButton;

    public void Initialize()
    {
        gameObject.SetActive(true);
    }

    public void TransitionSceneToTitle()
    {
        scene.transform.DOLocalMove(new Vector2(0.0f, 0.0f), 0.5f).OnComplete(() => gameObject.SetActive(false));
    }

    public void TransitionSceneToGame()
    {
        scene.transform.DOLocalMove(new Vector2(-110.0f, 0.0f), 0.5f).OnComplete(() => gameObject.SetActive(false));
    }

    private void OnDestroy()
    {
        
    }
}
