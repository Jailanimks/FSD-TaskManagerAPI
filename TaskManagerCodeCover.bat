F:\FSDCapsule\TaskManager\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user "-filter:+[TaskManager.Tests]*" "-target:F:\FSDCapsule\TaskManager\packages\NUnit.ConsoleRunner.3.9.0\tools\nunit3-console.exe" "-targetargs: F:\FSDCapsule\TaskManager\TaskManager.Tests\bin\Debug\TaskManager.Tests.dll"

F:\FSDCapsule\TaskManager\packages\ReportGenerator.3.1.0\tools\ReportGenerator.exe "-reports:results.xml" "-targetdir:F:\FSDCapsule\TaskManager\codecoverage"

pause