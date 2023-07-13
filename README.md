# TestTwoPerson
# Introduction

This project is part of my doctoral thesis research. Two users in a co-located environment can share their gaze in real-time.

## Use Flow

1. Print a QR code before starting to use. Place the printed QR code in the workspace of collaboration.
2. Two users wear the Hololens and launch this application. Sometimes the HoloLens will request calibration of users' gaze, if so, please follow the guidance of HoloLens.
3. Two users wear their HoloLens and look at the printed QR code in step 1. By scanning the QR code, our system will synchronize two coordinates of users' AR environments.
4. Users can see each other's gaze point and the gaze ray of partner user. We use different color to illustrate different user.

# Development Environment 

- Unity 2020.4
- MRTK 2.8.3

- [x]  Visualization of gaze
    - [x]  Gaze point of partner
    - [x]  Gaze ray of partner
    - [x]  Gaze point of self
    - [x]  Do not implement Gaze ray of self because of collision
- [ ]  Visualization of gaze behaviors
    - [ ]  Visualization of fixations and saccades
        - [x]  fixations indicator: color red → green
        - [x]  trajectory of saccades: transparent 100 → 0
        - [x]  conclusion of interest: review the color
- [ ]  Synchronization
    - [x]  Coordinate synchronization


- [ ]  Debug
    - [ ]  Visualization of gaze behavior will cause HoloLens application crash   
