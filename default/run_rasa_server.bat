@echo off

REM Command to run "rasa run actions" in one terminal
start "" cmd /k rasa run actions

REM Add a small delay to allow the first terminal to start. Change delay as required
timeout /t 5

REM Command to run "rasa run" in another terminal
start "" cmd /k rasa run -m 20230719-171951-excited-screen.tar.gz --enable-api --cors "*"
