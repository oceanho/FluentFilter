#
# NugetPackTask.ps1
#

$NugetKey="$ENV:NUGETAPIKEY"
$NugetSvr="https://www.nuget.org/api/v2/package"

$NugetPack="../../packs"
$CsProject="../../src/OhDotNetLib/OhDotNetLib.csproj"

Function BuildProj($Configuration="Debug"){
	Invoke-Expression "dotnet restore $CsProject"
	Invoke-Expression "dotnet build -c $Configuration  $CsProject"
}

Function CreateBetaPack {
	ClearNugetPack
	BuildProj -Configuration "Debug"
	Invoke-Expression "dotnet pack --output $NugetPack -c Debug --include-symbols --version-suffix '-Beta' $CsProject"
	CleanNonSymbolsPack
}

Function CreateAlphaPack {
	ClearNugetPack
	BuildProj -Configuration "Debug"
	Invoke-Expression "dotnet pack --output $NugetPack --include-symbols --version-suffix '-Alpha' $CsProject"
	CleanNonSymbolsPack
}

Function CreateReleasePack {
	ClearNugetPack
	BuildProj -Configuration "Release"
	Invoke-Expression "dotnet pack --output $NugetPack -c Release  $CsProject"
}

Function ClearNugetPack {
	If (Test-Path $NugetPack){
		Remove-Item "$NugetPack/*.nupkg" > $null
	}
}

Function PublishNugetPack {
	If ($NugetKey -eq ""){
		Write-Warning -Message "Nuget.org Api Key not set.Your need to set an Key of NUGETAPIKEY system enveriment veriable"
		Return
	}
	If ((Test-Path -PathType Leaf "$NugetPack/*.nupkg") -eq $false){
		Write-Error -Message "Your should be build nuget pack first by Create[Beta/Alpha/Release]Pack function"
		Return
	}
	Get-ChildItem "$NugetPack/*.nupkg" | ForEach-Object -Process {
		Invoke-Expression "dotnet nuget push $_ -s $NugetSvr -k $NugetKey"
	}
}

Function CleanNonSymbolsPack {
	If (Test-Path $NugetPack){
		Get-ChildItem "$NugetPack/*.nupkg" | ForEach-Object -Process {
			If(!($_.Name.EndsWith(".symbols.nupkg"))){
				Remove-Item $_.FullName > $null
			}
		}
	}
}

Function SetNugetApiKey {
	$api_key = Read-Host "Input Your nuget Api Key(Empty Exited)"
    If([System.String]::IsNullOrEmpty($api_key) -ne $true){
        $NugetKey = $api_key
        [System.Environment]::SetEnvironmentVariable("NUGETAPIKEY",$api_key,[System.EnvironmentVariableTarget]::Machine)
        Return
    }
    Write-Warning -Message "The api key is null . nothing to do. "
}