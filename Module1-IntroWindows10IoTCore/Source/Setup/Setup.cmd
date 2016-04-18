@ECHO off
%~d0

CD "%~dp0"
ECHO Install Visual Studio 2015 Code Snippets for the module: Intro to Windows 10 IoT Core
ECHO -------------------------------------------------------------------------------------
CALL .\Scripts\InstallCodeSnippets.cmd
ECHO Done!
ECHO.
ECHO *******************************************************************************
ECHO.

@PAUSE
