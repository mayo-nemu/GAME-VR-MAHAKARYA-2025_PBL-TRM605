using System.Collections.Generic;
using System.Linq;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HurricaneVR.Framework.Core.UI
{
    public class Quest2Button : MonoBehaviour
    {

        [Tooltip("Button used to toggle presses.")]
        public HVRButtons PressButton;

        public HVRHandSide HandSide;

        private Button uiButton;

        void Start()
        {
            // Mendapatkan komponen Button dari GameObject yang sama
            uiButton = GetComponent<Button>();
        }

        void Update()
        {
            var buttonState = HVRController.GetButtonState(HandSide, PressButton);
            // Jika tombol yang ditentukan ditekan
            if (buttonState.JustActivated)
            {
                // Memicu event onClick dari UI Button
                uiButton.onClick.Invoke();
            }
        }
    }
}