using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStatePanel : StatePanel
{
    private WinStateParams _currentParams;

    public override void Activate(StatePanelParamsBase additionalParams = null)
    {
        _currentParams = (WinStateParams) additionalParams;

        _content.SetActive(true);
    }

    public void ContinueButtonClicked()
    {
        _currentParams?.OnContinueButtonClicked?.Invoke();
    }

    public void RestartButtonClicked()
    {
        _currentParams?.OnRestartButtonClicked?.Invoke();
    }
}

public class WinStateParams : StatePanelParamsBase
{
    public System.Action OnRestartButtonClicked;
    public System.Action OnContinueButtonClicked;
}
