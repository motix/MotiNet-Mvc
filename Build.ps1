# Taken from psake https://github.com/psake/psake

<#
.SYNOPSIS
  This is a helper function that runs a scriptblock and checks the PS variable $lastexitcode
  to see if an error occcured. If an error is detected then an exception is thrown.
  This function allows you to run command-line programs without having to
  explicitly check the $lastexitcode variable.
.EXAMPLE
  exec { svn info $repository_trunk } "Error executing SVN. Please verify SVN command-line client is installed"
#>
function Exec
{
    [CmdletBinding()]
    param(
        [Parameter(Position=0,Mandatory=1)][scriptblock]$cmd,
        [Parameter(Position=1,Mandatory=0)][string]$errorMessage = ($msgs.error_bad_command -f $cmd)
    )
    & $cmd
    if ($lastexitcode -ne 0) {
        throw ("Exec: " + $errorMessage)
    }
}

if(Test-Path .\artifacts) { Remove-Item .\artifacts -Force -Recurse }

exec { & dotnet restore }

# exec { & dotnet test .\test\MotiNet.Core.Tests -c Release }

if ($env:APPVEYOR_REPO_TAG -eq $true) {
	exec { & dotnet pack .\src\MotiNet.AspNetCore.Mvc -c Release -o ..\..\artifacts }
	exec { & dotnet pack .\src\MotiNet.AspNetCore.Mvc.SharedStrings -c Release -o ..\..\artifacts }
	exec { & dotnet pack .\src\MotiNet.AspNetCore.Mvc.RazorPagesViews -c Release -o ..\..\artifacts }
	exec { & dotnet pack .\src\MotiNet.AspNetCore.Mvc.MvcViews -c Release -o ..\..\artifacts }
	exec { & dotnet pack .\src\MotiNet.AspNetCore.Compozr -c Release -o ..\..\artifacts }
	exec { & dotnet pack .\src\MotiNet.AspNetCore.Compozr.SharedStrings -c Release -o ..\..\artifacts }
	exec { & dotnet pack .\src\MotiNet.AspNetCore.Compozr.RazorPagesViews -c Release -o ..\..\artifacts }
	exec { & dotnet pack .\src\MotiNet.AspNetCore.Compozr.MvcViews -c Release -o ..\..\artifacts }
	exec { & dotnet pack .\src\MotiNet.AspNetCore.Angular -c Release -o ..\..\artifacts }
	exec { & dotnet pack .\src\aspnet\MotiNet.AspNetCore.Mvc.DataAnnotations -c Release -o ..\..\..\artifacts }
} else {
	$revision = @{ $true = $env:APPVEYOR_BUILD_NUMBER; $false = 1 }[$env:APPVEYOR_BUILD_NUMBER -ne $NULL];
	$revision = "{0:D4}" -f [convert]::ToInt32($revision, 10);
	$suffix = "beta-" + $revision

	exec { & dotnet pack .\src\MotiNet.AspNetCore.Mvc -c Release -o ..\..\artifacts --version-suffix=$suffix }
	exec { & dotnet pack .\src\MotiNet.AspNetCore.Mvc.SharedStrings -c Release -o ..\..\artifacts --version-suffix=$suffix }
	exec { & dotnet pack .\src\MotiNet.AspNetCore.Mvc.RazorPagesViews -c Release -o ..\..\artifacts --version-suffix=$suffix }
	exec { & dotnet pack .\src\MotiNet.AspNetCore.Mvc.MvcViews -c Release -o ..\..\artifacts --version-suffix=$suffix }
	exec { & dotnet pack .\src\MotiNet.AspNetCore.Compozr -c Release -o ..\..\artifacts --version-suffix=$suffix }
	exec { & dotnet pack .\src\MotiNet.AspNetCore.Compozr.SharedStrings -c Release -o ..\..\artifacts --version-suffix=$suffix }
	exec { & dotnet pack .\src\MotiNet.AspNetCore.Compozr.RazorPagesViews -c Release -o ..\..\artifacts --version-suffix=$suffix }
	exec { & dotnet pack .\src\MotiNet.AspNetCore.Compozr.MvcViews -c Release -o ..\..\artifacts --version-suffix=$suffix }
	exec { & dotnet pack .\src\MotiNet.AspNetCore.Angular -c Release -o ..\..\artifacts --version-suffix=$suffix }
	exec { & dotnet pack .\src\aspnet\MotiNet.AspNetCore.Mvc.DataAnnotations -c Release -o ..\..\..\artifacts --version-suffix=$suffix }
}
