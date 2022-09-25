using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailStatePanel : StatePanel
{
    private FailStateParams _currentParams;

    public override void Activate(StatePanelParamsBase additionalParams = null)
    {
        _currentParams = (FailStateParams) additionalParams;

        _content.SetActive(true);
    }

    public void RestartButtonClicked()
    {
        if (_currentParams == null)
            return;

        _currentParams.OnRestartButtonClicked?.Invoke();
    }
}

public class FailStateParams : StatePanelParamsBase
{
    public System.Action OnRestartButtonClicked;
}
