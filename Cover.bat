REM Create a 'GeneratedReports' folder if it does not exist
if not exist "%~dp0GeneratedReports" mkdir "%~dp0GeneratedReports"
 
REM Remove any previous test execution files to prevent issues overwriting
IF EXIST "%~dp0TaskManagerService.trx" del "%~dp0TaskManagerService.trx%"
 
REM Remove any previously created test output directories
CD %~dp0
FOR /D /R %%X IN (%USERNAME%*) DO RD /S /Q "%%X"
 
REM Run the tests against the targeted output
call :RunOpenCoverUnitTestMetrics
 
REM Generate the report output based on the test results
if %errorlevel% equ 0 (
 call :RunReportGeneratorOutput
)
 
REM Launch the report
if %errorlevel% equ 0 (
 call :RunLaunchReport
)

 
:RunOpenCoverUnitTestMetrics
"F:\FSDCapsule\TaskManager\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe" ^
-register:user ^
-target:"%VS120COMNTOOLS%\..\IDE\mstest.exe" ^
-targetargs:"/testcontainer:\"%~dp0..\TaskManager.Tests\bin\Debug\TaskManager.Tests.dll\" /resultsfile:\"%~dp0TaskManagerService.trx\"" ^
-filter:"+[TaskManagerService*]* -[TaskManager.Tests]* -[*]TaskManagerService.RouteConfig" ^
-mergebyhash ^
-skipautoprops ^
-output:"%~dp0\GeneratedReports\TaskManagerServiceReport.xml"

 
:RunReportGeneratorOutput
"F:\FSDCapsule\TaskManager\packages\ReportGenerator.3.1.0\tools\ReportGenerator.exe" ^
-reports:"%~dp0\GeneratedReports\TaskManagerServiceReport.xml" ^
-targetdir:"%~dp0\GeneratedReports\ReportGenerator Output"

:RunLaunchReport
start "report" "%~dp0\GeneratedReports\ReportGenerator Output\index.htm"
pause
