using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private string currentSong;
    private float time;
    private int sceneIndex;
    public GameObject AudioSourceParent;
    public static AudioManager instance;
    [SerializeField]
    private List<AudioClip> MusicClips;
    [SerializeField]
    private List<AudioClip> Sounds;
    public AudioMixer mixer;
    public AudioSource MusicSource;
    public AudioMixerGroup SFXGroup;
    public AudioMixerSnapshot deathSs;
    public AudioMixerSnapshot[] normal, musicOff;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(!InstanceCheck()) return;
        instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void SaveSceneData()
    {
        currentSong = MusicSource.clip.name;
        time = MusicSource.time;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void SubscriveToEvents()
    {
        PlayerStateManager.OnDeath += SaveSceneData;
        PlayerStateManager.OnDeath += TransitionToDeath;

    }
    public void UnubscriveToEvents()
    {
        PlayerStateManager.OnDeath -= SaveSceneData;
        PlayerStateManager.OnDeath -= TransitionToDeath;

    }
    private void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        SubscriveToEvents();

        AudioSourceParent = FindObjectOfType<AudioListener>().gameObject;
        MusicSource = GameObject.Find("MusicSourceObject").GetComponent<AudioSource>();
        if(s.buildIndex == sceneIndex)
        {
            normal[0].TransitionTo(0.01f);
            MusicSource.clip = MusicClips.Find(x => x.name == currentSong);
            MusicSource.time = time;
            MusicSource.Play();
        }
        else if (s.buildIndex == 1)
        {
            PlayMusic("PlatformerStageMusic");
        }
    }

    public void TransitionToDeath()
    {
        deathSs.TransitionTo(0.01f);
    }

    private bool InstanceCheck()
    {
        if(instance != this && instance != null)
        {
            Destroy(this.gameObject);
            return false;
        }
        return true;
    }
    public void PlaySound(string name)
    {
        AudioSource[] audioSources = AudioSourceParent.GetComponents<AudioSource>();
        foreach (AudioSource AS in audioSources)
        {
            if (!AS.isPlaying)
            {
                AS.clip = Sounds.Find(x => x.name == name);
                AS.Play();
                return;
            }
        }
        AudioSource As = AudioSourceParent.AddComponent<AudioSource>();
        As.outputAudioMixerGroup = SFXGroup;
        As.clip = Sounds.Find(x => x.name == name);
        As.Play();
    }
    public void PlayMusic(string name)
    {
        if (MusicSource.isPlaying)
        {
            StartCoroutine(VolumeTransition(true, true, MusicClips.Find(x => x.name == name)));
        }
        else
        {
            StartCoroutine(VolumeTransition(false, true));
            MusicSource.clip = MusicClips.Find(x => x.name == name);
            MusicSource.Play();
        }
    }
    private IEnumerator VolumeTransition(bool transitionDown, bool transitionUp, AudioClip clip = null, float transitionTime = 1f)
    {
        if (transitionDown && transitionUp)
        {
            mixer.TransitionToSnapshots(musicOff, new float[] { 1 }, transitionTime / 2);
            yield return new WaitForSeconds(transitionTime / 2);
            MusicSource.clip = clip;
            MusicSource.Play();
            mixer.TransitionToSnapshots(normal, new float[] { 1 }, transitionTime / 2);
            yield break;
        }

        if (transitionDown)
        {
            mixer.TransitionToSnapshots(musicOff, new float[] { 1 }, transitionTime / 2);
        }
        if (transitionUp)
        {
            mixer.TransitionToSnapshots(musicOff, new float[] { 1 }, 0.01f);
            yield return new WaitForSeconds(0.01f);
            mixer.TransitionToSnapshots(normal, new float[] { 1 }, transitionTime / 2);
        }
        yield break;
    }

}
