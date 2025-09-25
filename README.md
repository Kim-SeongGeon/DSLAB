# DS LAB PROJECT

[![KR](https://img.shields.io/badge/README-한국어-blue)](./README.ko.md)
[![EN](https://img.shields.io/badge/README-English-red)](./README.md)

<img src="https://capsule-render.vercel.app/api?type=waving&color=413fd9&height=150&section=header&text=Mapping%20the%20Future%20of%20Ground%20Robotics!%20🤖&fontSize=35" />

## 🧠 About DS LAB PROJECT
The DS LAB PROJECT is a research initiative aimed at implementing real-time bidirectional communication between multiple UGV (Unmanned Ground Vehicle) and GCS.
This project focuses on designing and controlling communication based on the STANAG 4586 standard protocol, while analyzing communication stability and data loss rates across various simulation environments.

### 🎯 Main Objectives
- Design and implement a STANAG-4586-based UGV communication protocol  
- Enhance real-time mission management and data monitoring through integration with VSM and SCM modules  
- Conduct real-time data exchange experiments between X-Plane and Mission Planner  
- Measure and analyze data loss rates during communication  
- Ensure visualization and stability of the ground robot control system  
- Develop a **Ground Control Station (GCS)** to implement integrated control of multiple UGVs  

### 🧰 Tools
- **Simulation Tools**: X-Plane, Mission Planner
- **Protocol**: STANAG-4586, MAVLink, ROS2

### 🛠 Tech Stack
**Languages**  
![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?style=flat&logo=javascript&logoColor=black)
![Python](https://img.shields.io/badge/Python-3776AB?style=flat&logo=Python&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=flat&logo=c-sharp&logoColor=white)
![HTML](https://img.shields.io/badge/HTML5-E34F26?style=flat&logo=html5&logoColor=white)
![CSS](https://img.shields.io/badge/CSS3-1572B6?style=flat&logo=css3&logoColor=white)

**Frameworks & Libraries**  
![React](https://img.shields.io/badge/React-61DAFB?style=flat&logo=react&logoColor=black)
![Spring Boot](https://img.shields.io/badge/Spring%20Boot-6DB33F?style=flat&logo=springboot&logoColor=white)

**Build & Tools**  
![Gradle](https://img.shields.io/badge/Gradle-02303A?style=flat&logo=gradle&logoColor=white)

**Database**  
![MySQL](https://img.shields.io/badge/MySQL-4479A1?style=flat&logo=mysql&logoColor=white)


<p><br></p>

---

## September 18, 2025

### 📝 To-Do (09/18/2025)

- [X] Designed and populated a test database
- [X] Verified GCS main page integration with database (preliminary test prior to robot communication)

### 📌 Notes

- To validate the data integration structure of the GCS in advance, a test MySQL database was designed and set up, including 50 sample entries.
<img width="420" height="580" alt="DB_25 09 24" src="https://github.com/user-attachments/assets/e16496c1-46a2-4b20-94ed-8ce011d96d49" />
- 📂 Confirmation of test DB structure and sample data insertion

- A follow-up integration test with the GCS main page was successfully conducted, confirming that real-time data display for a multi-robot system (three robots total) is functional.
<img src="https://github.com/user-attachments/assets/c4657a1d-0b61-4818-ac16-87e4244d363c" width="400"/>
- 🔄 Screenshot of GCS displaying real-time data for each robot after DB integration

### ✅ Conclusion

- The integration between the test database and GCS was successfully completed, and real-time retrieval and display of data for multiple robots was verified to be working as intended.
- Visual confirmation is required to ensure that robot positions based on GPS data are correctly reflected on Google Maps, along with the ability to track moving paths in real time.
- The next development phase will focus on building a visualization module for the robot dashboard and implementing intuitive UI components for displaying key real-time metrics such as speed, direction, and sensor data.

<p><br></p>

---

## September 3, 2025

### 📝 To-Do (08/27/2025)

- [X] Completed GCS main page layout
- [X] Prototype layout of Operaion page completed

### 📌 Notes

- Based on the GCS prototype layout created last week, the main page has been completed.
   - → Includes a section that displays real-time robot information in a table format.  

<img width="420" height="580" alt="GCS_25 09 02" src="https://github.com/user-attachments/assets/6bfdd38d-3aeb-4248-9d17-a1c0f2896915" /> - GCS Main Page UI Image

- Further review is needed to determine whether directional information such as azimuth should be included alongside GPS coordinates in the robot information table on the main page.
   - → Considering whether directional data would be visually helpful for users and whether it would complicate the UI.
- A test is planned to check whether real-time data from the MySQL database can be fetched and displayed in each section of the configured main page.
   - → Currently, only the database connection is established; API integration and real-time update logic are still in development.
- Additionally, an Operation page displaying footage from the depth camera mounted on the robot will be designed in line with the overall prototype flow.
   - → Muttiple factors to consider, such as the actual video streaming path, display method (UI), and transition triggers.  

<img width="420" height="580" alt="3차 프로토타입(operation)" src="https://github.com/user-attachments/assets/811032f4-5686-4d2b-9350-3360ca40ed5e" /> - GCS Operation Page Prototype Image

### ✅ Conclusion

- The UI layout of the GCS main page has been completed, and detailed implementation is underway for real-time data integration and display methods.
- A key remaining task is to determine the scalability of the robot information table (GPS + directional data) and to test real-time data display using a MySQL-based system.
- For the Operation page, the implementation will begin by maintaining the existing prototype concept while refining the actual video data flow and its integration with the UI.
- The next phase of development will focus on data integration logic and video streaming tests.

<p><br></p>

---

## August 27, 2025

### 📝 To-Do (08/27/2025)

- [X] Modify the configuration of robot information displayed on the first page of the GCS (object ID, GPS location, speed, battery level, mode, etc.)
- [X] Partially revise the UI structure and data display method of the GCS website
- [X] Decide on the purchase of a new robot

### 📌 Notes

- The first screen of the GCS currently under development is being designed to display key status information of the robot in real time, including object ID, GPS coordinates, current speed, battery level, and operating mode.
- Previously, this information was presented as a simple list of text, but we are planning to switch to a table-based visual layout to improve user-friendliness and readability.
<img width="420" height="580" alt="3차 프로토타입" src="https://github.com/user-attachments/assets/5dcfcd3c-5f2e-4557-a5a0-583abb9535bb" /> 
- Prototype image with modified robot information layout
<img width="420" height="580" alt="수정한 GCS" src="https://github.com/user-attachments/assets/a271854e-8027-4533-95d5-9cd68556f189" />
- Image of the GCS developed so far

- In addition, upon inspecting the previously secured autonomous vehicle, we found partial hardware damage and confirmed that its operating system is not compatible with the ROS2 environment.
- As it was deemed unsuitable for our research and development purposes (such as GCS integration, real-time control, and streaming), we decided to purchase a new robot.

### ✅ Conclusion

- The robot information display on the main screen of the GCS is being improved with a UI that considers both real-time performance and readability.
- The existing autonomous vehicle was deemed incompatible with the direction of our research, leading to the decision to replace it with a new robot.
- The selected robot is optimized for a ROS2-based development environment, which is expected to offer advantages in future testing and scalability.
- Moving forward, we plan to enhance GCS functionality by focusing on data table visualization and real-time streaming of sensor data.

<p><br></p>

---

## August 20, 2025

### 📝 To-Do (08/20/2025)

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

### 📝 To-Do (08/13/2025)

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

