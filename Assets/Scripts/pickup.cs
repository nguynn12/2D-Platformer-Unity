using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public enum pickupType { coin,gem,health}

    public pickupType pt;
    [SerializeField] GameObject PickupEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(pt == pickupType.coin)
        {
            if(collision.gameObject.tag == "Player")
            {
                GameManager.instance.IncrementCoinCount();

                AudioManager.instance.PlaySFX(AudioManager.instance.coinSound);
           
                Instantiate(PickupEffect, transform.position, Quaternion.identity);

                Destroy(this.gameObject,0.2f);
                
            }
            
        }

        if (pt == pickupType.gem)
        {
            if (collision.gameObject.tag == "Player")
            {
                GameManager.instance.IncrementGemCount();

                AudioManager.instance.PlaySFX(AudioManager.instance.gemSound);
            
                Instantiate(PickupEffect, transform.position, Quaternion.identity);

                Destroy(this.gameObject, 0.2f);

            }

        }
    }
}
