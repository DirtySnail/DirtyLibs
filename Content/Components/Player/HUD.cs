using DirtySnail.Components;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DirtySnail.NinjaWarrior
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private LevelProgression _levelProgression;
        [SerializeField] private TMP_Text _levelText;

        private void Start()
        {
            _levelText.text = $"Level : {_levelProgression.GetPlayerProgressionLevel()}";
        }
    }
}

