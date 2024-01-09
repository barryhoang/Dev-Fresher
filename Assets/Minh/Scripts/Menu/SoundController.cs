using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minh
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private AudioSource _vfxSound;
        private void Start()
        {
            if (!PlayerPrefs.HasKey("VFX Volume"))
            {
                PlayerPrefs.SetFloat("VFX Volume",1);
            }
            else
            {
                Load();
            }
        }

        public void ChangeVolume()
        {
            _vfxSound.volume = _volumeSlider.value;
            PlayerPrefs.SetFloat("VFX Volume",_volumeSlider.value);
        }

        private void Load()
        {
            _volumeSlider.value = PlayerPrefs.GetFloat("VFX Volume");
        }
    }
}

