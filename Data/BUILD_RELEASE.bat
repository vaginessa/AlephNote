@ECHO OFF
@REM ================ BUILD ================

cd ..

del AlephNote.zip

cd Source
nuget restore


echo "Please build RELEASE"
PAUSE
REM dotnet msbuild /t:Clean /p:Configuration=Release /verbosity:m
REM dotnet msbuild /t:Build /p:Configuration=Release /verbosity:m

@REM ================ CLEAN ================

cd ..
cd Bin
cd Release
cd net46

rmdir de /S /Q
rmdir es /S /Q
rmdir fr /S /Q
rmdir hu /S /Q
rmdir it /S /Q
rmdir pt-BR /S /Q
rmdir ro /S /Q
rmdir ru /S /Q
rmdir sv /S /Q
rmdir zh-Hans /S /Q

del /q *.pdb
del /q *.vshost.exe
del /q *.manifest
del /q *.xml
del /q *.config
if exist .notes rd /S /Q .notes
cd Plugins
del /q *.pdb
del /q *.vshost.exe
del /q *.manifest
del /q *.xml
cd ..

@REM ================ PACKAGE ================

cd ..
cd ..
cd ..

if exist AlephNote.zip del AlephNote.zip

cd Data

7za.exe a .\..\AlephNote.zip .\..\Bin\Release\net46\*

@REM ================ FINISHED ================

echo "Finished successfully"
PAUSE

