call win-route.bat
cd..
cd %FOLDER%
powershell -File build.ps1 -Verbosity Diagnostic -ScriptArgs "-Directory=../test/CoverScript.Simple"
pause