# Build (Optional)

First, install BepInEx 5 and create a VS project using their guide. I used TPM netstandard2.1 and Unity version 2022.17.3 (https://docs.bepinex.dev/articles/dev_guide/plugin_tutorial/index.html)  
Next, create a blank BepInEx project with those parameters. (https://docs.bepinex.dev/articles/dev_guide/plugin_tutorial/2_plugin_start.html)  
Then add project references for UnityCore UnityCoreUI and the C# Assembly Dll from the game. (You may not need the UI Dll but better safe than sorry)  
Next hit build and you'll have your DLL.  


# Install
Download or Build your Dll  
Install BepInEx into your game and the DLL to the plugin folder. (https://docs.bepinex.dev/articles/user_guide/installation/index.html)  
Configs for the mod will automatically be generated after starting the game once.  
