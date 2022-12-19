using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }    
}
