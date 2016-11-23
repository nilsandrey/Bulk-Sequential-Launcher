setlocal enableextensions
setlocal enabledelayedexpansion
for /f %%a in ('dir /b /on "*.exe"') do (%%a /quiet /passive /norestart)
