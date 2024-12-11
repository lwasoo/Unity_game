using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneManagerScript : MonoBehaviour
{
    private static Stack<string> sceneHistory = new Stack<string>(); // 用于记录返回路径

    private static SceneManagerScript instance;
    public static SceneManagerScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Object.FindFirstObjectByType<SceneManagerScript>();
                if (instance == null)
                {
                    Debug.LogError("No SceneManagerScript instance found in the scene!");
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject); // 确保此对象在场景切换时不会销毁
        }
        else
        {
            Destroy(gameObject); // 避免重复实例
        }
    }

    public void LoadScene(string sceneName)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("sceneName: " + sceneName + "||currentScene:" + currentScene);
        if (!sceneName.Equals(currentScene)) // 避免重复加载当前场景
        {
            sceneHistory.Push(currentScene); // 记录当前场景
            SceneManager.LoadScene(sceneName);
        }
    }

    public void GoBack()
    {
        if (sceneHistory.Count > 0)
        {
            string previousScene = sceneHistory.Pop(); // 获取返回目标场景
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.LogWarning("No previous scene in history.");
        }
    }
}
