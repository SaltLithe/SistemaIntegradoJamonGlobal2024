using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Audio;
using System.Threading.Tasks;

public enum Character{MrK, Cass, Delilah}


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
            audioSources = new List<AudioSource>
            {
                drumsSource,
                clickNeutralSource,
                clickONSource,
                clickOFFSource,
                redButtonSource,
                voicesSource,
                musicSource,
                laughSource,
                jeerSource,
                whisperSource
            };
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
    [SerializeField] AudioClip redButton;

    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip endingMusic;

    [SerializeField] AudioClip laugh1, laugh2, laugh3;

    [SerializeField] AudioClip whispers;
    [SerializeField] AudioClip abucheo;

    [SerializeField] AudioMixerGroup SFXGroup, MusicGroup, VoicesGroup;

    int mrKCount = 0;
    [SerializeField] List<AudioClip> mrKVoices;
    [SerializeField] List<AudioClip> delilahVoices;
    [SerializeField] List<AudioClip> cassVoices;
    

    AudioSource drumsSource;
    AudioSource clickNeutralSource;
    AudioSource clickONSource;
    AudioSource clickOFFSource;
    AudioSource redButtonSource;
    AudioSource voicesSource;
    AudioSource musicSource;
    AudioSource laughSource;
    AudioSource jeerSource;
    AudioSource whisperSource;


    private void Start()
    {
        drumsSource = gameObject.AddComponent<AudioSource>();
        drumsSource.clip = drumsClip;
        drumsSource.outputAudioMixerGroup = SFXGroup;

        clickNeutralSource = gameObject.AddComponent<AudioSource>();
        clickNeutralSource.clip = clickNeutral;
        clickNeutralSource.outputAudioMixerGroup = SFXGroup;

        clickONSource = gameObject.AddComponent<AudioSource>();
        clickONSource.clip = clickON;
        clickONSource.outputAudioMixerGroup = SFXGroup;

        clickOFFSource = gameObject.AddComponent<AudioSource>();
        clickOFFSource.clip = clickOFF;
        clickOFFSource.outputAudioMixerGroup = SFXGroup;

        redButtonSource = gameObject.AddComponent<AudioSource>();
        redButtonSource.clip = redButton;
        redButtonSource.outputAudioMixerGroup = SFXGroup;

        drumsSource = gameObject.AddComponent<AudioSource>();
        drumsSource.clip = drumsClip;
        drumsSource.outputAudioMixerGroup = SFXGroup;

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = menuMusic;
        musicSource.outputAudioMixerGroup = MusicGroup;
        musicSource.loop = true;

        voicesSource = gameObject.AddComponent<AudioSource>();
        voicesSource.clip = mrKVoices[0];
        voicesSource.outputAudioMixerGroup = VoicesGroup;

        laughSource = gameObject.AddComponent<AudioSource>();
        laughSource.outputAudioMixerGroup = SFXGroup;

        jeerSource = gameObject.AddComponent<AudioSource>();
        jeerSource.clip = abucheo;
        jeerSource.outputAudioMixerGroup = SFXGroup;

        whisperSource = gameObject.AddComponent<AudioSource>();
        whisperSource.clip = whispers;
        whisperSource.outputAudioMixerGroup= SFXGroup;
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

    public void PlayVoice(Character character) {
        switch (character)
        {
            case Character.MrK:
                if (mrKCount <= mrKVoices.Count)
                    voicesSource.clip = mrKVoices[mrKCount++];
                else
                    Debug.Log("No quedan dialogos para mrK / mrKVoices se pasa de rango");
                break;
            case Character.Cass:
                int randomCassVoice = Random.Range(0, cassVoices.Count);
                voicesSource.clip = cassVoices[randomCassVoice];
                break;
            case Character.Delilah: 
                int randomDelVoice = Random.Range(0, delilahVoices.Count);
                voicesSource.clip = delilahVoices[randomDelVoice];
                break;
            default:
                voicesSource.clip = null;
                Debug.Log("Personaje no reconocido");
                break;
            
        }
        
        if(voicesSource && voicesSource.clip)
        {
            voicesSource.Play();
        }
        else
        {
            Debug.Log("Algo pasa con las voces");
        }
            
    }
    public void StopVoice() {
        if(voicesSource && voicesSource.clip)voicesSource.Stop();
    }

    public void StartMonologueMusic()
    {
        musicSource.Stop();
        musicSource.clip = gameMusic;
        musicSource.Play();
    }

    public async void StartMenuMusic()
    {
        while(musicSource == null)
        {
            await Task.Delay(1000);
        }

        musicSource.Stop();
        musicSource.clip = menuMusic;
        musicSource.Play();
    }

    public void StartEndingMusic()
    {
        musicSource.Stop();
        musicSource.clip = endingMusic;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayLaugh(int laugh)
    {
        switch (laugh)
        {
            case 1:
                laughSource.clip = laugh1;
                break;
            case 2:
                laughSource.clip = laugh2;
                break;
            case 3:
                laughSource.clip = laugh3;
                break;
        }
        laughSource.Play();
    }

    public void PlayJeer()
    {
        jeerSource.Play();
    }

    public void PlayWhispers()
    {
        whisperSource.Play();
    }

    public void StopWhispers()
    {
        whisperSource.Stop();
    }

    public void StopAll() 
    { 
        foreach (AudioSource aSource in audioSources)
        {
            aSource.Stop();
        }
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
