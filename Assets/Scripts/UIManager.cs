using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject mobileControls;

    public bool fadeToBlack, fadeFromBlack;
    public Image blackScreen;
    public float fadeSpeed = 2f;

    //player reference

    public PlayerController playerController;

    [Header("--- Menu UI Panels ---")]
    public GameObject mainPanel;   // Kéo MainPanel vào đây
    public GameObject aboutPanel;  // Kéo AboutPanel vào đây

    // Hàm mở Menu About (Tắt Main, Bật About)
    public void OpenAboutMenu()
    {
        if (mainPanel != null) mainPanel.SetActive(false);
        if (aboutPanel != null) aboutPanel.SetActive(true);
    }

    // Hàm đóng Menu About (Tắt About, Bật Main)
    public void CloseAboutMenu()
    {
        if (aboutPanel != null) aboutPanel.SetActive(false);
        if (mainPanel != null) mainPanel.SetActive(true);
    }

    private void Awake()
    {
        instance = this;
    }

    public void DisableMobileControls()
    {
        mobileControls.SetActive(false);
    }
    public void EnableMobileControls()
    {
        mobileControls.SetActive(true);
    }

    private void Update()
    {
        UpdateFade();
    }

    private void UpdateFade()
    {
        if (fadeToBlack)
        {
            FadeToBlack();
        }
        else if (fadeFromBlack)
        {
            FadeFromBlack();
        }
    }

    private void FadeToBlack()
    {
        FadeScreen(1f);

        if (blackScreen.color.a >= 1f)
        {
            fadeToBlack = false;
        }
    }

    private void FadeFromBlack()
    {
        FadeScreen(0f);

        if (blackScreen.color.a <= 0f)
        {
            if(playerController.controlmode == Controls.mobile)
            {
                EnableMobileControls();
            }
            fadeFromBlack = false;
        }
    }

    private void FadeScreen(float targetAlpha)
    {
        Color currentColor = blackScreen.color;
        float newAlpha = Mathf.MoveTowards(currentColor.a, targetAlpha, fadeSpeed * Time.deltaTime);
        blackScreen.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
    }
}
