using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();

                if (instance == null)
                {
                    GameObject go = new GameObject("AudioManager");
                    instance = go.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }
   

    private List<AudioSource> audioSources;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Inicializar la lista de AudioSources
            audioSources = new List<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] AudioClip drumsClip;
    [SerializeField] AudioClip clickNeutral;
    [SerializeField] AudioClip clickON;
    [SerializeField] AudioClip clickOFF;

     AudioSource drumsSource;
     AudioSource clickNeutralSource;
     AudioSource clickONSource;
     AudioSource clickOFFSource;

    private void Start()
    {
        drumsSource = gameObject.AddComponent<AudioSource>();
        drumsSource.clip = drumsClip;

        clickNeutralSource = gameObject.AddComponent<AudioSource>();
        clickNeutralSource.clip = clickNeutral;

        clickONSource = gameObject.AddComponent<AudioSource>();
        clickONSource.clip = clickON;

        drumsSource = gameObject.AddComponent<AudioSource>();
        drumsSource.clip = drumsClip;
    }


    public void PlayDrums()
    {
        
        drumsSource.Play();
        //PlayWithFadeIn(drumsClip, 0.1f);
    }
    public void PlayClickNeutral()
    {
        clickNeutralSource.Play();
        //PlayWithFadeIn(clickNeutral, 0.1f);

    }
    public void PlayClickON()
    {
        
        clickONSource.Play();
        //PlayWithFadeIn(clickON, 0.1f);

    }
    public void PlayClickOFF()
    {
        AudioSource clickOFFSource = gameObject.AddComponent<AudioSource>();
        clickOFFSource.clip = clickOFF;
        clickOFFSource.Play();
        //PlayWithFadeIn(clickOFF, 0.1f);

    }


    // Método para reproducir un clip con fade in
    //public void PlayWithFadeIn(AudioClip clip, float fadeInDuration)
    //{
    //    AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
    //    audioSources.Add(newAudioSource);
    //    Debug.Log(audioSources.Count);
    //    StartCoroutine(FadeIn(newAudioSource, clip, fadeInDuration));
    //    StartCoroutine(DeleteFinishedTrack(newAudioSource));

    //}

    //// Método para detener la reproducción con fade out
    //public void StopWithFadeOut(float fadeOutDuration)
    //{
    //    foreach (AudioSource audioSource in audioSources)
    //    {
    //        StartCoroutine(FadeOut(audioSource, fadeOutDuration));
    //    }
    //}


    //private IEnumerator DeleteFinishedTrack(AudioSource audioClip)
    //{
    //    yield return new WaitForSeconds(audioClip.clip.length);
    //    audioSources.Remove(audioClip);
    //}
    //private System.Collections.IEnumerator FadeIn(AudioSource audioSource, AudioClip clip, float fadeInDuration)
    //{
    //    float currentTime = 0f;
    //    audioSource.clip = clip;
    //    audioSource.Play();

    //    while (currentTime < fadeInDuration)
    //    {
    //        float normalizedTime = currentTime / fadeInDuration;
    //        audioSource.volume = Mathf.Lerp(0f, 1f, normalizedTime);
    //        currentTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    audioSource.volume = 1f;
    //}

    //private System.Collections.IEnumerator FadeOut(AudioSource audioSource, float fadeOutDuration)
    //{
    //    float currentTime = 0f;

    //    while (currentTime < fadeOutDuration)
    //    {
    //        float normalizedTime = currentTime / fadeOutDuration;
    //        audioSource.volume = Mathf.Lerp(1f, 0f, normalizedTime);
    //        currentTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    audioSource.Stop();
    //    audioSources.Remove(audioSource);
    //    Destroy(audioSource);
    //}
}
