@echo off

FOR %%i IN (InfragisticsSL4.*.11.2.dll) DO call generate.xtmap.xml.bat "%cd%\%%i"

pause