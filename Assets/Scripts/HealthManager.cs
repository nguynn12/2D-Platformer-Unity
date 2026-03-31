using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public GameObject damageEffect;

    // Đổi MaxHealth thành 3 (Tương đương 3 mạng trọn vẹn)
    private int MaxHealth = 3;
    public int currentHealth;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite FullHeartSprite;
    // (Đã xóa biến HalfHeartSprite vì chúng ta không xài nữa)
    [SerializeField] private Sprite EmptyHeartSprite;

    private GameObject Player;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        currentHealth = MaxHealth; // Bơm đầy 3 mạng lúc mới vào game
        DisplayHearts();
    }

    public void HurtPlayer()
    {
        if (currentHealth > 0)
        {
            // Trừ đúng 1 điểm (tương đương 1 trái tim)
            currentHealth--;
            DisplayHearts();

            // Phát âm thanh
            if (AudioManager.instance != null) 
                AudioManager.instance.PlaySFX(AudioManager.instance.deathSound);

            // Sinh hiệu ứng (nếu có kéo thả vào)
            if (damageEffect != null)
            {
                Instantiate(damageEffect, Player.transform.position, Quaternion.identity);
            }

            // Kiểm tra số mạng còn lại
            if (currentHealth <= 0)
            {
                GameManager.instance.GameOver(); 
            }
            else
            {
                GameManager.instance.RespawnPlayer();
            }
        }
    }

    // Thuật toán hiển thị tim mới: Siêu đơn giản và trực quan
    public void DisplayHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = FullHeartSprite; // Còn mạng thì hiện tim đỏ
            }
            else
            {
                hearts[i].sprite = EmptyHeartSprite; // Mất mạng thì hiện tim xám (rỗng)
            }
        }
    }
}