@echo off
title Proyecto 404 – Inicio rápido

:: Verificar Node.js
where node >nul 2>nul
if %errorlevel% neq 0 (
    echo [ERROR] Node.js no está instalado.
    echo Abriendo navegador para descargarlo...
    start https://nodejs.org/en/download/
    echo Una vez instalado, vuelve a ejecutar este archivo.
    pause
    exit /b
)

echo [OK] Node.js encontrado:
node -v
echo.

echo Instalando dependencias...
npm install

echo.
echo Iniciando servidor con Vite...
npm run dev

pause
