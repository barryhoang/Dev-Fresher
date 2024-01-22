using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class KeyboardInputToButton : MonoBehaviour
    {
        public Button yourButton;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TriggerButtonClick();
            }
        }

        private void TriggerButtonClick()
        {
            if (yourButton != null)
            {
                yourButton.onClick.Invoke();
            }
            else
            {
                Debug.LogError("Button reference not set in the inspector!");
            }
        }
    }
}
