MiniMap Note
===
1. Assets Windows → create → Render Texture (***A***)
2. Hierarchy Windows → create → Camera (***B***  for mini-map)
3. set the Target Texture in Camera ***B***'s Component in Inspector with the Render Texture ***A***
4. adjust the Camera ***B***
5. Hierarchy Windows → create → UI → Raw Image (***C***)
6. set the Texture in Raw Image ***C***'s Component in Inspector with the Render Texture ***A***

The graph that Camera ***B*** take will be showed on the Raw Image ***C***

And in this project, I also add some FPS element and conbine my another project **Unity-Socket Test**, just use the **Client.exe** to create enemies.