@ECHO OFF
REM This batch file will read a .NET project resource file, locate all the resources, 
REM and find where they are used in the projects associated with the resource

REM The workaround to check for missing parameter (e.g. empty string)
REM is taken from http://www.febooti.com/products/command-line-email/batch-files/dos-batch-file-if-statement.html
REM WARNING: Do not used bracket () inside block IF/FOR statement, even as ECHO parameters, else script will behave unexpectedly.
IF (%1) == () (
	ECHO Missing first parameter: path to the VB.NET source code files, e.g. WindowsApplication1\*.vb
	EXIT /B 1
) ELSE (
	SET SRCPATH=%1
)

IF (%2) == () (
	ECHO Missing 2nd parameter: name of project resource file, e.g. WindowsApplication1\My Project\Resources.resx
	EXIT /B 2
) ELSE (
	SET INPUTFILE=%2
)

IF (%3) == () (
	ECHO Missing 3rd parameter: name of file where all resources' usage are written, e.g. all_resources.txt
	EXIT /B 3
) ELSE (
	SET OUT_ALLTOKENS=%3
)

IF (%4) == () (
	ECHO Missing 4th parameter: name of file where all unused resources are written, e.g. unused_resources.txt
	EXIT /B 4
) ELSE (
	SET OUT_UNUSEDTOKENS=%4
)

REM Check for existence of project resource file
IF NOT EXIST %INPUTFILE% (
	ECHO Unable to open project resource file: %INPUTFILE%
	EXIT /B 5
)

ECHO Analysing %INPUTFILE%. Please wait...

REM Tempory data file. From http://unserializableone.blogspot.com/2009/04/create-unique-temp-filename-with-batch.html
:GETTEMPNAME
set TMPFILE=%TMP%\mytempfile-%RANDOM%-%TIME:~6,5%.tmp
if exist "%TMPFILE%" GOTO :GETTEMPNAME 

REM Delete old output files
IF EXIST %OUT_ALLTOKENS% DEL %OUT_ALLTOKENS%
IF EXIST %OUT_UNUSEDTOKENS% DEL %OUT_UNUSEDTOKENS%

REM Enable delayed environment variable expansion. This is needed for nested FOR/IF to work.
SETLOCAL ENABLEDELAYEDEXPANSION

REM Read the resource fileand extract all resource declaration into a temporary file
findstr /C:"<data name=" %INPUTFILE% > %TMPFILE%

REM Process each line in the temporary file
REM e.g. <data name="msgErrorCallInProgress" xml:space="preserve"> 
REM and retrieve the resource name, e.g. msgErrorCallInProgress

REM Batch script FOR /F does not allow usage of double quotes (") as delimiters.
REM Workaround taken from http://www.computing.net/answers/programming/how-to-specify-quotes-as-delimiter/15313.html
FOR /F "tokens=*" %%a IN (%TMPFILE%) DO (
	SET x=%%a
	SET x=!x:"= !
	FOR /F "tokens=1-3" %%a IN ("!x!") DO (
		REM Output name of resource (%c), which is the 3rd token after splitted by double quotes as delimiters.
		ECHO %%c >> %OUT_ALLTOKENS%
		ECHO. >> %OUT_ALLTOKENS%
		
		REM Search the project folder for reference to the resource name.
		REM including sub-directories (/S). Ignore binary files with non-printable char (/P). Print line number (%P)
		findstr /S /P /N /C:"%%c" %SRCPATH% >> %OUT_ALLTOKENS%			
		
		REM FindStr returns an errorlevel of 0 if the string was found, 1 and above if it was not found.
		IF ERRORLEVEL 1 (
			REM The resource is not not used at all, we write its name to output file
			ECHO %%c >> %OUT_UNUSEDTOKENS%
		)
		
		REM Line separator between tokens
		ECHO. >> %OUT_ALLTOKENS%
	)
)

REM Delete temporary file
DEL /Q %TMPFILE%

ECHO Done analysing %INPUTFILE%.
ECHO.