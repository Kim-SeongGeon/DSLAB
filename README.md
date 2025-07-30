# DS LAB PROJECT

<img src="https://capsule-render.vercel.app/api?type=waving&color=413fd9&height=150&section=header&text=Mapping%20the%20Future%20of%20Ground%20Robotics🤖&fontSize=35" />

Record our DS LAB PROJECT✍️

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
  <em>Figure 1. Current aircraft symbol display in Mission Planner.</em>
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

---

