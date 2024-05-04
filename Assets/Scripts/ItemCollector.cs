using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int strawberries = 0;
    private int melon = 0;
    private int pineapple = 0;


    [SerializeField] private Text strawberryText;
    [SerializeField] private Text melonText;
    [SerializeField] private Text pineappleText;


    [SerializeField] private AudioSource collectionSoundEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Strawberry"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            strawberries++;
            
            strawberryText.text = "Strawberries: " + strawberries;
        }

        if (collision.gameObject.CompareTag("Melon"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            melon++;
            melonText.text = "Melons: " + melon;
        }

        if (collision.gameObject.CompareTag("Pineapple"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            pineapple++;
            pineappleText.text = "Pineapples: " + pineapple;
        }


    }

}
