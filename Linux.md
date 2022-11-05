
# MeindosMod (or other mods) on Linux<br>

A guide for running Among Us (Steam Edition) with mods on linux.<br>

## IMPORTANT
To run any mod on any version using BepInEx, you should add `WINEDLLOVERRIDES="winhttp=n,b"` to the custom launch command in Steam.<br>
Otherwise it WONT work at all.<BR>

## Note about Among Us 2022.10.25<br>

Since the release of 2022.10.25, BepInEx, the framework used for injecting code and modifying the game, switched over to CoreCLR instead of mono.<br>
Because of this change, Among Us wont launch with normal proton over Steam.<br>
It wont launch because Proton, the compatibility tool used by steam, hasn't implemented the fix for this issue, which HAS been fixed in newer wine versions.<br>

## 2022.10.25 and up

To run MeindosMod  on this version you need a custom proton version, until the official one is patched.<br>
You can get this custom proton from [here](https://mini.duikbo.at/proton-7.0-4-bepinex.tar.xz) precompiled (Thanks Miniduikboot), or compile his [branch](https://github.com/miniduikboot/wine/tree/fix/bepinex_coreclr_i386) by yourself.<br>
For a bit more information from miniduikboot himself, please check this [issue](https://github.com/NuclearPowered/Reactor/issues/66) (Again thanks a lot Miniduikboot).<br><br>
Once you have downloaded the custom version you need to install it.<br>
1. To install the custom proton version, first create a ``~/.steam/root/compatibilitytools.d`` directory if it doesnt exist.<br><br>
2. Then extract the archive to that direcory ``tar -xf proton-7.0-4-bepinex.tar.xz -C ~/.steam/root/compatibilitytools.d/``<br><br>
3. Restart steam<br><br>
4. Enable the custom proton version in game settings<br>

To enable a custom proton version follow these steps<br>
1. Right click any game in Steam and click Properties.<br>
2. At the bottom of the `Compatibility` tab, Check `Force the use of a specific Steam Play compatibility tool`, then select proton-7.0-4-bepinex.<br>
It should now look something like this ![compatibility](compat.png)

### After installing custom proton

You should download the latest [BepInEx bleeding edge](https://builds.bepinex.dev/projects/bepinex_be).<br><br>
Then download the appropriate [Reactor](https://github.com/NuclearPowered/Reactor/releases/tag/2.0.0) mod and [MeindosMod](https://github.com/Meindo/MeindosMod).<br><br>
Once those files have been acquired, you need to extract the BepInEx file to the root of the game directory. (You can find by going into steam and clicking browse local files).<br><br>
At last move the 2 dll's from the mods into the `BepInEx/plugins` folder<br><br>

## 2022.8.23 and Under

Generally the specific mod has instructions on its readme or release notes, please refer to that for now (Since this section is a WIP).<br>

## Credits

- [MeindoMC](https://github.com/meindo), myself for this guide
- [Miniduikboot](https://github.com/miniduikboot) for the fixed proton and assisting on the issue.
- [The proton team](https://github.com/ValveSoftware/Proton/graphs/contributors), for proton itself. Indirectly also the [wine team](https://github.com/ValveSoftware/wine/graphs/contributors).
- The [BepInEx team]()