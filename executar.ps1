param([switch]$Testar, [switch]$Compilar)
$ErrorActionPreference = 'Stop'
$localSdk = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../../work/dotnet'))
if (Test-Path (Join-Path $localSdk 'dotnet.exe')) {
    $dotnetApp = Join-Path $localSdk 'dotnet.exe'
    $env:DOTNET_ROOT = $localSdk
    $env:PATH = "$localSdk;$env:PATH"
    $env:DOTNET_CLI_HOME = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../../work/dotnet-home'))
    $env:NUGET_PACKAGES = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../../work/nuget'))
} else {
    $dotnetApp = (Get-Command dotnet -ErrorAction SilentlyContinue).Source
    if (-not $dotnetApp) { throw 'Instale o SDK .NET 10 e a carga maui-windows para executar o projeto.' }
}
$env:DOTNET_CLI_TELEMETRY_OPTOUT = '1'
if ($Testar) {
    & $dotnetApp run --project (Join-Path $PSScriptRoot 'Tests/agenda13.Tests.csproj')
    exit $LASTEXITCODE
}
& $dotnetApp build (Join-Path $PSScriptRoot 'agenda13.csproj') -f net10.0-windows10.0.19041.0 --nologo
if ($LASTEXITCODE -ne 0) { throw 'Não foi possível compilar o App Hotel.' }
if (-not $Compilar) {
    $appFile = Get-ChildItem (Join-Path $PSScriptRoot 'bin/Debug/net10.0-windows10.0.19041.0') -Filter agenda13.exe -Recurse | Select-Object -First 1
    if (-not $appFile) { throw 'Executável não encontrado após a compilação.' }
    Start-Process -FilePath $appFile.FullName -WorkingDirectory $appFile.DirectoryName
}
