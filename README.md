# Lissajous-Anim-Unity


*********README***********

Project Overview:
This Unity project showcases procedural 3D object creation, animation, and interaction. The main features include:
- Procedural mesh creation for Object A (a sphere with a cone in front).
- Lissajous curve animations for both objects to create dynamic, aperiodic motion.
- Object rotation logic for Object A to always face Object B.
- Color transitions for Object A based on its angular position relative to Object B.
- Vertex animation using Perlin noise to create organic mesh deformation.
- AR/VR integration for Meta Quest with hand-tracking support for interaction (Meta XR Building Blocks).

![Demo of Project](./assets/demo.gif)

Setup Instructions:

Requirements:
- Unity 2023.3 or later (URP setup is required).
- Meta Quest 2 or 3 headset.
- XR Interaction Toolkit and Oculus Integration packages (installed via Unity Package Manager).

Steps to Set Up the Project
- Clone the repository from GitHub: git clone https://github.com/Nichojay729/Lissajous-Anim-Unity.git
- Open the project in Unity 2023.3 or later.
- Ensure the Universal Render Pipeline (URP) is correctly set up:
- Go to Edit > Project Settings > Graphics and assign the URP asset.
- Adjust URP settings for optimized performance on Meta Quest.
- Verify that the XR Plug-in Management is enabled:
- Go to Edit > Project Settings > XR Plug-in Management.
- Enable the Oculus Plug-in for Meta Quest.
- Connect your Meta Quest headset:
- Use Oculus Link or Air Link for testing.
- Build and deploy the project using Build and Run.
- Test the project:
- Verify animations, interactions, and Passthrough AR/VR features.

Description of the Implementation
Procedural Mesh Creation
- Object A is procedurally generated using a sphere for the body and a cone for the front.
- The mesh is assigned dynamically to a MeshFilter component.

Lissajous Curve Animations
- Both Object A and Object B follow Lissajous curve paths with distinct parameters to create aperiodic motion.
- Object Rotation and Color Change
- Object A continuously rotates to face Object B.
- The color of Object A transitions from red to blue based on the angle between its forward vector and the direction to Object B.
- Vertex Animation
- Object A's vertices are displaced using Perlin noise to simulate organic deformation.
- AR/VR Integration
- Objects are anchored to the user's view in Passthrough AR.
- Right hand is an attractor for Object A, while the left hand is an attractor for Object B.
- Interactions are handled using the XR Interaction Toolkit and Oculus APIs.

Thank you for reviewing this project!
