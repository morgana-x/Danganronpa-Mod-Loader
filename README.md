# Notice
I've been working on a reloaded II mod that emulates Danganronpa WAD Files, at https://github.com/morgana-x/Wad-File-Emulator-ReloadedII

This reloadedII mod allows other mods to replace files within the wads, at runtime without touching the actual archives

This allows pretty much instantaneous loading of mods, and no messing around with physcial files,

I'd probably reccomend using that over this loader now

# Danganronpa-Mod-Loader  ![Downloads](https://img.shields.io/github/downloads/morgana-x/Danganronpa-Mod-Loader/total)
A very basic mod loader for the PC ports of Danganronpa 1 and 2

Currently uses WAD file related code from [DRAT](https://github.com/Liquid-S/Danganronpa-Another-Tool)

Basically place your mods in the mod folder using the file structure described below, run the program, select game and it will automatically create a backup of dr1/2_keyboard_us.wad and patch a copy of it with the mods

After initally loading the mods and closing the game etc, you can edit modConfig.json to change whether they are enabled and their load order


# Usage
The configuration files and mod folders by default regardless of where you run this program and what game for, they will be located in 

**My Documents/Danganronpa2**

There you will find (after initally running the program) a ```mods``` folder, and a ```modConfig.json```

**My Documents/Danganronpa2/mods/**
+ folders named with the mods' id

**Inside the mod folder**
+ ```metaData.json``` file
+ folder called ```wad``` including all files for the mod

the data for the mod in /wad/ ignores specific wad files as it's all chucked in dr2_keyboard_us.wad essentially and works

**Example of a really basic layout for a script mod:**

```My Documents/Danganronpa2/mods/myCoolMod/wad/Dr2/data/us/script/e01_001_100.lin```

# Example/Test mod
Says "MODDING SUCCESS LOL" at start of chapter 1 after the rules are shown 

[examplemod.zip](https://github.com/morgana-x/Danganronpa-Mod-Loader/files/13465657/examplemod.zip)


# Example metadata.json
Note: 

"Game": 0 = Dr1, 1 = Dr2

Gamebanna link does not do anything currently

```json
{
  "Name": "Dungeon Destructive - Danganronpa Badly Translated",
  "Description": "Dungeon Destructive is a mod that translates the dialogue from the first 2 chapters of the game into random languages 10 times in a row to come up with funny results.",
  "Author": "MorsGames",
  "Version": "0",
  "GameBanana": "https://gamebanana.com/mods/50041",
  "Game": 0
}
```

# Example modConfig.json
```json
{
  "gamePath": {
    "Dr1": "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Danganronpa Trigger Happy Havoc",
    "Dr2": "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Danganronpa 2 Goodbye Despair"
  },
  "modsPath": "C:\\Users\\USERNAMEHERE\\Documents\\My Games\\Danganronpa2\\mods",
  "modLoadOrder": {
    "badlytranslated": 1,
    "dr1.dungeondestructive": 2,
    "ilovenagito": 3,
    "testmod": 4
  },
  "modEnabled": {
    "badlytranslated": true,
    "dr1.dungeondestructive": true,
    "ilovenagito": true,
    "testmod": true
  }
}
```

