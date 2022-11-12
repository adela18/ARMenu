using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAfterTime : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeLoading = 10f;
    [SerializeField]
    private float delayBeforeLoadingFade = 10f;
    [SerializeField]
    private string sceneNamaToLoad;
    private float timeElapsed;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > delayBeforeLoadingFade)
        {
            animator.SetTrigger("FadeOut");
        }

        if (timeElapsed > delayBeforeLoading)
        {
            SceneManager.LoadScene(sceneNamaToLoad);
        }
    }


}
