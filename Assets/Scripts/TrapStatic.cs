using UnityEngine;

public class TrapStatic : MonoBehaviour
{
    // OnTriggerEnter2D được gọi khi Player đi xuyên qua vùng Trigger của bẫy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem đối tượng chạm vào bẫy có tag là "Player" không
        if (collision.CompareTag("Player"))
        {
            HealthManager.instance.HurtPlayer();
        }
    }
}