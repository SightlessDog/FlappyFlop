using System;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private Slider speedSlider;
        public void AdjustGameSpeed()
        {
            GameManager.Instance.SetGameSpeed(speedSlider.value);
        }
    }
}