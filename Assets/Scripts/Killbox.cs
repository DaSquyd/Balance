using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour {

    public AudioSource popSound;

    private void OnTriggerEnter2D(Collider2D collision) {
        popSound.Play();
        Destroy(collision.gameObject);
    }
}
