using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void StartGame()
    {
        StartCoroutine(ShowSceneTransition());
    }

    private IEnumerator ShowSceneTransition() 
    {
        animator.enabled = true;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GamePlay Scene");
    }
}
