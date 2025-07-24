using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.IO;
using LitJson;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI dangerText;
    public Transform choicesPanel;
    public GameObject choiceButtonPrefab;
    public Button clickCatcherButton;  // �� Inspector ��� ClickCatcher ��ť

    public Image backgroundImage; // �Ͻ� Inspector
    public Image dialogBackground;
    public List<Sprite> backgroundSprites; // ���б���ͼ��Դ��������ƥ��

    private List<DialogueEntry> dialogueList;
    private Dictionary<string, DialogueEntry> dialogueDict;
    private DialogueEntry currentEntry;

    private int dangerValue = 0;

    void Start()
    {
        LoadDialogue();
        ShowDialogue("start");
    }

    void LoadDialogue()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "dialog.json");
        string json = File.ReadAllText(path);
        dialogueList = JsonMapper.ToObject<List<DialogueEntry>>(json);
        dialogueDict = new Dictionary<string, DialogueEntry>();
        foreach (var entry in dialogueList)
        {
            dialogueDict[entry.id] = entry;
        }
    }

    void ShowDialogue(string id)
    {
        ClearChoices();

        if (!dialogueDict.ContainsKey(id)) return;

        currentEntry = dialogueDict[id];
        speakerText.text = currentEntry.speaker;
        dialogueText.text = currentEntry.text;
        UpdateDangerDisplay();

        // 1. ���±�������������ˣ�
        if (!string.IsNullOrEmpty(currentEntry.background))
        {
            UpdateBackground(currentEntry.background);
        }

        dialogBackground.gameObject.SetActive(true);

        if (currentEntry.end)
        {
            //dialogueText.text += "\n\n���Ի�������";
            //dialogBackground.gameObject.SetActive(false);
            return;
        }

        if (currentEntry.choices != null && currentEntry.choices.Count > 0)
        {
            foreach (var choice in currentEntry.choices)
            {
                GameObject btnObj = Instantiate(choiceButtonPrefab, choicesPanel);
                btnObj.GetComponentInChildren<TextMeshProUGUI>().text = choice.option;
                btnObj.GetComponent<Button>().onClick.AddListener(() =>
                {
                    ApplyDangerDelta(choice.dangerDelta);
                    ShowDialogue(choice.next);
                });
            }
        }
        else if (!string.IsNullOrEmpty(currentEntry.next))
        {
            // ���õ������
            clickCatcherButton.gameObject.SetActive(true);
            clickCatcherButton.onClick.RemoveAllListeners();
            clickCatcherButton.onClick.AddListener(() =>
            {
                clickCatcherButton.gameObject.SetActive(false);
                ShowDialogue(currentEntry.next);
            });
        }
    }

    void ClearChoices()
    {
        foreach (Transform child in choicesPanel)
        {
            Destroy(child.gameObject);
        }
    }

    void ApplyDangerDelta(int delta)
    {
        dangerValue += delta;
        dangerValue = Mathf.Clamp(dangerValue, 0, 100);
        UpdateDangerDisplay();
    }

    void UpdateDangerDisplay()
    {
        dangerText.text = $"Σ��ֵ��{dangerValue}";
    }

    // ��������������ͼ�л� + ����Ӧ
    void UpdateBackground(string bgName)
    {
        Sprite newBg = backgroundSprites.FirstOrDefault(s => s.name == bgName);
        if (newBg != null)
        {
            backgroundImage.sprite = newBg;
            backgroundImage.preserveAspect = false;

            // ����ͼƬΪԭʼ��С
            //backgroundImage.SetNativeSize();

            // ��Ļ��������
            float screenRatio = (float)Screen.width / Screen.height;
            float imageRatio = newBg.rect.width / newBg.rect.height;

            if (imageRatio > screenRatio)
            {
                float height = backgroundImage.rectTransform.sizeDelta.y;
                backgroundImage.rectTransform.sizeDelta = new Vector2(height * imageRatio, height);
            }
            else
            {
                float width = backgroundImage.rectTransform.sizeDelta.x;
                backgroundImage.rectTransform.sizeDelta = new Vector2(width, width / imageRatio);
            }
        }
        else
        {
            Debug.LogWarning($"����ͼ {bgName} δ�ҵ���");
        }
    }
}

[System.Serializable]
public class DialogueEntry
{
    public string id;
    public string speaker;
    public string text;
    public string next;
    public bool end;
    public List<Choice> choices;
    public string background;
}

[System.Serializable]
public class Choice
{
    public string option;
    public string next;
    public int dangerDelta;
}
