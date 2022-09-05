using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SFXMusic : MonoBehaviour
{
    #region Singleton
    public static SFXMusic instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance");
            return;
        }
        instance = this;
        LoadAssets();
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        foreach(string s in names)
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("Game"))
        {
            PlayStartUp();
        }
    }

    public void PlayHurt()
    {
        for(int i = 0; i < audios.Count; i++)
        {
            if(audios[i].name.Contains("player_hit"))
            {
                source.PlayOneShot(audios[i]);
                return;
            }
        }
    }

    public void PlayShoot()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].name.Contains("shoot"))
            {
                source.PlayOneShot(audios[i]);
                return;
            }
        }
    }

    public void PlayPlayerDeath()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].name.Contains("player_death"))
            {
                source.PlayOneShot(audios[i]);
                return;
            }
        }
    }

    public void PlayEnemyDeath()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].name.Contains("enemy_death"))
            {
                source.PlayOneShot(audios[i]);
                return;
            }
        }
    }

    public void PlaySelect()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].name.Contains("select"))
            {
                source.PlayOneShot(audios[i]);
                return;
            }
        }
    }

    public void PlayPick()
    {
        /*for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].name.Contains("pick_button"))
            {
                source.PlayOneShot(audios[i]);
                return;
            }
        }*/
    }

    public void PlayBulletHit()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].name.Contains("bullet_hit"))
            {
                source.PlayOneShot(audios[i]);
                return;
            }
        }
    }

    /*public void Play()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].name.Contains("Enemy_Death"))
            {
                source.PlayOneShot(audios[i]);
                return;
            }
        }
    }*/

    public void PlayMenuSelect()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].name.Contains("menu_select"))
            {
                source.PlayOneShot(audios[i]);
                return;
            }
        }
    }

    public void PlayExplosion()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].name.Contains("explosion"))
            {
                source.PlayOneShot(audios[i]);
                return;
            }
        }
    }

    public void PlayStartUp()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].name.Contains("start_up"))
            {
                source.PlayOneShot(audios[i]);
                return;
            }
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
