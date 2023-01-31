The following ReadMe data details how to build a game
executable and what needs to be included for distribution.

** Building the Executable **
You will need to have Unity running to build an executable.
With the Editor opened up, click "file" from the top ribbon
and then select "Build Settings".
The "Scenes in Build" at the top are the scenes that are included
in the Executable game. As of 2022, you should have all Pseudo, Python and
CSharp scenes and their associated Levels checkmarked.
The build options for the game should be "Target Windows" and 
"Intel 64-bit Architecture". All other boxes should be un-checked and
settings are at their default.
Once all this is setup, click the "build" button and select the folder
you want the executable and scene files to go into. Preferably you want
this folder to be empty, and typically you would select a location on
a flashdrive of some sort.

** Necessary Includes **
To share this game with another person, we need to
have three things included with the build.
Luckily for you, all three of these items are included
in our project and all ready to go.
You will find all three of these items located in this same folder
where you found this ReadMe.txt

1. The first item you want to include in the build is the Manual.
Just copy and paste this in the root of the folder (where Robot and I.exe is).

2. The second item you want to include in the build are the licenses.
The Python and CSharp compilers come with licenses stating free distribution,
copying and modification. The catch is that the license is distributed along with
the game. The licenses are in a folder named "Licenses". To the best of my knowledge,
all Licenses are in this folder and appropriately named. Also, to the best of my
knowledge you should be able to just copy and paste this folder into the root of the
executable folder (where Robot and I.exe is)

3. The final item you want to include in the build is critical if you want the game
to work properly. The CSharp compiler requires a mono.exe and mcs.exe file in order
to run code at runtime. Along with the executables, there are several dependencies that need
to be included. All this is included in the Mono folder. The placement of this
folder is critical, due to the compiler path being "hard-coded" in (it is free form based
on directory the executable is in, but is hard-coded to the executables location).
I have made this really easy for you though. You must place the directory "Mono" at the
root of the executables directory. (Yes, this folder as well goes right next to Robot and I.exe).

Once all the above has been done, you may distribute the game freely to anyone who wishes to play
the game and give us feedback for improvement.