using UnityEngine;

namespace Obvious.Soap.Example
{
    public class PlayersNotifier : MonoBehaviour
    {
        [SerializeField] private ScriptableListPlayer scriptableListPlayer = null;
        [SerializeField] private IterationType _iterationType = IterationType.Foreach;

        private float _delay = 1.5f;
        private float _timer = 0f;

        private enum IterationType
        {
            Foreach,
            Indexer
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _delay)
            {
                NotifyPlayers();
                _timer = 0;
            }
        }

        private void NotifyPlayers()
        {
            if (_iterationType == IterationType.Foreach)
            {
                //IEnumerable quality of life
                foreach (var player in scriptableListPlayer)
                    player.Notify();
            }
            else
            {
                //iterate backwards, as the number of items of the list can change at runtime
                //Index quality of life
                for (int i = scriptableListPlayer.Count - 1; i >= 0; i--)
                    scriptableListPlayer[i].Notify();
            }
        }
    }
}