To run "hello.cs" script in terminal execute the following command:
./cscs.exe ./hello.cs

NOTE: on Ubuntu 11.04 you do not need to specify "mono" at the start of the command line. 


To open it in MonoDevelop pass the script through "debug" parent script
./cscs.exe debug ./hello.cs

Note using css_config.xml is optional but it can be useful if you need to specify additional script/assembly probing ditrectories.
The default css-config.xml can be created by the scrip engine executed with the -noconfig parameter and the reserved file name "out" for the config file: cscs.exe -noconfig:out