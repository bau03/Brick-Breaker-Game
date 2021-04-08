using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform paddle;
    public float speed;
    public Transform explossion;
    public GameManager gm;
    public Transform powerup;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver) {
            return;
        }
        if (!inPlay) {
            transform.position = paddle.position;
        }

        if (Input.GetButtonDown("Jump") && !inPlay) {
            inPlay=true;
            rb.AddForce(Vector2.up * speed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bottom"))
        {
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.UpdateLives(-1); //can güncelleme
        }
    }
    void OnCollisionEnter2D(Collision2D other) { //tuglanın kırılması
        if (other.transform.CompareTag("brick")){
            BrickScript brickScript = other.gameObject.GetComponent<BrickScript>();
            if (brickScript.hitsToBreak>1){
                brickScript.BreakBrick(); //özel tugla fonksiyonu
            }
            else { 
            int randomChance = Random.Range(1, 101);
            if (randomChance < 50){
                Instantiate(powerup,other.transform.position,other.transform.rotation); //sayı tutarsa extralife icon cıkar
            }
            Transform newExplosion=Instantiate(explossion, other.transform.position, other.transform.rotation); //Efektin cıkması
            Destroy(newExplosion.gameObject, 2.5f); //efekt clone kaldırma 
            gm.UpdateScore(brickScript.points); //puan güncelleme
            gm.UpdateNumberOfBricks();
            Destroy(other.gameObject); //tuglayı kaybet
            }

            audio.Play();
        }
    }
}
