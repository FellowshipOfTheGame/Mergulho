using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    public GameObject sprite;

    private SpriteRenderer spriteRenderer;
    private AsyncOperation async;

    private void Start()
    {
        spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        StartCoroutine(LoadNewScene());
    }

    private void Update()
    {
        //spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, f);
    }

    private IEnumerator LoadNewScene()
    {
        async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!async.isDone)
            yield return new WaitForSeconds(0.5f);
    }
}
