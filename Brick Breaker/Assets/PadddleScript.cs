using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadddleScript : MonoBehaviour
{
    public float speed; //hızını ayarladın
    public float rightScreenEdge;
    public float leftScreenEdge;
    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver){
            return;
        }
        float horizontal = Input.GetAxis("Horizontal"); //klavyeden gelen değer göre saga sola gitmesini sağladın.
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);
        if (transform.position.x < leftScreenEdge){
            transform.position = new Vector2(leftScreenEdge, transform.position.y);
        }
        if (transform.position.x > rightScreenEdge)
        {
            transform.position = new Vector2(rightScreenEdge, transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("extraLife")) { 
        gm.UpdateLives(1);  //Extractor can
        Destroy(other.gameObject);
        }
    }
}
