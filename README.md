# Danganronpa-Mod-Loader
A very basic but very "effective" mod loader for the PC ports of Danganronpa 1 and 2

Currently uses WAD file related code from [DRAT](https://github.com/Liquid-S/Danganronpa-Another-Tool)

(To be changed)
# Usage
The configuration files and mod folders by default regardless of where you run this program and what game for, they will be located in 

**My Documents/Danganronpa2**

There you will find (after initally running the program) a **mods** folder, and a **modConfig.json**

inside the mods folder a mod's (current and probably going to change) format is:

A folder with the mod's name/id

a metaData.json file inside that folder

Then the data for the mod in /wad/ (which ignores specific wad files as it's all chucked in dr2_keyboard_us.wad essentially and works)

Example of a really basic layout for a script mod:

My Documents/Danganronpa2/mods/myCoolMod/wad/Dr2/data/us/script/e01_001_100.lin
