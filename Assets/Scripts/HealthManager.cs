using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public GameObject damageEffect;

    private int MaxHealth = 3;
    public int currentHealth;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite FullHeartSprite;
    [SerializeField] private Sprite HalfHeartSprite;
    [SerializeField] private Sprite EmptyHeartSprite;

    private GameObject Player;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        currentHealth = MaxHealth; // Bơm đầy máu lúc mới vào game
        DisplayHearts();
    }

    // Hàm gốc của tác giả, ta thêm logic Game Over/Respawn vào đây
    public void HurtPlayer()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            DisplayHearts();

            // Phát âm thanh mất máu
            if (AudioManager.instance != null) 
                AudioManager.instance.PlaySFX(AudioManager.instance.deathSound);

            // LỚP GIÁP BẢO VỆ: Chỉ sinh hiệu ứng nếu có file hiệu ứng được kéo thả vào
            // Nếu quên kéo thả, game sẽ bỏ qua dòng này và chạy tiếp xuống dưới, không bị crash!
            if (damageEffect != null)
            {
                Instantiate(damageEffect, Player.transform.position, Quaternion.identity);
            }

            // KIỂM TRA MẠNG BÂY GIỜ SẼ CHẠY MƯỢT MÀ
            if (currentHealth <= 0)
            {
                // HẾT SẠCH MÁU -> Gọi màn hình Game Over
                GameManager.instance.GameOver(); 
            }
            else
            {
                // CÒN MÁU -> Chỉ hồi sinh nhân vật về vị trí cũ
                GameManager.instance.RespawnPlayer();
            }
        }
    }

    // Hàm này giữ nguyên 100% của tác giả để vẽ nửa trái tim
    public void DisplayHearts()
    {
        int fullHeartsCount = currentHealth / 2; 
        bool hasHalfHeart = (currentHealth % 2) == 1; 

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < fullHeartsCount)
            {
                hearts[i].sprite = FullHeartSprite;
            }
            else if (hasHalfHeart && i == fullHeartsCount)
            {
                hearts[i].sprite = HalfHeartSprite;
            }
            else
            {
                hearts[i].sprite = EmptyHeartSprite;
            }
        }
    }
}