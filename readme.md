# FolderLogger - because Windows sucks. 
### After crash or power loss windows file explorer won't restore previous folders after login.
I made it for myself for my needs. It's done 'quick and dirty'.
You are free to download the tool and use it for your private porpuses.
If you modify this program and you want to share it, make a fork.
You have to include link to original repository on the top as shown below:
Oryginal repository: [link]
## How to use (How I use it.)
Put shortcut to the FolderLogger .exe from 
```sh
'FolderLogger\FolderLogger\bin\Debug\netcoreapp3.1'
```
Into your startup directory:
```sh
run -> Win+R
shell:startup
```
That way program will start on windows launch.
It is hidden in the tray by default and saving time is set to: 600000 ms
To hide app to the tray - double click on whitespace.
## Problems I found, but don't bother to fix.
After file explorer.exe crashes, but retains opened folders (I have explorer windows as separate processes) the process magically is loosing list of opened explorer windows (paths), so the log file containing opened explorer windows is saved blank. 
In this case I just stop program close all opened directories by close all, and start program manually than click -> recover.
Or if I'm not using folders, open new ones close others etc. I just go on normally with Folderlogger closed remembering that I need to reboot windows.
Or just reboot windows...
