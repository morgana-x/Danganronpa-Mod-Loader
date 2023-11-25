# Danganronpa-Mod-Loader
A very basic but very "effective" mod loader for the PC ports of Danganronpa 1 and 2

Currently uses WAD file related code from [DRAT](https://github.com/Liquid-S/Danganronpa-Another-Tool)

(To be changed)
# Usage
The configuration files and mod folders by default regardless of where you run this program and what game for, they will be located in 

**My Documents/Danganronpa2**

There you will find (after initally running the program) a **mods** folder, and a **modConfig.json**

**My Documents/Danganronpa2/mods/**
+ folders named with the mods' id

**Inside the mod folder**
+ metaData.json file
+ folder called "wad" including all files for the mod

the data for the mod in /wad/ ignores specific wad files as it's all chucked in dr2_keyboard_us.wad essentially and works

**Example of a really basic layout for a script mod:**
My Documents/Danganronpa2/mods/myCoolMod/wad/Dr2/data/us/script/e01_001_100.lin


# Example metadata.json
Note: 

"Game": 0 = Dr1, 1 = Dr2

```json
{
  "Name": "Dungeon Destructive - Danganronpa Badly Translated",
  "Description": "Dungeon Destructive is a mod that translates the dialogue from the first 2 chapters of the game into random languages 10 times in a row to come up with funny results.",
  "Author": "MorsGames",
  "Version": "0",
  "GameBanana": "https://gamebanana.com/mods/50041",
  "Game": 0
}

