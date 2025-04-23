using System;
using UnityEngine;
using UnityEngine.UI;

public class TitleView : MonoBehaviour, IView
{
    [field: SerializeField]
    public Button startButton {  get; private set; }

    public void Initialize()
    {
        gameObject.SetActive(true);
    }

    public void TransitionGameScreen()
    {
        // �J�ڂ������Active�̐؂�ւ����������A���Ɉړ����ď�������
        gameObject.SetActive(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        // ����͑Ή�����
        if (disposing)
        {

        }
    }
}
