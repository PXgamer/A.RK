REM set msBuildDir=%WINDIR%\Microsoft.NET\Framework\v4.5
set msBuildDir=%WINDIR%\Microsoft.NET\Framework\v4.0.30319
call %msBuildDir%\msbuild.exe  CLI.sln /p:Configuration=Debug /l:FileLogger,Microsoft.Build.Engine;logfile=Buildlog.log
set msBuildDir=