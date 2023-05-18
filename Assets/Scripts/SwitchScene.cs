using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void SwitchToScene()
    {
        StartCoroutine(LoadScene(1));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public IEnumerator LoadScene(int index)
    {
        // Trigger the "fade" animation.
        animator.SetTrigger("fade");

        // Wait for 1 second to allow the animation to play.
        yield return new WaitForSeconds(1f);

        // Load the specified scene by index.
        SceneManager.LoadScene(index);
    }
}
