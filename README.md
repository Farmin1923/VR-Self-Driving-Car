## VR Based Self-Driving Car


I am developing a VR based self-driving car simulator for using it as a testing bed for predicting pedestrian intention. 
VR simulators are recognized for their affordability, and adaptability in producing a variety of traffic situations, 
and it is more reliable to conduct human-factor research in autonomous cars. 
I have made a multi-player client-server where the car and the pedestrian input can be controlled from different systems.
Also, animated an avatar and write scripting for synchronizing VR-input action with Unity 3D input.

</br>


## Demonstration of the Simulator

<img src="https://user-images.githubusercontent.com/115661274/221333598-c175a97f-a8cc-4a21-ab77-31f36c1547f0.mp4" width="200" />


<img width="430" src="https://user-images.githubusercontent.com/115661274/221333598-c175a97f-a8cc-4a21-ab77-31f36c1547f0.mp4" />
brew install ffmpeg
brew install gifsicle
ffmpeg -i Screen\ Recording\ 2022-07-30\ at\ 16.35.36.mov -vf "fps=30,split[s0][s1];[s0]palettegen[p];[s1][p]paletteuse" output.mp4 && gifsicle -O3 output.mp4 -o output.mp4



## Demonstration of Self-Driving Car



https://user-images.githubusercontent.com/115661274/221333598-c175a97f-a8cc-4a21-ab77-31f36c1547f0.mp4

</br>
</br>


## Camera view of the Client-Server Systems

Egocentric View of the car            |  Egocentric view of the pedestrian
:-------------------------:|:-------------------------:
![Egocentric View of the car](https://user-images.githubusercontent.com/115661274/221333624-dac2659a-2939-4344-91a2-d0097f724cf7.png) |  ![Egocentric View of the pedestrian](https://user-images.githubusercontent.com/115661274/221333627-8ef09546-4d8f-468e-9ce8-9c94ba8782b1.png)


