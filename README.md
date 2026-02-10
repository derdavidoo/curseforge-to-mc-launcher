<img width="200" height="200" alt="logo_square" src="https://github.com/user-attachments/assets/a27d6076-e5f5-44b7-baa4-996d4416d4bf" />


# CurseForge to MC Launcher
[![GitHub release](https://img.shields.io/github/v/release/derdavidoo/curseforge-to-mc-launcher)](https://github.com/derdavidoo/curseforge-to-mc-launcher/releases)
[![Downloads](https://img.shields.io/github/downloads/derdavidoo/curseforge-to-mc-launcher/total)](https://github.com/derdavidoo/curseforge-to-mc-launcher/releases)
[![License](https://img.shields.io/github/license/derdavidoo/curseforge-to-mc-launcher)](LICENSE)
![Platform](https://img.shields.io/badge/platform-Windows-blue)

***A simple tool for transferring game instances from CurseForge to the default Minecraft Launcher***

CurseForge to MC Launcher is a lightweight tool that transfers profiles from CurseForge to the Minecraft Launcher while keeping mods and settings in sync.
It allows you to manage your mods in CurseForge while launching the game faster through the default Minecraft Launcher.

<img width="500" height="278" alt="Unbenannt-1" src="https://github.com/user-attachments/assets/68bf8bd7-bc17-4bc6-9cfd-dca2c98b2e0b" />

## How to use
1. Download the latest release of this tool and run the executable.
2. Click the **Start** button, select all profiles you want to transfer, and run the tool.
3. Open the Minecraft Launcher and enjoy.

**IMPORTANT:** All profiles you want to transfer must be launched at least once through CurseForge before using this tool.

## Troubleshooting
### How to configure directories
For the tool to work correctly, it needs access to both the folder where CurseForge stores its game instances and the folder used by the Minecraft Launcher. These locations are set automatically by default, but they may differ if you changed them manually in the past.

Below is a guide on how to configure these paths correctly if you encounter any issues.

<ins>**CurseForge modding folder location**</ins>
* Launch CurseForge and navigate to the **Minecraft** tab on the left to open the page where all your modpacks are listed.
* Click the three dots in the top-right corner and select **Open Modding Folder**.
  
  <img width="254" height="187" alt="grafik" src="https://github.com/user-attachments/assets/257e6e3a-018c-4be9-a403-f9b7a3bdfa62" />

* Copy the path of this folder and paste it into the **CurseForge Modding Folder Location** field in the settings.

  <img width="918" height="165" alt="grafik" src="https://github.com/user-attachments/assets/28626ae8-db3f-476a-8e68-750e2824f999" />

<ins>**Minecraft folder location**</ins>

This folder usually does not need to be changed, as Microsoft does not allow you to freely choose the installation location of the Minecraft Launcher by default.

You can still locate the correct folder by opening the **Installations** tab in the Minecraft Launcher and clicking the folder icon next to **Latest Release** (assuming you have not manually changed its directory before).

<img width="1004" height="118" alt="grafik" src="https://github.com/user-attachments/assets/0072d86d-51ee-4920-abc2-2eba7991fbdf" />

## Bugs & Issues
This project is still in development and is fully open source. If you encounter any bugs, please report them on the **Issues** page.

If you would like to contribute to this project, feel free to open a pull request.

## Disclaimer
This project is an independent, community-made tool.

It is **not affiliated with, endorsed by, or associated with Mojang Studios, Microsoft, CurseForge, or Overwolf** in any way.

All trademarks and copyrights belong to their respective owners.