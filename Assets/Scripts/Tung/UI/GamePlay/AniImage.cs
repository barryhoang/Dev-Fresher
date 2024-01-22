
using System.Collections.Generic;
using MEC;
using UnityEngine;
using UnityEngine.UI;

namespace Tung
{
    public class AniImage : MonoBehaviour
    {
        [SerializeField] private Image myImage;
        public static float duration = 1f; // Thời gian giảm từ 1 đến 0 (1 giây)

        void Start()
        {

        }

        public IEnumerator<float> DecreaseFillAmount()
        {
            float startTime = Time.time; // Lấy thời gian bắt đầu
            while (Time.time - startTime <= duration)
            {
                float elapsedTime = Time.time - startTime;
                float fillAmount = 1 - elapsedTime / duration; // Tính toán giá trị FillAmount

                myImage.fillAmount = fillAmount;
                yield return 0;
            }

            myImage.fillAmount = 0; // Đảm bảo giá trị FillAmount là 0 khi kết thúc
        }
        public IEnumerator<float> IncreaseFillAmount()
        {
            float startTime = Time.time; // Lấy thời gian bắt đầu

            while (Time.time - startTime <= duration)
            {
                float elapsedTime = Time.time - startTime;
                float fillAmount = elapsedTime / duration; // Tính toán giá trị FillAmount

                myImage.fillAmount = fillAmount;
                yield return 0; // Wait for the next frame
            }

            myImage.fillAmount = 1; // Đảm bảo giá trị FillAmount là 1 khi kết thúc
        }
    }
}
