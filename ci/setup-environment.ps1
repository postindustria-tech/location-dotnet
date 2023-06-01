param(
    [Parameter(Mandatory=$true)]
    [string]$RepoName,
    [string]$ProjectDir = ".",
    [string]$Name = "Release_x64",
    [string]$Arch = "x64",
    [string]$Configuration = "Release",
    [hashtable]$Keys
)

Write-Output "Setting SUPER_RESOURCE_KEY "
$env:SUPER_RESOURCE_KEY = $Keys.TestResourceKey

