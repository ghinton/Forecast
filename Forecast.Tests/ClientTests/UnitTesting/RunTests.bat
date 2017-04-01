@ECHO OFF
REM "Type for /i for full infomration on various symbols that can be used in the prompt - use dp0 for drive path"
REM "http://stackoverflow.com/questions/5034076/what-does-dp0-mean-and-how-does-it-work"

SET BASE_DIR=%~dp0
karma start "%BASE_DIR%\Config\karma.conf.js"