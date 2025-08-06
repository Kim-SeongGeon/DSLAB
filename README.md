# DS LAB PROJECT

<img src="https://capsule-render.vercel.app/api?type=waving&color=413fd9&height=150&section=header&text=Mapping%20the%20Future%20of%20Ground%20Robotics🤖&fontSize=35" />

## Record our DS LAB PROJECT✍️

<p><br></p>

## June 11, 2025

### 📝 Todo List (06/11/2025)

- [x] Comparison of bidirectional data exchange between X-Plane and Mission Planner.

### 📌 Notes
Currently, the connection between X-Plane and Mission Planner has been successfully established, and upon visual inspection of the simulation, there appear to be no major issues.

### ✅ Conclusion

Although no major issues are observed through visual inspection at present, data loss may occur in areas that are not visible, making it necessary to measure the data loss rate.

<p><br></p>

---

## June 20, 2025

### 📝 Todo List (06/20/2025)

- [x] Measurement of data loss rate between X-Plane and Mission Planner communication.

### 📌 Notes
The connection between X-Plane and Mission Planner has been partially successful, and since this is essential content to be included in the paper, it must be measured.  
Therefore, the measurement was based on a flight from Daegu Airport to Incheon Airport.  

<details>
<summary>📉 Ping RTT & Data Loss Rate Graph</summary>
  <p align="left">
    <img src="https://github.com/user-attachments/assets/8cfac926-1902-4d20-8c34-0ba0ff40d2fb" width="580" height="420" alt="데이터손실률">
    <br>
    <em>ㄴ Graph showing Ping RTT and Data Loss Rate during flight simulation from Daegu to Incheon using X-Plane and Mission Planner.</em>
  </p>
</details>

<p><br></p>

---

## June 2 - 20, 2025

### 📝 Todo List (06/02–06/20/2025)

- [x] Drafting and revising the conference paper for KSCI 2025 Summer Conference
- [x] Resolving research direction and title structure based on advisor's feedback

### 📌 Notes
During this period, we focused on drafting and refining our conference paper titled:
“Implementation of a Simulation-Based UAV Control Verification System Applying Standard Protocols”
for submission to the KSCI 2025 Summer Conference.

Throughout the writing process, we encountered and resolved several key issues based on the constructive guidance from our academic advisor: 

1. Title Clarity
> “A paper’s title should be nearly half of the abstract.”  
To make the research purpose immediately recognizable, we revised the title to better reflect the central contribution of the work:  
managing UAVs with different protocols via a single GCS.

2. Clarifying Purpose vs. Method  
Our advisor emphasized the importance of distinguishing research purpose from research method:
> “Our core research aim is to manage UAVs with different protocols using a unified GCS.  
The SIL simulation and protocol conversion are tools to achieve that goal — not the goal itself.”

3. Iterative Refinement  
Based on the above feedback, we restructured the abstract and introduction to clearly emphasize the problem of multi-GCS inefficiency and proposed our STANAG-4586–based VSM as the key solution.

### 📄 Paper Summary
- Purpose: Address the inefficiency of requiring multiple GCSs for multiple UAVs by proposing a unified control system using protocol conversion.
- Approach: Develop a VSM (Vehicle Specific Module) for converting between different UAV communication protocols and integrate it into a simulation-based SIL (Software-In-the-Loop) environment using X-Plane and Mission Planner.
- Verification: Demonstrated real-time communication, packet conversion, and stability between simulated UAVs and GCS through visual inspection and data transmission verification.
- Outcome: Validated the feasibility of applying protocol standardization (STANAG-4586) to support diverse UAV control, with future expansion planned toward ground robot integration.

<p><br></p>

---

### 📆 Timeline Notice

To ensure transparency in our development process, the following periods reflect temporary pauses in research activity due to external academic and entrepreneurial events:

<details>
<summary>KSCI-Hackathon</summary>
  <p align="left">
  <img src="https://github.com/user-attachments/assets/ff3647de-16ce-4be9-878d-a8a867414b0b" width="420" height="580" alt="KSCI-Hackathon" >
  <br>
  <em>ㄴ Poster for the 2025 KSCI Hackathon held at Jeju National University.</em>
</details>
- **July 8 – 11, 2025:** Participated in the KSCI-Hackathon and presented at the KSCI 2025 Summer Conference

<p><br></p>

<details>
<summary>SW Startup Camp</summary>
  <p align="left">
  <img src="https://github.com/user-attachments/assets/7893966c-60c3-457f-87a4-1cfcf9589871" width="420" height="580" alt=SW Startup Camp" >
  <br>
  <em>ㄴ Poster for the 2025 SW Startup Camp held at Hyundai Haesang Hi-Vision Center, Gwangju.</em>
</details>
- **July 14 – 18, 2025:** Participated in the SW Startup Camp focused on tech-based startup development

<p><br></p>

---

## July 11, 2025

### 🏅 Publication & Award

**🗓 July 11, 2025 — Best Paper Award**  
We presented our research on a UAV control verification system using simulation with standard protocol application, and received the **Best Paper Award** at the 2025 Summer Conference hosted by the Korean Society of Computer Information (KSCI).

📄 **Paper Title:** *Implementation of a Simulation-Based UAV Control Verification System Applying Standard Protocols*  
🧑‍🔬 **Authors:** SoJeong Kim, SeongGeon Kim, UkGyun Ha (Kyungwoon University)  
🏛 **Hosted by:** Korean Society of Computer Information (KSCI), 2025 Summer Conference
<details>
<summary>🏅 Best Paper Award – KSCI 2025 Summer Conference</summary>
  <p align="left">
  <img src="https://github.com/user-attachments/assets/3cad90ab-c0cc-487e-a3a0-e26faae8d7b6" width="420" height="580" alt="2025_1 우수논문상" >
  <br>
  <em>ㄴ Certificate of Best Paper Award from KSCI, July 2025.</em>
</details>

<p><br></p>

---

## July 29, 2025

### 📝 Todo List (07/29/2025)

- [x] Check the scale of the map in Mission Planner.
- [x] Check whether the aircraft symbol displayed on the map in Mission Planner can be changed to a custom icon.
<!-- Markdown에서 체크박스는 다음과 같이 표시합니다:

[ ] → 아직 안 한 항목 (빈 체크박스)

[x] → 완료한 항목 (체크된 박스) -->


### 📌 Notes
<details>
<summary>🖼️ Mission Planner Map Display</summary>
  <p align="left">
  <!--<img src="https://github.com/user-attachments/assets/68ca9f17-48d9-4877-9081-39726e4cbfd0" width="580" height="500" alt="MP">-->
  <img src="https://github.com/user-attachments/assets/c564c51c-6e7c-4e95-bc4d-441373c15987" width="580" height="420" alt="MP+UBUNTU" >
  <br>
  <em>ㄴ Current aircraft symbol display in Mission Planner.</em>
</details>

Currently, the map in Mission Planner uses the 2025 Google Map.

However, when zoomed in, the image becomes too pixelated to be practical for use with actual robots.  
Therefore, we are considering using **Google Earth** instead.

> 📷 Reference: For an example image, please refer to the 🖼️ Map Display Example section above.

Currently, the map in Mission Planner displays a red aircraft icon.  
However, since our research focuses on ground robots, we wanted to change the icon to one that better suits our application.

We searched through all the image files in both the Mission Planner and ArduPilot directories we downloaded, but couldn't find the relevant icon file.  
As a result, we posted a question on the ArduPilot community site and received help from other users:  
> 👉 [ArduPilot Forum Discussion – Changing Vehicle Icon in Mission Planner](https://discuss.ardupilot.org/t/how-to-change-icon-in-mission-planner-map/137234)

### ✅ Conclusion

We plan to integrate Google Earth to maintain smooth real-time visualization of the vehicle’s movement without data loss, as achieved with the current setup.

Although we followed the guidance from the ArduPilot community and searched for the related icon file, it was not visible in the provided directories.  
Nonetheless, we will continue investigating and aim to successfully locate the icon file in order to replace it with a custom symbol that better fits our ground robot application.

<p><br></p>

---

## August 5, 2025

### 📝 Todo List (08/05/2025)

- [x] Find and edit the file that contains the aircraft icon displayed on the Mission Planner map.

### 📌 Notes

Despite checking all image files (PNG and JPG) within the ArduPilot and Mission Planner folders on the C: drive, I couldn’t find anything that looked like it.  
ㄴ I read through the comments on my question on the ArduPilot Discourse site, but I still couldn't find it, likely because the person who replied is using a different environment:  
> 👉 [ArduPilot Forum Discussion – Changing Vehicle Icon in Mission Planner](https://discuss.ardupilot.org/t/how-to-change-icon-in-mission-planner-map/137234)

### ✅ Conclusions

I found the aircraft image file that needs to be changed on the official ArduPilot Mission Planner GitHub. 
> 👉 [MissionPlanner/Resources/planetracker.png](https://github.com/ArduPilot/MissionPlanner/blob/master/Resources/planetracker.png)

So, in case any related GitHub files were installed on the PC with Mission Planner, I need to replace the image and check if it's correctly reflected on the Mission Planner map.   
=> After confirming that the change is applied, I’ll probably need to link Mission Planner with Google Earth

<p><br></p>

---

## August 9, 2025

### 📝 Todo List (08/06/2025)

- [x] Research on how to change the Mission Planner icon
- [ ] Replace the current 2025 Google Map displayed in the Mission Planner map with a Google Earth integration

### 📌 Notes

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
