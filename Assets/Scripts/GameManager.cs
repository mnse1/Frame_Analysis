using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public MovingObject movingObject;
    public Transform goalPoint;
    public InputHandler inputHandler;

    public Button mode1Button;
    public Button mode2Button;
    public Button startButton;
    public Button restartButton;

    public TMP_Text countdownText;

    private int targetFPS = 120;
    private bool hasStopped = false;

    private int currentTrial = 0;
    private int maxTrials = 5;
    private bool isMeasuring = false;
    
    private List<float> accuracyList = new List<float>();

    void Start()
    {
        mode1Button.gameObject.SetActive(true);
        mode2Button.gameObject.SetActive(true);

        restartButton.gameObject.SetActive(false);
        restartButton.onClick.AddListener(RestartExperiment);

        QualitySettings.vSyncCount = 0;

        inputHandler.enabled = false;
        countdownText.text = "";

        // Start 버튼 비활성화
        startButton.interactable = false;
        startButton.gameObject.SetActive(false); // 화면에는 보이되 눌 수 없음

        // 모드 선택 버튼 이벤트 연결
        mode1Button.onClick.AddListener(() => OnModeSelected(120));
        mode2Button.onClick.AddListener(() => OnModeSelected(144));

        // Start 버튼에 카운트다운 연결
        startButton.onClick.AddListener(() => StartCoroutine(StartCountdown()));
    }

    void OnModeSelected(int fps)
    {
        targetFPS = fps;
        Application.targetFrameRate = fps;

        // 모드 버튼 숨기기
        mode1Button.gameObject.SetActive(false);
        mode2Button.gameObject.SetActive(false);
        currentTrial = 0;
        // Start 버튼 활성화
        startButton.gameObject.SetActive(true);
        startButton.interactable = true;
    }

    IEnumerator StartCountdown()
    {
        startButton.interactable = false;
        startButton.gameObject.SetActive(false);
        hasStopped = false;
        movingObject.ResetPosition();
        countdownText.text = "Ready...";
        yield return new WaitForSeconds(1f);

        countdownText.text = "Go!";
        yield return new WaitForSeconds(0.5f);

        countdownText.text = "";

        movingObject.StartMoving();
        inputHandler.enabled = true;
        isMeasuring = true;
    }



    public void StopObject()
    {
        if (!isMeasuring || hasStopped) return;

        hasStopped = true;
        isMeasuring = false;
        movingObject.StopMoving();
        inputHandler.enabled = false;

        float centerDistance = Vector2.Distance(movingObject.transform.position, goalPoint.position);
        float rawError = Vector2.Distance(movingObject.transform.position, goalPoint.position);

        float maxError = 2f; // 허용 오차 기준
        float accuracy = Mathf.Clamp01(1f - rawError / maxError) * 100f;

        accuracyList.Add(accuracy);
    
        // 화면에 정확도 표시
        countdownText.text = $"accuracy: {accuracy:F1}%";

        currentTrial++;
        if (currentTrial < maxTrials)
        {
            StartCoroutine(NextTrialWithDelay());
        }
        else
        {
            float average = 0f;
            foreach (float a in accuracyList) average += a;
            average /= accuracyList.Count;

            // CSV에 평균만 저장
            ResultLogger.LogAverage(targetFPS, average);

            // 화면에도 표시
            countdownText.text = $"End\nAverage: {average:F1}%";
            restartButton.gameObject.SetActive(true);
        }
        

    }


    IEnumerator NextTrialWithDelay()
    {
        if (currentTrial < maxTrials)
            countdownText.text = $"({currentTrial}/{maxTrials})";
        else
            countdownText.text = "last";
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(StartCountdown());
    }   



    float deltaTime = 0.0f;

    void Update()
    {
        transform.Translate(Vector2.right * movingObject.moveSpeed * Time.deltaTime);
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
    }

    void RestartExperiment()
    {
        // UI 초기화
        mode1Button.gameObject.SetActive(true);
        mode2Button.gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);
        startButton.interactable = false;
        restartButton.gameObject.SetActive(false);

        // 내부 상태 초기화
        currentTrial = 0;
        accuracyList.Clear();
        countdownText.text = "";

        // 오브젝트 위치 초기화
        movingObject.ResetPosition();
    }

}

