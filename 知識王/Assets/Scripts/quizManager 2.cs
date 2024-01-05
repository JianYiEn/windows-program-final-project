using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager2 : MonoBehaviour
{
    [System.Serializable]
    public class PhoneticQuestion
    {
        public string phonetic; // 注音
        public string romanization; // 對應的羅馬拼音
    }

    public List<PhoneticQuestion> phoneticQuestions = new List<PhoneticQuestion>(); // 存儲問題的列表
    private PhoneticQuestion currentQuestion; // 當前問題

    public int totalQuestions = 3; // 總測驗題數
    private int currentQuestionIndex = 0; // 當前題目索引

    public Button continueButton; // 繼續按鈕
    public TextMeshProUGUI questionText; // 用於顯示問題的 TextMeshProUGUI
    public Button[] answerButtons; // 答案按鈕
    public TextMeshProUGUI resultText; // 用於顯示結果的 TextMeshProUGUI

    public Color normalColor; // 普通顏色
    public Color highlightColor; // 高亮顏色
    public Color correctColor = Color.green; // 正確答案的顏色
    public Color wrongColor = Color.red; // 錯誤答案的顏色

    private string selectedAnswer; // 選中的答案
    private string correctAnswer;
    public Button checkAnswerButton;
    public TextMeshProUGUI endMessageText;

    void Start()
    {
        continueButton.onClick.AddListener(Continue);
        continueButton.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
        GenerateQuestion();
        checkAnswerButton.onClick.AddListener(CheckAnswer);
    }

    void GenerateQuestion()
    {
        if (phoneticQuestions.Count > 0)
        {
            int randomIndex = Random.Range(0, phoneticQuestions.Count);
            currentQuestion = phoneticQuestions[randomIndex];
            correctAnswer = currentQuestion.romanization; // 更新正確答案
            questionText.text = currentQuestion.phonetic; // 顯示問題的注音
            UpdateAnswerButtons();
        }
    }


    void UpdateAnswerButtons()
    {
        HashSet<string> answers = new HashSet<string>();
        while (answers.Count < answerButtons.Length - 1)
        {
            string potentialAnswer = phoneticQuestions[Random.Range(0, phoneticQuestions.Count)].romanization;
            if (potentialAnswer != correctAnswer)
            {
                answers.Add(potentialAnswer);
            }
        }
        answers.Add(correctAnswer); // 確保包含正確答案

        List<string> shuffledAnswers = new List<string>(answers);
        ShuffleList(shuffledAnswers); // 打亂答案順序

        for (int i = 0; i < answerButtons.Length; i++)
        {
            string answer = shuffledAnswers[i];
            Button button = answerButtons[i];
            button.GetComponentInChildren<TextMeshProUGUI>().text = answer;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => SelectAnswer(answer));
        }

        ResetButtonColors();
    }


    void SelectAnswer(string romanization)
    {
        selectedAnswer = romanization;

        foreach (var button in answerButtons)
        {
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            button.GetComponent<Image>().color = buttonText.text == romanization ? highlightColor : normalColor;
        }
    }

    void CheckAnswer()
    {
        bool isCorrect = selectedAnswer == currentQuestion.romanization;
        resultText.text = isCorrect ? "正確!" : "錯誤!";
        resultText.gameObject.SetActive(true);

        foreach (var button in answerButtons)
        {
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText.text == currentQuestion.romanization)
            {
                button.GetComponent<Image>().color = correctColor;
            }
            else if (buttonText.text == selectedAnswer)
            {
                button.GetComponent<Image>().color = wrongColor;
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


    public void Continue()
    {
        
        if (currentQuestionIndex < totalQuestions - 1)
        {
            currentQuestionIndex++;
            GenerateQuestion();
            continueButton.gameObject.SetActive(false);
            resultText.gameObject.SetActive(false);
        }
        else
        {
            
            endMessageText.text = "測驗結束!";
        }
    }
}
