using DG.Tweening;
using TMPro;
using UnityEngine;

public class InfoPopupText : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private FloatRange _yPositionChangeRange;
    [SerializeField] private FloatRange _xPositionChangeRange;
    [SerializeField] private FloatRange _dissappearRange;

    public void SetTitle(string title)
    {
        _titleText.text = title;

        float animationDuration = _dissappearRange.GetRandomValue();

        _titleText.CrossFadeAlpha(0f, animationDuration, false);
        transform.DOMove(new Vector3(transform.position.x + _xPositionChangeRange.GetRandomValue(), transform.position.y + _yPositionChangeRange.GetRandomValue(), transform.position.z), animationDuration).OnComplete(()=> 
        {
            transform.DOKill();
            Destroy(gameObject);    
        });
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
