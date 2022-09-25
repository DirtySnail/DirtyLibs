using CMF;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBase : CharacterInput
{
    public bool ActionsBlocked;

    [Header("Base UI")]
    [SerializeField] private TMP_Text _levelCountText;

    public int Level { get; protected set; }

    public override float GetHorizontalMovementInput()
    {
        return 0f;
    }

    public override float GetVerticalMovementInput()
    {
        return 0f;
    }

    public override bool IsJumpKeyPressed()
    {
        return false;
    }

    public virtual void SetLevel(int value)
    {
        int difference = value - Level;
        Level = value;
        UpdateLevel();
    }

    public virtual void UpdateLevel()
    {
        if(_levelCountText != null)
        {
            _levelCountText.text = $"{Level}";
        }
    }

    public virtual void SetWin()
    {

    }

    public virtual void Warmup()
    {

    }

    public virtual void StartGameplay()
    {
        
    }

    public virtual void SetFail()
    {

    }
}
