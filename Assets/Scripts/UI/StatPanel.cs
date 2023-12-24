using Entity;
using Obvious.Soap;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StatPanel : MonoBehaviour
    {
        [SerializeField] private BindTextMeshPro _healthText;
        [SerializeField] private BindTextMeshPro _damageText;
        [SerializeField] private BindTextMeshPro _attackText;

        public void SetText(EntityData entityData)
        {
            _healthText.Prefix = "Health: ";
        }
    }
}
