using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager3 : MonoBehaviour
{
    public List<string> words = new List<string>(); // 存儲單詞的列表
    private string currentWord; // 當前題目的單詞

    public TextMeshProUGUI questionText; // 用於顯示問題的 TextMeshProUGUI

    private int totalQuestions = 3; // 總測驗題數
    private int currentQuestionIndex = 0; // 當前題目索引
    public Button continueButton; // 繼續按鈕
    public TMP_InputField inputField; // TMP 版本的 InputField
    public TextMeshProUGUI resultText; // 用於顯示結果的 TextMeshProUGUI
    public TextMeshProUGUI endMessageText;


    void Start()
    {
        continueButton.onClick.AddListener(GenerateQuestion);
        continueButton.gameObject.SetActive(false); // 初始時隱藏繼續按鈕
        resultText.gameObject.SetActive(false);
        GenerateQuestion();
    }


    void GenerateQuestion()
    {
        Image inputFieldImage = inputField.GetComponent<Image>();
        resultText.gameObject.SetActive(false); // 重新隱藏結果文字
        continueButton.gameObject.SetActive(false); // 重新隱藏繼續按鈕
        inputFieldImage.color = Color.white;

        if (currentQuestionIndex < totalQuestions && words.Count > 0)
        {
            int randomIndex = Random.Range(0, words.Count);
            currentWord = words[randomIndex];
            questionText.text = currentWord;
            inputField.text = ""; // 清空輸入欄

            currentQuestionIndex++;
        }
        else
        {
            endMessageText.gameObject.SetActive(true);
            endMessageText.text = "測驗結束!";
        }
    }


    public void CheckAnswer()
    {
        Image inputFieldImage = inputField.GetComponent<Image>(); // 獲取 InputField 的 Image 組件
        resultText.gameObject.SetActive(true);
        bool isCorrect = inputField.text == currentWord;
        resultText.text = isCorrect ? "正確！" : "錯誤！";
        if (inputField.text == currentWord)
        {
            resultText.text = "正確!";
            inputFieldImage.color = Color.green; // 設置為綠色
        }
        else
        {
            resultText.text = "錯誤!";
            inputFieldImage.color = Color.red; // 設置為紅色
        }

        if (currentQuestionIndex >= totalQuestions)
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


}

