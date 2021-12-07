using UnityEngine;
using UnityEngine.UI;


namespace VS.CW2RTS.UI
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] Slider volumeSlider;
        // Start is called before the first frame update
        void Start()
        {
            if (!PlayerPrefs.HasKey("musicVolume"))
            {
                PlayerPrefs.SetFloat("musicVolume", 0.3f);
                Load();
            }
            else
            {
                Load();
            }
        }

        // Update is called once per frame
        public void ChangeVolume()
        {
            AudioListener.volume = volumeSlider.value;
            Save();
        }

        private void Load()
        {
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }

        private void Save()
        {
            PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        }
    }

}
