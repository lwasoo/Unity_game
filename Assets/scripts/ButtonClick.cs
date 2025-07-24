using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems; // 引入 EventSystem 命名空间
using System.Collections;


public class ButtonClick : MonoBehaviour
{
    private const float WAIT = 0.4f;
    public static ButtonClick instance; // 单例模式，方便全局调用
    [SerializeField] private AudioSource sfxSource; // 播放音效的 AudioSource
    [SerializeField] private AudioClip clickSound;  // 点击按钮的音效
    private void Awake()
    {
        // 确保只有一个 SoundManager 实例
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject); // 保持在场景切换时不被销毁
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 播放按钮点击音效的方法
    public void PlayClickSound()
    {
        if (sfxSource != null && clickSound != null)
        {
            sfxSource.PlayOneShot(clickSound);
        }
    }

    public void LoadGameScene()
    {
        PlayClickSound();
        GameObject sceneToLoad = EventSystem.current.currentSelectedGameObject;
        switch (sceneToLoad.name)
        {
            case "play":
                StartCoroutine(LoadSceneWithDelay("mainGameScene"));
                break;

            case "setting":
                StartCoroutine(LoadSceneWithDelay("SettingScene"));
                break;

            case "exit":
                StartCoroutine(ExitWithDelay());
                break;

            default:
                Debug.LogWarning("No matching action for button: " + sceneToLoad.name);
                break;
        }
    }

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(WAIT); // 延迟 0.5 秒
        SceneManagerScript.Instance.LoadScene(sceneName);
    }

    private IEnumerator ExitWithDelay()
    {
        yield return new WaitForSeconds(WAIT); // 延迟 0.5 秒
        Application.Quit();
    }
    public void GoBack()
    {
        PlayClickSound();
        StartCoroutine(BackWithDelay());
    }
    private IEnumerator BackWithDelay()
    {
        yield return new WaitForSeconds(WAIT); // 延迟 0.5 秒
        SceneManagerScript.Instance.GoBack();
    }
}
