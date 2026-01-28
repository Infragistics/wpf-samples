FOR %%i IN (InfragisticsSL4.Controls.Charts.*.dll) DO emm "%cd%\%%i" PING 1.1.1.1 -n 1 -w 1000 >NUL

pause