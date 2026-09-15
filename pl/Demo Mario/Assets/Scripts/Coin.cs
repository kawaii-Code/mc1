using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private int count;

    private GameObject player;
    private AudioSource audioSource;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = player.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().AddCoin(count);
            audioSource.PlayOneShot(audioClip);
            Destroy(gameObject);
        }
    }
}
