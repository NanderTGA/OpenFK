@echo off
echo Welcome to OpenFK OCX fetcher!
echo This version downloads the 32-bit and 64-bit OCXs
echo Original written by GittyMac
echo ------------------------------
for /f "delims=" %%a in ('powershell $PSVersionTable.PSVersion.Major') do set "var=%%a"
if %var% lss 3 @echo Your Powershell version is too old to fetch the OCX. Update Powershell to version 3 or higher. && pause && exit

echo Downloading data...
mkdir tempdl
cd tempdl
powershell -Command "$ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest http://download.windowsupdate.com/c/msdownload/update/software/secu/2019/06/windows10.0-kb4503308-x64_b6478017674279c8ba4f06e60fc3bab04ed7ae02.msu -OutFile update.msu"
echo Extracting MSU...
expand update.msu -f:Windows10.0-KB4503308-x64.cab ./
echo Extracting CAB...
expand Windows10.0-KB4503308-x64.cab -f:flash.ocx ./

echo Fetching OCX...
echo Grabbing 64-bit OCX...
copy "%cd%\amd64_adobe-flash-for-windows_31bf3856ad364e35_10.0.18362.172_none_815470a5fb446c4e\flash.ocx" "..\Flash64.ocx"
echo Grabbing 32-bit OCX...
copy "%cd%\wow64_adobe-flash-for-windows_31bf3856ad364e35_10.0.18362.172_none_8ba91af82fa52e49\flash.ocx" "..\Flash32.ocx"

cd ..
rmdir /s /q tempdl
echo Your Flash.OCX is served!
