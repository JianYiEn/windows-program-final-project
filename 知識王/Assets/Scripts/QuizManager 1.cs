using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager1 : MonoBehaviour
{
    [System.Serializable]
    public class AudioQuestion
    {
        public AudioClip audioClip;
        public string correctPhonetic;
    }
    public int totalQuestions = 3; // 總測驗題數
    private int currentQuestionIndex = 0; // 當前題目索引
    public Button continueButton; // 繼續按鈕
    public AudioSource audioSource; // 用於播放音檔的 AudioSource
    public List<AudioQuestion> questions = new List<AudioQuestion>(); // 存儲音檔和答案的列表
    private AudioQuestion currentQuestion; // 當前題目
    private string selectedAnswer;
    private string correctAnswer;
    public Button[] answerButtons;
    public Color normalColor; // 普通顏色
    public Color highlightColor; // 高亮顏色
    public TextMeshProUGUI resultText; // 用於顯示結果的 TextMeshProUGUI
    public TextMeshProUGUI questionText;
    public Color correctColor = Color.green; // 正確答案的顏色
    public Color wrongColor = Color.red; // 錯誤答案的顏色
    public Button checkAnswerButton;
    public TextMeshProUGUI endMessageText;

    void Start()
    {
        // 假設您已經設置了正確答案
        correctAnswer = "正確的注音"; // 這裡需要根據實際情況設置
        resultText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);

        // 為每個答案按鈕添加點擊事件
        foreach (var button in answerButtons)
        {
            string phoneticText = button.GetComponentInChildren<TextMeshProUGUI>().text;
            button.onClick.AddListener(() => SelectAnswer(phoneticText));
        }

        checkAnswerButton.onClick.AddListener(CheckAnswer);
        GenerateQuestion();
    }

    void SelectAnswer(string phonetic)
    {
        selectedAnswer = phonetic;

        // 更新按鈕顏色
        foreach (var button in answerButtons)
        {
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText.text == phonetic)
            {
                button.GetComponent<Image>().color = highlightColor; // 選中的答案高亮顯示
            }
            else
            {
                button.GetComponent<Image>().color = normalColor; // 其他答案恢復正常顏色
            }
        }
    }


    void CheckAnswer()
    {
        bool isCorrect = selectedAnswer == correctAnswer;
        resultText.text = isCorrect ? "正確!" : "錯誤!";
        resultText.gameObject.SetActive(true);
        // 更新答案按鈕顏色
        foreach (var button in answerButtons)
        {
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText.text == correctAnswer)
            {
                button.GetComponent<Image>().color = correctColor; // 正確答案顯示為綠色
            }
            else if (buttonText.text == selectedAnswer)
            {
                button.GetComponent<Image>().color = wrongColor; // 選擇的錯誤答案顯示為紅色
            }
            else
            {
                button.GetComponent<Image>().color = normalColor; // 其他答案保持普通顏色
            }
        }
        if (currentQuestionIndex >= totalQuestions - 1)
        {
            // 最後一題已回答
            endMessageText.gameObject.SetActive(true);
            endMessageText.text = "測驗結束!";
            continueButton.gameObject.SetActive(false); // 不再顯示繼續按鈕
        }
        else
        {
            continueButton.gameObject.SetActive(true); // 顯示繼續按鈕以進入下一題
        }
    }


    void GenerateQuestion()
    {
        resultText.gameObject.SetActive(false);
        if (questions.Count > 0)
        {
            int randomIndex = Random.Range(0, questions.Count);
            currentQuestion = questions[randomIndex];

            correctAnswer = currentQuestion.correctPhonetic; // 更新正確答案
            questionText.text = correctAnswer; // 顯示問題的注音

            // 更新答案按鈕的文本和音檔
            UpdateAnswerButtons();

            // 重置答案按鈕的顏色
            ResetButtonColors();
        }
    }

    void UpdateAnswerButtons()
    {
        HashSet<AudioQuestion> answerSet = new HashSet<AudioQuestion>();
        while (answerSet.Count < 5)
        {
            AudioQuestion potentialAnswer = questions[Random.Range(0, questions.Count)];
            if (potentialAnswer.correctPhonetic != currentQuestion.correctPhonetic)
            {
                answerSet.Add(potentialAnswer);
            }
        }
        answerSet.Add(currentQuestion); // 確保包含正確答案

        List<AudioQuestion> shuffledAnswers = new List<AudioQuestion>(answerSet);
        ShuffleList(shuffledAnswers);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            AudioClip currentClip = shuffledAnswers[i].audioClip;
            string phonetic = shuffledAnswers[i].correctPhonetic;
            Button button = answerButtons[i];
            button.GetComponentInChildren<TextMeshProUGUI>().text = phonetic;

            button.onClick.RemoveAllListeners(); // 移除所有現有的監聽器

            // 為按鈕添加播放音檔的監聽器
            button.onClick.AddListener(() => PlayAudioClip(currentClip));

            // 為按鈕添加選擇答案的監聽器
            button.onClick.AddListener(() => SelectAnswer(phonetic));
        }
    }

    void PlayAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void Continue()
    {
        // 檢查是否達到題數限制
        if (currentQuestionIndex < totalQuestions - 1)
        {
            currentQuestionIndex++;
            GenerateQuestion();
            continueButton.gameObject.SetActive(false); // 隱藏繼續按鈕
        }
        else
        {
            endMessageText.text = "測驗結束!";
        }
    }
    void ResetButtonColors()
    {
        foreach (var button in answerButtons)
        {
            button.GetComponent<Image>().color = normalColor;
        }
    }
    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
