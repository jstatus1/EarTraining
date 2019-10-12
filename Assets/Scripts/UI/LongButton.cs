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

        public UnityEvent onSingleClick;

        [SerializeField] Image Image_Fill;

        void Start()
        {
            onLongClick = new UnityEvent();
        }

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
                Debug.Log(pointerDownTimer);
                if(pointerDownTimer >= requireHoldTime)
                {
                    if(onLongClick != null)
                        onLongClick.Invoke();

                    Reset();
                }
                //Image_Fill.fillAmount = pointerDownTimer / requireHoldTime;
            }
        }

        void Reset()
        {
            pointerDown = false;
            pointerDownTimer = 0;
            //Image_Fill.fillAmount = pointerDownTimer / requireHoldTime;
        }

    }
}