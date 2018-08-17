@echo off
powershell -ExecutionPolicy Unrestricted ./build.ps1 --settings_skipverification=true %CAKE_ARGS% %*
