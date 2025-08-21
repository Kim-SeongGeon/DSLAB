# DS LAB PROJECT

<img src="https://capsule-render.vercel.app/api?type=waving&color=413fd9&height=150&section=header&text=Mapping%20the%20Future%20of%20Ground%20Robotics!%20🤖&fontSize=35" />

## 🧠 About DS LAB PROJECT
The DS LAB PROJECT is a research initiative aimed at implementing real-time bidirectional communication between a UGV (Unmanned Ground Vehicle) and GCS.
This project focuses on designing and controlling communication based on the STANAG 4586 standard protocol, while analyzing communication stability and data loss rates across various simulation environments.

### 🎯 Key Objectives
- Design and implementation of a UGV communication protocol based on STANAG 4586
- Experimental setup for real-time data exchange between X-Plane and Mission Planner
- Measurement and analysis of data loss during communication
- Visualization and stabilization of the ground robot control system

### 🧰 Tools
- Simulation Tools: X-Plane, Mission Planner
- Protocol: STANAG-4586
- Language: Python, C#, JavaScrpit, React, Spring Boot, MySQL

<p><br></p>

---

## August 20, 2025

### 📝 To-Do (2025‑08‑20)

- [X] Reconfigured the GCS prototype to fit the small robot platform environment
- [X] Modified parts of the GCS structure (updated display layout)
- [X] Reviewed the existing autonomous robot OS and evaluated potential new robot platforms for purchase

### 📌 Notes

- The GCS prototype built last week was originally designed for four camera streams (front, rear, left, and right). However, the currently available small robot platform only supports two cameras—front and rear.
- Therefore, the Real Streaming component in the GCS has been adjusted to support only two channels: Front and Back.
<img width="420" height="580" alt="Screenshot 2025-08-21 132109" src="https://github.com/user-attachments/assets/94ce3a06-d757-430d-8dfa-16207ca8c90d" />  
- Latest prototype image reflecting the changes

- Additionally, the GCS display structure was initially planned as a dual-display setup, but for operational efficiency and compatibility with deployment environments, we are transitioning to a single-display layout with tab switching functionality.
> 🔧 Additional system configuration checks:
> - Confirm whether the OS installed on the autonomous robot used last year was based on ROS2
> - Verify if the new small robot platform being considered supports ROS2 builds and provides the necessary interface for the current research objectives    
> → The robot specifications will be organized and shared later    
> https://www.eduino.kr/product/detail.html?product_no=9619&cate_no=181&display_group=1

### ✅ Conclusion

- The GCS prototype has been successfully redesigned for a two-channel streaming setup in line with current hardware conditions
- The display structure is being updated to a tab-switching layout to enhance the user experience
- The GCS is being designed to scale flexibly to four-channel streaming in the future if additional cameras are integrated
- Evaluation of robot platforms is in progress to determine whether to reuse the existing system or adopt a new platform

<p><br></p>

---

## August 13, 2025

### 📝 To-Do (2025‑08‑13)

- [X] Locally built the backend code provided by the participating company
- [X] Verified connection with the frontend after server launch
- [X] Removed audio/video features not compatible with our GCS and updated the frontend accordingly
- [X] Set up the prototype layout for the GCS

### 📌 Notes

- Encountered various issues while building the backend code locally, but most have been resolved:
  - Port conflicts
  - Package dependency errors due to mismatched Node.js versions
  - Missing .env environment variables
  - Database connection failures (due to missing initial config or access permissions)
  - CORS policy conflicts, etc.
- After running the server successfully and checking the frontend, we noticed that the company's system differs from the direction of our Ground Control System (GCS). Therefore, we decided to remove unused audio/video streaming features and plan to revise the UI to better fit our ground control interface.
<img width="420" height="580" alt="참여 기업 측 GCS 화면" src="https://github.com/user-attachments/assets/e2d818a5-5408-4da7-afd7-984692e8efe2" />  
- GCS interface used by the company

- Image assets required for the current GCS prototype have been organized.
<img width="420" height="580" alt="GCS 프로토타입" src="https://github.com/user-attachments/assets/02a135d5-bf99-403e-a5fa-307110aa35a3" />
- 🛰️ GCS Prototype

### ✅ Conclusion

- Most issues with the backend build have been resolved; the server runs successfully in the local environment
- Analyzing the frontend structure is more critical than initially expected; we plan to customize it by focusing only on the necessary features
- The foundation for the GCS prototype has been established → Next steps include gradual improvements in UI/UX design

<p><br></p>

---

## August 7, 2025

### 📝 Todo List (08/07/2025)

- [x] Web-based GCS code analysis
- [x] Prepare equipment for building a small robot

### 📌 Notes

- In today’s meeting with the participating company, we confirmed the successful integration of Mission Planner with X-Plane. Based on this, we received feedback that GCS and robot integration would be feasible in real-world deployment.
- Instead of continuing with the existing Mission Planner-based GCS, we plan to proceed with development using the web-based GCS developed by the participating company.
  - Frontend: **React**
  - Backend: **Spring Boot**
  - Database: **MySQL**
- The actual robot is managed by the company and cannot be easily transported → We will build a small robot in the lab.
  - Setup environment: **Jetson Nano, front/rear/left/right cameras (for Jetson Nano), LTE GPS module, ROS2**
  - The setup will replicate the actual robot environment as closely as possible.
- The GCS will be configured with a dual-display setup (prototype images to be added later).

<p><br></p>

---
## August 6, 2025

### 📝 Todo List (08/06/2025)

- [x] Research on how to change the Mission Planner icon

### 📌 Notes

In the installed version of Mission Planner, the planetracker.png file is included as a resource, but it cannot be accessed directly through a regular file explorer.  
=> This is because planetracker.png is embedded as an internal resource within the Mission Planner executable file.  

In the installed Mission Planner folder, planetracker.png does not exist as a separate file. Instead, it is embedded within MissionPlanner.exe and dynamically loaded during runtime.

### ✅ Conclusions

It seems that the most effective approach would be to modify the Mission Planner source code and build it from scratch.  

1. Clone the full Mission Planner repository from GitHub :
> bash  
> git clone https://github.com/ArduPilot/MissionPlanner.git

2. Replace the Resources/planetracker.png file with the desired image
3. Build the project in Visual Studio (install any required SDKs if necessary)

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

## July 29, 2025

### 📝 Todo List (07/29/2025)

- [x] Check the scale of the map in Mission Planner.
- [x] Check whether the aircraft symbol displayed on the map in Mission Planner can be changed to a custom icon.

### 📌 Notes
<img src="https://github.com/user-attachments/assets/c564c51c-6e7c-4e95-bc4d-441373c15987" width="580" height="420" alt="MP+UBUNTU" >
- Mission Planner's Current Aircraft Icon Display Screen

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

## July 11, 2025

### 🏅 Publication & Award

**🗓 July 11, 2025 — Best Paper Award**  
We presented our research on a UAV control verification system using simulation with standard protocol application, and received the **Best Paper Award** at the 2025 Summer Conference hosted by the Korean Society of Computer Information (KSCI).

📄 **Paper Title:** *Implementation of a Simulation-Based UAV Control Verification System Applying Standard Protocols*  
🧑‍🔬 **Authors:** SoJeong Kim, SeongGeon Kim, UkGyun Ha (Kyungwoon University)  
🏛 **Hosted by:** Korean Society of Computer Information (KSCI), 2025 Summer Conference

<img src="https://github.com/user-attachments/assets/3cad90ab-c0cc-487e-a3a0-e26faae8d7b6" width="420" height="580" alt="2025_1 우수논문상" >
- Certificate of Best Paper Award from KSCI, July 2025

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

## June 20, 2025

### 📝 Todo List (06/20/2025)

- [x] Measurement of data loss rate between X-Plane and Mission Planner communication.

### 📌 Notes
The connection between X-Plane and Mission Planner has been partially successful, and since this is essential content to be included in the paper, it must be measured.  
Therefore, the measurement was based on a flight from Daegu Airport to Incheon Airport.  

<img src="https://github.com/user-attachments/assets/8cfac926-1902-4d20-8c34-0ba0ff40d2fb" width="580" height="420" alt="데이터손실률">
- Graph showing Ping RTT and Data Loss Rate during flight simulation from Daegu to Incheon using X-Plane and Mission Planner

<p><br></p>

---

## June 11, 2025

### 📝 Todo List (06/11/2025)

- [x] Comparison of bidirectional data exchange between X-Plane and Mission Planner.

### 📌 Notes
Currently, the connection between X-Plane and Mission Planner has been successfully established, and upon visual inspection of the simulation, there appear to be no major issues.

### ✅ Conclusion

Although no major issues are observed through visual inspection at present, data loss may occur in areas that are not visible, making it necessary to measure the data loss rate.

<p><br></p>

---

