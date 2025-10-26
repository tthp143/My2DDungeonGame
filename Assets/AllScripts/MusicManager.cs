using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    
    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName;
        public AudioClip musicClip;
    }
    
    [SerializeField] private SceneMusic[] sceneMusicList;
    private AudioSource audioSource;
    
    void Awake()
    {
        // Singleton Pattern - มีได้แค่ตัวเดียว
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        // ฟังเมื่อมีการเปลี่ยน Scene
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // หาเพลงที่ตรงกับ Scene
        foreach (SceneMusic sceneMusic in sceneMusicList)
        {
            if (sceneMusic.sceneName == scene.name)
            {
                ChangeMusic(sceneMusic.musicClip);
                return;
            }
        }
    }
    
    void ChangeMusic(AudioClip newClip)
    {
        if (audioSource.clip == newClip) return; // ถ้าเป็นเพลงเดิมไม่ต้องเปลี่ยน
        
        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();
    }
    
    // ฟังก์ชันเพิ่มเติม
    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
    }
    
    public void PauseMusic()
    {
        audioSource.Pause();
    }
    
    public void ResumeMusic()
    {
        audioSource.UnPause();
    }
    
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}