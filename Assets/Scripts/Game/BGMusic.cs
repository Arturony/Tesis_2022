using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class BGMusic : MonoBehaviour
{
    

    #region Singleton
    public static BGMusic instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance");
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
        LoadAssets();
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private List<string> names;

    [SerializeField]
    private List<AudioClip> audios;

    private void LoadAssets()
    {
        foreach (string s in names)
        {
            LoadedAssets(s);
        }
    }

    void LoadedAssets(string addressName)
    {
        Addressables.LoadAssetAsync<AudioClip>(addressName).Completed += LoadDone;
    }

    void LoadDone(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<AudioClip> obj)
    {
        audios.Add(obj.Result);
        Debug.Log("finish load asset " + obj.Result.name);
    }

    public void ChangePitch(float picth)
    {
        source.pitch = picth;
    }

    private IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(1f);
        PlayMusic();
    }

    private IEnumerator WaitSecondsMenu()
    {
        yield return new WaitForSeconds(1f);
        PlayMusicMenu();
    }

    public void PlayMusic()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].name.ToLower().Contains("background"))
            {
                source.clip = audios[i];
                source.Play();
                return;
            }
        }
    }

    public void PlayMusicMenu()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].name.ToLower().Contains("menu"))
            {
                source.clip = audios[i];
                source.Play();
                return;
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        source.Stop();

        if (scene.name.Equals("Game"))
        {
            StartCoroutine(WaitSeconds());
        }
        else if(scene.name.Equals("MainMenu"))
        {
            StartCoroutine(WaitSecondsMenu());
        }
    }


    /*public void PlayTank()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].name.Contains("Line of Fire"))
            {
                source.clip = audios[i];
                source.Play();
                return;
            }
        }
    }

    public void PlayTouhou()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].name.Contains("Unknown Assault"))
            {
                source.clip = audios[i];
                source.Play();
                return;
            }
        }
    }

    public void PlayPlane()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].name.Contains("Line of Fire"))
            {
                source.clip = audios[i];
                source.Play();
                return;
            }
        }
    }

    public void PlayCrab()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].name.Contains("One by One"))
            {
                source.clip = audios[i];
                source.Play();
                return;
            }
        }
    }

    public void PlayBackground()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].name.Contains("Fortress"))
            {
                source.clip = audios[i];
                source.Play();
                return;
            }
        }
    }

    public void PlayNpc()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].name.Contains("Wild Wind"))
            {
                source.clip = audios[i];
                source.Play();
                return;
            }
        }
    }*/

    public void Stop()
    {
        source.Stop();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
