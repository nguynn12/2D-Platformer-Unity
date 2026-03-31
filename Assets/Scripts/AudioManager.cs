using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Tạo Singleton để các script khác có thể gọi AudioManager.instance
    public static AudioManager instance;

    [Header("--- Audio Sources ---")]
    public AudioSource bgmSource; // Nguồn phát nhạc nền
    public AudioSource sfxSource; // Nguồn phát hiệu ứng

    [Header("--- Audio Clips ---")]
    public AudioClip backgroundMusic;
    public AudioClip coinSound;
    public AudioClip gemSound;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip winSound;

    private void Awake()
    {
        // Thiết lập Singleton và giữ cho AudioManager không bị hủy khi chuyển màn chơi
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Bắt đầu phát nhạc nền ngay khi vào game
        if (backgroundMusic != null)
        {
            bgmSource.clip = backgroundMusic;
            bgmSource.Play();
        }
    }

    // Hàm public này dùng để các Script khác gọi khi cần phát âm thanh
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            // PlayOneShot giúp phát đè nhiều âm thanh cùng lúc (ví dụ ăn 2 đồng xu liên tiếp)
            sfxSource.PlayOneShot(clip);
        }
    }
}