cls
@echo off
color 0C
echo.
echo     -----------------------------------------------------------------
echo     +                                                               +
echo     +    UBC Badminton Club                                         +
echo     +                                                               +
echo     +              Website:    www.ams.ubc.ca/badminton             +
echo     +              Email:      ubc.badm@gmail.com                   +
echo     +                                                               +
echo     +              Created by: Jason Poon (mr.j.poon@gmail.com)     +
echo     +                                                               +
echo     -----------------------------------------------------------------
echo.
echo     -----------------------------------------------------------------
echo     +                                                               +
echo     +    This batch file will upload all necessary web files to     +
echo     +    the AMS server to update the UBC Badminton Club webpage    +
echo     +                                                               +
echo     -----------------------------------------------------------------
echo.
echo.
pause

cls
echo.
echo     -----------------------------------------------------------------
echo     +                                                               +
echo     +    UBC Badminton Club                                         +
echo     +                                                               +
echo     +              Website:    www.ams.ubc.ca/badminton             +
echo     +              Email:      ubc.badm@gmail.com                   +
echo     +                                                               +
echo     +              Created by: Jason Poon (mr.j.poon@gmail.com)     +
echo     +                                                               +
echo     -----------------------------------------------------------------
echo.
echo.
echo     Total Progress: ÛÛ²²²²²²²²²²²²²²²²²² 10%%
echo.
echo.
echo FTP server login credentials 
set /p username=Username: 

cls
echo.
echo     -----------------------------------------------------------------
echo     +                                                               +
echo     +    UBC Badminton Club                                         +
echo     +                                                               +
echo     +                Website:   www.ams.ubc.ca/badminton            +
echo     +                Email:     ubc.badm@gmail.com                  +
echo     +                                                               +
echo     -----------------------------------------------------------------
echo.
echo.
echo     Total Progress: ÛÛÛÛ²²²²²²²²²²²²²²²² 20%%
echo.
echo.
echo FTP server login credentials 
set /p password=Password: 

cls
echo.
echo     -----------------------------------------------------------------
echo     +                                                               +
echo     +    UBC Badminton Club                                         +
echo     +                                                               +
echo     +                Website:   www.ams.ubc.ca/badminton            +
echo     +                Email:     ubc.badm@gmail.com                  +
echo     +                                                               +
echo     -----------------------------------------------------------------
echo.
echo.
echo     Total Progress: ÛÛÛÛÛÛ²²²²²²²²²²²²²² 30%%
echo.
echo.
echo     Building ftp script...


echo open www.ams.ubc.ca > script.dat
echo %username%>> script.dat
echo %password%>> script.dat
type ftp.txt>> script.dat

cls
echo.
echo     -----------------------------------------------------------------
echo     +                                                               +
echo     +    UBC Badminton Club                                         +
echo     +                                                               +
echo     +                Website:   www.ams.ubc.ca/badminton            +
echo     +                Email:     ubc.badm@gmail.com                  +
echo     +                                                               +
echo     -----------------------------------------------------------------
echo.
echo.
echo     Total Progress: ÛÛÛÛÛÛ²²²²²²²²²²²²²² 50%%
echo.
echo.
echo     Updating AMS web server. This may take awhile...

echo FTP Commands > log.txt
echo --------------- >> log.txt
type script.dat >> log.txt
echo. >> log.txt
echo FTP Log >> log.txt
echo --------------- >> log.txt
ftp -i -s:script.dat >> log.txt

cls
echo.
echo     -----------------------------------------------------------------
echo     +                                                               +
echo     +    UBC Badminton Club                                         +
echo     +                                                               +
echo     +                Website:   www.ams.ubc.ca/badminton            +
echo     +                Email:     ubc.badm@gmail.com                  +
echo     +                                                               +
echo     -----------------------------------------------------------------
echo.
echo.
echo     Total Progress: ÛÛÛÛÛÛÛÛÛÛÛÛÛÛÛÛÛÛÛ² 99%%
echo.
echo.
echo     Done. Logs stored in log.txt.
echo.

del script.dat > nul

pause
