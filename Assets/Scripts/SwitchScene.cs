using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    private void Start() {
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
        animator.SetTrigger("fade");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }
}
