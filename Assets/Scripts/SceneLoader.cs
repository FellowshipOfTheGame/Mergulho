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
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.PingPong(Time.time, 0.5f));
    }

    private IEnumerator LoadNewScene()
    {
        async = SceneManager.LoadSceneAsync(sceneName);

        while (!async.isDone)
            yield return null;
    }
}
