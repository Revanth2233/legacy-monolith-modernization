<#
.SYNOPSIS
    Deploy Legacy Monolith Modernization to IIS / Azure App Service.
.DESCRIPTION
    Builds, tests, publishes and optionally deploys the application.
.PARAMETER Environment
    Target environment: Development | Staging | Production
.PARAMETER SkipTests
    Skip unit tests (not recommended for Production).
#>
param(
    [Parameter(Mandatory)]
    [ValidateSet("Development","Staging","Production")]
    [string]$Environment,
    [switch]$SkipTests
)

$ErrorActionPreference = "Stop"
$ProjectPath = Join-Path $PSScriptRoot "..\src\LegacyModern.csproj"
$PublishDir  = Join-Path $PSScriptRoot "..\publish"

function Write-Step([string]$msg) { Write-Host "`n==> $msg" -ForegroundColor Cyan }

# Step 1: Restore
Write-Step "Restoring packages..."
dotnet restore $ProjectPath

# Step 2: Build
Write-Step "Building ($Environment)..."
dotnet build $ProjectPath -c Release --no-restore

# Step 3: Test (optional skip)
if (-not $SkipTests) {
    Write-Step "Running tests..."
    dotnet test (Join-Path $PSScriptRoot "..\tests") --configuration Release --no-build
}

# Step 4: Publish
Write-Step "Publishing to $PublishDir..."
dotnet publish $ProjectPath -c Release -o $PublishDir --no-build

# Step 5: Environment-specific deployment
Write-Step "Deploying to $Environment..."
switch ($Environment) {
    "Development" {
        Write-Host "Dev deploy: files in $PublishDir are ready." -ForegroundColor Green
    }
    "Staging" {
        # Example: Robocopy to staging share
        # robocopy $PublishDir "\\staging-server\apps\legacy-modern" /MIR /XO
        Write-Host "Staging deploy complete (add robocopy command)." -ForegroundColor Yellow
    }
    "Production" {
        Write-Host "Production deploy — ensure change ticket is approved!" -ForegroundColor Red
        # az webapp deployment source config-zip -g RG -n AppName --src deploy.zip
        Write-Host "Add Azure CLI deploy command here." -ForegroundColor Yellow
    }
}

Write-Host "`nDeployment to $Environment complete." -ForegroundColor Green
