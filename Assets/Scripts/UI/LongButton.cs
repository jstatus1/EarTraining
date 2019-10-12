using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.IndividualButton
{
    public class LongButton:MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool pointerDown;
        private float pointerDownTimer;
        
        [SerializeField]
        private float requireHoldTime;
        public UnityEvent onLongClick;

        public void OnPointerDown(PointerEventData eventData)
        {
            pointerDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Reset();
        }

        void Update()
        {
            if(pointerDown)
            {
                pointerDownTimer += Time.deltaTime;
                if(pointerDownTimer >= requireHoldTime)
                {
                    if(onLongClick != null)
                    onLongClick.Invoke();

                    Reset();
                }
            }
        }

        void Reset()
        {
            pointerDown = false;
            pointerDownTimer = 0;
        }

    }
}