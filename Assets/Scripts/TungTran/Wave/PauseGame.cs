using TungTran.Player.Player;
using UnityEngine;

namespace TungTran.Wave
{
    public class PauseGame : MonoBehaviour
    {
        public void Pause()
        {
            Time.timeScale = 1f;
        }
    }
}
