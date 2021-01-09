using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> MusicClips;
    [SerializeField]
    private List<AudioClip> Sounds;
    public AudioMixer mixer;
    public AudioSource MusicSource;
    public AudioMixerGroup SFXGroup;
    public AudioMixerSnapshot[] normal, musicOff;
    public void PlaySound(string name)
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach (AudioSource AS in audioSources)
        {
            if (!AS.isPlaying)
            {
                AS.clip = Sounds.Find(x => x.name == name);
                AS.Play();
                return;
            }
        }
        AudioSource As = gameObject.AddComponent<AudioSource>();
        As.outputAudioMixerGroup = SFXGroup;
        As.clip = Sounds.Find(x => x.name == name);
        As.Play();
    }
    public void PlayMusic(string name)
    {
        if (MusicSource.isPlaying)
        {
            MusicSource.clip = MusicClips.Find(x => x.name == name);
            StartCoroutine(VolumeTransition(true, true));
            MusicSource.PlayDelayed(1f);
        }
        else
        {
            StartCoroutine(VolumeTransition(false, true));
            MusicSource.clip = MusicClips.Find(x => x.name == name);
            MusicSource.Play();
        }
    }
    private IEnumerator VolumeTransition(bool transitionDown, bool transitionUp, float transitionTime = 2f)
    {
        if (transitionDown && transitionUp)
        {
            mixer.TransitionToSnapshots(musicOff, new float[] { 1 }, transitionTime / 2);
            yield return new WaitForSeconds(transitionTime / 2);
            mixer.TransitionToSnapshots(normal, new float[] { 1 }, transitionTime / 2);
            yield break;
        }

        if (transitionDown)
        {
            mixer.TransitionToSnapshots(musicOff, new float[] { 1 }, transitionTime / 2);
        }
        if (transitionUp)
        {
            mixer.TransitionToSnapshots(musicOff, new float[] { 1 }, 0);
            mixer.TransitionToSnapshots(normal, new float[] { 1 }, transitionTime / 2);
        }
        yield break;
    }

}
