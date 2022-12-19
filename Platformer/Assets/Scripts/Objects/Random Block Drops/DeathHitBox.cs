
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHitBox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            print("Squished");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }  
}
