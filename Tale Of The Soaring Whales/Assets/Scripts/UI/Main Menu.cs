using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartButton()
    {
        Debug.Log("EFDFASAFSDFADFF");
        animator.SetTrigger("Leave");
    }
    
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
