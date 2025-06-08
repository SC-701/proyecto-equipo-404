@echo off 
where node >nul 2>nul 
 
if %0%==0 ( 
    echo Node.js ya est  instalado. 
    node -v 
) else ( 
    echo Node.js no est  instalado. 
    echo Abriendo el navegador para descargarlo... 
    start https://nodejs.org/en/download/ 
) 
 
pause 
