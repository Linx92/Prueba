# Parameters - Solution
$SolutionFileName = "TestApi.sln"
$CoverageCoverletDir = ".coverage"
$CoverageReportDir = ".coverage-report"	

# Parameters - Coverlet
$DataCollectorFormat = "cobertura"
$CoverletOutputFormat = "cobertura"
$CoverletOutputExtension = ".xml"
$Collect = "XPlat Code Coverage"
$CoverageFileName = "coverage.$CoverletOutputFormat$CoverletOutputExtension"
	
# Parameters - Report Generator
$ReportTypes = "HTML;cobertura;"
$HtmlReportIndexFileName = "index.html"	

# Calculated Parameters - Solution - CHANGE WITH EXTREME CAUTION
$SolutionDir = (Get-Item $PSScriptRoot).Parent.FullName
$Solution = [IO.Path]::Combine($SolutionDir, $SolutionFileName)

# Calculated Parameters - Coverlet - CHANGE WITH EXTREME CAUTION
$CoverageRunIdentifier = [GUID]::NewGuid().ToString()
$CoverageDir = Join-Path $SolutionDir -ChildPath coverage-script
$CoverletOutput = [IO.Path]::Combine($CoverageDir, $CoverageCoverletDir)	

# Build and Test
dotnet.exe test $Solution --collect:"$Collect" --results-directory:"$CoverletOutput" -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format="$DataCollectorFormat"	

# Calculated Parameters - Report Generator - CHANGE WITH EXTREME CAUTION
$TargetDir = [IO.Path]::Combine($CoverageReportDir, $CoverageRunIdentifier)
$ReportHtmlFile = [IO.Path]::Combine($CoverageReportDir, $CoverageRunIdentifier, $HtmlReportIndexFileName)
$TestResultsDirs = Join-Path $CoverletOutput -ChildPath **\*
$CoverageFiles = (Split-Path $TestResultsDirs -Resolve | ForEach-Object -Process {[IO.Path]::Combine($_, $CoverageFileName)}) -join ";"

# Generate Report
reportgenerator.exe "-reports:$CoverageFiles" "-targetdir:$TargetDir" "-reporttypes:$ReportTypes"

# Open Report
Invoke-Item $ReportHtmlFile