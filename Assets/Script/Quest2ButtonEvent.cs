using System.Collections.Generic;
using System.Linq;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HurricaneVR.Framework.Core.UI
{
    public class Quest2ButtonEvent : MonoBehaviour
    {

        [Tooltip("Button used to toggle presses.")]
        public HVRButtons PressButton;

        public HVRHandSide HandSide;

        public UnityEngine.Events.UnityEvent OnClick;


        void Update()
        {
            var buttonState = HVRController.GetButtonState(HandSide, PressButton);
            if (buttonState.JustActivated)
            {
                OnClick.Invoke();
            }
        }
    }
}