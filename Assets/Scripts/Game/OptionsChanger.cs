using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsChanger : MonoBehaviour
{
    private AudioSource bgMusic;

    private AudioSource sfx;

    [SerializeField]
    private Slider master;

    [SerializeField]
    private Slider bgmSlider;

    [SerializeField]
    private Slider sfxSlider;

    private bool init = false;

    public void ChangeMaster()
    {
        if(init == false)
        {
            bgMusic.volume = master.value * bgmSlider.value;
            if(sfx != null && sfxSlider != null)
            {
                sfx.volume = master.value * sfxSlider.value;
                PlayerPrefs.SetFloat("SFX", sfx.volume);
            }           
            PlayerPrefs.SetFloat("BGM", bgMusic.volume);         
            PlayerPrefs.SetFloat("Master", master.value);
        }
        
    }

    public void ChangeBGM()
    {
        if(init == false)
        {
            bgMusic.volume = (bgmSlider.value * master.value);
            PlayerPrefs.SetFloat("BGM", bgMusic.volume);
            PlayerPrefs.SetFloat("BGMSlider", bgmSlider.value);
        }
    }

    public void ChangeSFX()
    {
        if(init == false)
        {
            sfx.volume = (sfxSlider.value * master.value);
            PlayerPrefs.SetFloat("SFX", sfx.volume);
            PlayerPrefs.SetFloat("SFXSlider", sfxSlider.value);
        }
    }

    private void Start()
    {
        bgMusic = BGMusic.instance.gameObject.GetComponent<AudioSource>();
        sfx = SFXMusic.instance.gameObject.GetComponent<AudioSource>();

        init = true;
        float mas = PlayerPrefs.GetFloat("Master", 1);
        master.value = mas;
        float sli1 = PlayerPrefs.GetFloat("BGMSlider", 1);
        bgmSlider.value = sli1;
        if(sfxSlider != null)
        {
            float sli2 = PlayerPrefs.GetFloat("SFXSlider", 1);
            sfxSlider.value = sli2;
        }
        float bgm = PlayerPrefs.GetFloat("BGM", 1);
        bgMusic.volume = bgm;
        if(sfx != null)
        {
            float sf = PlayerPrefs.GetFloat("SFX", 1);
            sfx.volume = sf;
        }
        init = false;
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
