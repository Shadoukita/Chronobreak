# Setup guide
* Install .NET 8 or newer ([link](https://dotnet.microsoft.com/en-us/download))
		
### Manual Setup (Windows/Mac)
* Install Microsoft Visual Studio 2019 or newer (Community Edition is fine)
* Open the GameServer Solution in VS, set the platform to x86, build and run.

### Manual Setup (Linux)
* Build the server by running ```dotnet build .```
* Enter the output directory by running ```cd GameServerConsole/bin/Debug/net8.0/```
* Start the server: ```./GameServerConsole```

# Running the game client
* Download the 4.20 version of League game client.

#### Automatically Launching from Visual Studio or GameServerConsole.exe
Click the debug button.
* Open ```GameServer/GameServerConsole/bin/Debug/net8.0/Settings/GameServerSettings.json```
* Change ```"autoStartClient": false``` to ```true```
* Set the path to your League of Legends' deploy folder, which shown by the example already in the file.

#### Manually Launching from command line
```
cd "Path/To/Your/League420/RADS/solutions/lol_game_client_sln/releases/0.0.1.68/deploy/"
start "" "League of Legends.exe" "" "" "" "127.0.0.1 5119 17BLOhi6KZsTtldTsizvHg== 1"
```

#### Manually Launching from command line (Linux)
* Install wine and winetricks using your package manager.
* Run ```winetricks d3dx9``` - without this you will get into the game, but your screen will be black.
* Enter the directory containing the client by running ```cd /path/to/your/League-of-Legends-4-20/RADS/solutions/lol_game_client_sln/releases/0.0.1.68/deploy/```
* Run the game:

```
wine "./League of Legends.exe" "" "" "" "127.0.0.1 5119 17BLOhi6KZsTtldTsizvHg== 1"
```

# License

This repository is under the [AGPL-3.0](LICENSE) license.
This essentially means that all changes that are made on top of this repository are required to be made public, regardless of where the code is being ran.
# Chronobreak
