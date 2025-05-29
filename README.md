# Frame Analysis (FPS 정확도 측정 프로그램)

Unity로 제작한 FPS(Frame Per Second) 별 색상 인식 정확도 측정 프로그램입니다.  
사용자가 120Hz와 144Hz 환경에서 색상 변화를 얼마나 잘 인지하는지 실험하고, 결과를 CSV 파일로 저장합니다.

---

##  프로젝트 특징

- **FPS 모드 선택**: 120Hz, 144Hz 중 선택 후 실험 진행
- **정확도 측정**: 색상 인지 결과를 바탕으로 평균 정확도(%) 계산
- **CSV 저장**: `result_log.csv` 파일에 결과 저장

---

## 실행 방법

1. [Releases 페이지](https://github.com/mnse1/Frame_Analysis/releases)에서 최신 빌드 파일(`Frame_Analysis_v1.0.zip`) 다운로드
2. 압축 해제 후 `Frame_Analysis.exe` 실행
3. 실험 완료 후, 실행한 폴더에 `result_log.csv` 파일이 생성됩니다.

---

##  결과 파일 (`result_log.csv`)
    FPS,AverageAccuracy(%)
    120,92.5
    144,88.3
    ...


---

## 개발 환경

- Unity 2022.X.X
- C#
- Windows

---

## 주요 코드

| 파일명              | 설명                           |
|-------------------|--------------------------------|
| `GameManager.cs`   | 실험 로직 및 UI 관리             |
| `InputHandler.cs`   | 사용자 입력 처리 (`Space`, 클릭)     |
| `MovingObject.cs`   | 움직이는 오브젝트 제어           |
| `ResultLogger.cs`   | CSV 파일 저장 기능              |

---

## 프로젝트 구조
    Frame_Analysis/
    ├── Assets/
    ├── Packages/
    ├── ProjectSettings/
    ├── .gitignore
    ├── README.md

