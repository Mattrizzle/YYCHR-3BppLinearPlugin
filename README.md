# YY-CHR.NET 3Bpp Linear Plugin
<img width="670" height="542" alt="YY-CHR NET 3BPP Linear Plugin" src="https://github.com/user-attachments/assets/1e382755-f9e2-4e20-a435-490c1f4dfcd5" />

Plugin for the .NET versions of YY-CHR to handle the 3bpp linear format used by the rotating Mode 7 background elements of the Iggy, Larry and Reznor battles in Super Mario World.
This is based on the [YY-CHR SNES 8bpp planar plugin by mrehkopf a.k.a. ikari_01](https://github.com/mrehkopf/YYCHR-Snes8BppPlanar) and the [CGA 2bpp linear plugin by gzip](https://github.com/gzip/cs-yychr-plugins), which are derivatives of YY's YY-CHR.NET sample plugin source files.

To compile, place a copy of CharactorLib.dll from YY-CHR into the same directory as YYCHR3BppLinear.cs, open the YYCHR3BppLinear.sln solution file in Visual Studio, then go to the Build menu > Build Solution. Copy the 3BppLinear.dll file from the bin/Debug folder and paste it into YY-CHR's plugins directory. Build tested in Visual Studio Community 2026.
