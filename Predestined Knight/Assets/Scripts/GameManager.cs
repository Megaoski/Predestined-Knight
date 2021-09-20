using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [System.NonSerialized]public bool gameOver = false;

    public float restartDelay = 1f;
    public Animator anim;
    public void EndGame()
    {
        if (gameOver == false)
        {
            gameOver = true;

            anim.SetTrigger("isDead");
            Debug.Log("GAME OVER");
            Invoke("Restart", restartDelay);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
