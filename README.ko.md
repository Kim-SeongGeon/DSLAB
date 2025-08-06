# DS LAB 프로젝트

<img src="https://capsule-render.vercel.app/api?type=waving&color=413fd9&height=150&section=header&text=Mapping%20the%20Future%20of%20Ground%20Robotics🤖&fontSize=35" />

## 우리의 DS LAB 프로젝트 기록✍️✍

<p><br></p>

## 2025년 6월 11일

### 📝 할 일 (2025‑06‑11)

- [x] X‑Plane과 Mission Planner 간 양방향 데이터 교환 비교

### 📌 메모
현재 X‑Plane과 Mission Planner 간 연결은 성공적으로 이루어졌으며, 시뮬레이션을 육안으로 확인했을 때 큰 문제는 없어 보입니다.

### ✅ 결론

현재 육안으로는 큰 문제가 관찰되지 않았지만, 눈으로 확인되지 않는 영역에서 데이터 손실이 발생할 수 있으므로 데이터 손실률 측정이 필요합니다.

<p><br></p>

---

## 2025년 6월 20일

### 📝 할 일 (2025‑06‑20)

- [x] X‑Plane과 Mission Planner 간 통신의 데이터 손실률 측정

### 📌 메모
X‑Plane과 Mission Planner 간 연결은 부분적으로 성공했으며, 이는 논문에 포함되어야 할 핵심 내용이므로 반드시 측정해야 합니다.
따라서 대구공항에서 인천공항까지의 비행을 기반으로 측정을 진행했습니다.  

<details>
<summary>📉 핑 RTT 및 데이터 손실률 그래프</summary>
  <p align="left">
    <img src="https://github.com/user-attachments/assets/8cfac926-1902-4d20-8c34-0ba0ff40d2fb" width="580" height="420" alt="데이터손실률">
    <br>
    <em>ㄴ Graph showing Ping RTT and Data Loss Rate during flight simulation from Daegu to Incheon using X-Plane and Mission Planner.</em>
  </p>
</details>

<p><br></p>

---

## 2025년 6월 2일 – 6월 20일

### 📝 할 일 (2025‑06‑02 ~ 2025‑06‑20)

- [x] KSCI 2025 여름 학술대회 논문 초안 작성 및 수정
- [x] 지도교수님의 피드백을 바탕으로 연구 방향 및 제목 구조 확정

### 📌 메모
이 기간 동안 “표준 프로토콜을 적용한 시뮬레이션 기반 UAV 제어 검증 시스템 구현”이라는 제목의 논문을 KSCI 2025 여름 학술대회 제출용으로 초안 작성 및 정련했습니다.

논문 작성 과정에서 지도교수님의 건설적인 지도에 따라 다음과 같은 핵심 사항들을 해결했습니다:

1. 제목의 명확성
> “논문의 제목은 초록의 거의 절반 정도가 되어야 한다.”  
연구 목적이 즉시 인식될 수 있도록, 단일 GCS를 통해 다양한 UAV 프로토콜을 관리한다는 핵심 기여를 제목에 반영했습니다.

2. 목적과 방법의 구분 
> “우리의 핵심 연구 목적은 단일 GCS를 사용해 여러 프로토콜을 가진 UAV를 관리하는 것이다.
SIL 시뮬레이션과 프로토콜 변환은 그 목적을 달성하기 위한 도구이지, 목표 자체는 아니다.”

3. 반복적 정제  
위 피드백을 반영해 초록과 도입부를 다시 구성하여, 다중 GCS 비효율 문제를 분명히 제기하고, STANAG‑4586 기반의 VSM을 주요 해결책으로 제시했습니다.

### 📄 논문 요약
- 목적: 여러 UAV에 대해 다수의 GCS가 필요해지는 비효율성을 해결하기 위해, 프로토콜 변환을 이용한 단일 통합 제어 시스템을 제안.
- 접근: UAV 통신 프로토콜 간 변환을 담당하는 VSM(Vehicle Specific Module)을 개발하고, 이를 X‑Plane과 Mission Planner를 기반으로 한 SIL(Software-In‑the‑Loop) 시뮬레이션 환경에 통합.
- 검증: 시뮬레이션 UAV와 GCS 간 실시간 통신, 패킷 변환, 안정성을 시각 검증 및 데이터 전송 검증을 통해 입증.
- 성과: 다양한 UAV 제어를 지원할 수 있는 프로토콜 표준화(STANAG‑4586) 적용 가능성을 확인했으며, 추후 지상 로봇 통합으로 확장 계획 있음.

<p><br></p>

---

### 📆 일정 안내

연구 진행 과정의 투명성을 위해, 다음 기간은 외부 학술 및 창업 행사의 참여로 인한 연구 활동의 일시 중단을 반영합니다:

<details> 
<summary>KSCI‑해커톤</summary>
  <p align="left">
  <img src="https://github.com/user-attachments/assets/ff3647de-16ce-4be9-878d-a8a867414b0b" width="420" height="580" alt="KSCI‑Hackathon">
  <br>
  <em>ㄴ 제주대학교에서 열린 2025 KSCI 해커톤 포스터</em></p>
</details> - **2025년 7월 8일 ~ 11일**: KSCI‑해커톤 참가 및 KSCI 2025 여름 학술대회 발표

<p><br></p>
  
<details>
<summary>SW 스타트업 캠프</summary>
  <p align="left">
  <img src="https://github.com/user-attachments/assets/7893966c-60c3-457f-87a4-1cfcf9589871" width="420" height="580" alt=SW Startup Camp" >
  <br>
  <em>ㄴ 광주 현대 해상 하이비전 센터에서 열린 2025 SW 스타트업 캠프 포스터</em> </p>
</details> - **2025년 7월 14일 ~ 18일**: 기술 기반 스타트업 개발 중심 SW Startup Camp 참여

<p><br></p>

---

## 2025년 7월 11일

### 🏅 발표 및 수상

**🗓 2025년 7월 11일 — 우수 논문상 수상**  
표준 프로토콜 적용 시뮬레이션 기반 UAV 제어 검증 시스템에 대한 연구를 발표했고, 한국컴퓨터정보학회(KSCI) 2025년 여름 학술대회에서 우수 논문상을 수상했습니다.

📄 **논문 제목:** *Implementation of a Simulation-Based UAV Control Verification System Applying Standard Protocols*  
🧑‍🔬 **저자:** 김소정, 김성건, 하옥균 (경운대학교)  
🏛 **주최:** 한국컴퓨터정보학회(KSCI), 2025 여름 학술대회

<details>
<summary>🏅 우수 논문상 – KSCI 2025 여름 학술대회</summary>
  <p align="left">
  <img src="https://github.com/user-attachments/assets/3cad90ab-c0cc-487e-a3a0-e26faae8d7b6" width="420" height="580" alt="2025_1 우수논문상" >
  <br>
  <em>ㄴ 2025년 7월, KSCI에서 수여한 우수 논문상 인증서.</em>
</details>

<p><br></p>

---

## 2025년 7월 29일

### 📝 할 일 (2025‑07‑29)

- [x] Mission Planner에서 지도의 축척 확인.
- [x] Mission Planner 맵에 표시된 항공기 아이콘을 사용자 지정 아이콘으로 변경 가능한지 확인.
<!-- Markdown에서 체크박스는 다음과 같이 표시합니다:

[ ] → 아직 안 한 항목 (빈 체크박스)

[x] → 완료한 항목 (체크된 박스) -->

### 📌 메모
<details>
<summary>🖼️ Mission Planner 지도 화면</summary>
  <p align="left">
  <!--<img src="https://github.com/user-attachments/assets/68ca9f17-48d9-4877-9081-39726e4cbfd0" width="580" height="500" alt="MP">-->
  <img src="https://github.com/user-attachments/assets/c564c51c-6e7c-4e95-bc4d-441373c15987" width="580" height="420" alt="MP+UBUNTU" >
  <br>
  <em>ㄴ Mission Planner의 현재 항공기 아이콘 표시 화면.</em>
</details>

현재 Mission Planner의 지도로는 2025년 Google Map이 사용되고 있습니다.

하지만 확대했을 때 이미지가 너무 픽셀화되어 실제 로봇 활용에는 비실용적입니다.
따라서 Google Earth 사용을 고려 중입니다.

> 📷 참조: 이미지 예시는 위의 🖼️ 지도 표시 예시 섹션을 참조하세요.

현재 Mission Planner에서 붉은 항공기 아이콘이 표시되고 있지만, 연구는 지상 로봇에 초점을 두고 있기 때문에 연구 목적에 더 적합한 아이콘으로 변경하고자 합니다.

다운로드한 Mission Planner 및 ArduPilot 디렉토리의 모든 이미지 파일을 확인했으나 관련 아이콘 파일을 찾지 못했습니다. 
결과적으로 ArduPilot 커뮤니티에 문의 글을 올렸고, 다른 사용자들로부터 도움을 받았습니다:    
> 👉 [ArduPilot Forum Discussion – Changing Vehicle Icon in Mission Planner](https://discuss.ardupilot.org/t/how-to-change-icon-in-mission-planner-map/137234)

### ✅ 결론

지금까지의 설정을 유지하면서 실시간으로 차량 움직임을 시각화하고 데이터 손실 없이 흐름을 유지하기 위해 Google Earth 통합을 계획 중입니다.

ArduPilot 커뮤니티의 지침을 따르고 관련 아이콘 파일을 찾으려 했지만, 현재 제공된 디렉토리에서는 찾을 수 없었습니다.
계속해서 조사하여 지상 로봇 연구에 더 적합한 사용자 지정 심볼로 아이콘을 교체하는 데 성공할 수 있도록 노력할 예정입니다.

<p><br></p>

---

## 2025년 8월 5일

### 📝 할 일 (2025‑08‑05)

- [x] Mission Planner 맵에 표시된 항공기 아이콘이 들어있는 파일 찾고 수정.

### 📌 메모

Windows C 드라이브 쪽에 Ardupilot과 Mission Planner 파일 내에 png 혹은 jpg 파일을 다 살펴봤지만, 비슷한 것도 나오지 않음.  
ㄴ Ardupilot Disourse 사이트에 질문을 남겨서 달아준 댓글들을 봤지만 댓글 달아준 사용자와 환경이 달라서인지 찾지 못했음:  
> 👉 [ArduPilot Forum Discussion – Changing Vehicle Icon in Mission Planner](https://discuss.ardupilot.org/t/how-to-change-icon-in-mission-planner-map/137234)

### ✅ 결론

Ardupilot Mission Planner 공식(?) Github에서 바꿔야 하는 비행체 이미지 파일을 찾음. 
> 👉 [MissionPlanner/Resources/planetracker.png](https://github.com/ArduPilot/MissionPlanner/blob/master/Resources/planetracker.png)

그래서 Mission Planner 설치 때 혹시나 PC 내에 같이 설치된 Github를 찾아보고 있다면 바꿔보고 Mission Planner map에도 잘 나오는지 확인해야 함.   
=> 바뀌는 거 확인 후 Mission Planner <-> Google Earth 연동해야할 듯

<p><br></p>

---

## 2025년 8월 6일

### 📝 할 일 (2025‑08‑06)

- [x] Mission Planner 아이콘 변경 방법 관련 조사
- [ ] 현재 Mission Planner 맵에 표시되고 있는 2025 Google Map 대신 Google Earth 연동.

### 📌 메모

설치된 Mission Planner 실행 버전에서는 planetracker.png 파일이 리소스에 포함되어 있지만, 일반적인 탐색기로는 직접 확인이 불가.   
=> planetracker.png 파일은 Mission Planner 실행 파일 내부의 리소스로 임베디드(내장)되어 있기 때문.  

설치된 Mission Planner 폴더에서는 planetracker.png가 별도로 존재하지 않고, 대신 MissionPlanner.exe 내부에 포함되어 실행 중에 동적으로 불러와서 사용.

### ✅ 결론

해결될 거 같은 방법으로는 Mission Planner 소스를 직접 수정해서 새로 빌드하는 것이 베스트라고 생각함.   
1. GitHub에서 Mission Planner 전체 소스를 클론 :
> bash  
> git clone https://github.com/ArduPilot/MissionPlanner.git

2. Resources/planetracker.png 파일을 원하는 이미지로 교체
3. Visual Studio에서 빌드 (필요하다면 필요한 SDK 설치 필요)
