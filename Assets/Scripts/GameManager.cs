using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TMP_Text coinText;
    [SerializeField] private PlayerController playerController;

    private int coinCount = 0;
    private int gemCount = 0;
    private bool isGameOver = false;
    private Vector3 playerPosition;

    // Level Complete UI
    [SerializeField] GameObject levelCompletePanel;
    [SerializeField] TMP_Text leveCompletePanelTitle;
    [SerializeField] TMP_Text levelCompleteCoins;
    
    private int totalCoins = 0;

    [Header("--- Game Over UI ---")]
    public GameObject gameOverPanel; // Kéo thả cái Panel bị che trong Canvas vào đây

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        UpdateGUI();
        UIManager.instance.fadeFromBlack = true;
        // Lưu lại vị trí an toàn ban đầu để hồi sinh
        playerPosition = playerController.transform.position;

        FindTotalPickups();
    }

    public void IncrementCoinCount()
    {
        coinCount++;
        UpdateGUI();
    }

    public void IncrementGemCount()
    {
        gemCount++;
        UpdateGUI();
    }

    private void UpdateGUI()
    {
        coinText.text = coinCount.ToString();
    }

    public void FindTotalPickups()
    {
        pickup[] pickups = GameObject.FindObjectsOfType<pickup>();

        foreach (pickup pickupObject in pickups)
        {
            if (pickupObject.pt == pickup.pickupType.coin)
            {
                totalCoins += 1;
            }
        }
    }

    public void LevelComplete()
    {
        levelCompletePanel.SetActive(true);
        leveCompletePanelTitle.text = "LEVEL COMPLETE";
        levelCompleteCoins.text = "COINS COLLECTED: " + coinCount.ToString() + " / " + totalCoins.ToString();
    }

    // ==========================================
    // CÁC HÀM XỬ LÝ HỒI SINH VÀ GAME OVER MỚI
    // ==========================================

    // 1. CÒN MẠNG: Chỉ dịch chuyển về chỗ cũ, không load lại Scene
    public void RespawnPlayer()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        // Khóa di chuyển, làm mờ màn hình
        UIManager.instance.DisableMobileControls();
        UIManager.instance.fadeToBlack = true;
        playerController.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        // Bê nhân vật đặt lại vị trí an toàn lúc bắt đầu
        playerController.transform.position = playerPosition;
        playerController.gameObject.SetActive(true);
        
        // Sáng màn hình lên lại và mở khóa di chuyển
        UIManager.instance.fadeFromBlack = true;
        if (playerController.controlmode == Controls.mobile)
            UIManager.instance.EnableMobileControls();
    }

    // 2. HẾT MẠNG: Kết thúc game
    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Debug.Log("GAME OVER!");

            UIManager.instance.DisableMobileControls();
            playerController.gameObject.SetActive(false); // Xóa xác nhân vật
            
            // Hiện panel Game Over mà tác giả đã che
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
            else 
            {
                // Phương án dự phòng: Tận dụng LevelCompletePanel nếu không tìm thấy GameOverPanel
                levelCompletePanel.SetActive(true);
                leveCompletePanelTitle.text = "GAME OVER";
                levelCompleteCoins.text = "Bạn đã hết mạng!";
            }
        }
    }
}