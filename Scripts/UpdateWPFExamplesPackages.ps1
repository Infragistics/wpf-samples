

$assemblyShortVerOld = '25.1.17'
$assemblyShortVerNew = '25.2.59'

$assemblyPartsVerOld = $assemblyShortVerOld -split '\.'
$assemblyPartsVerNew = $assemblyShortVerNew -split '\.'

# '25.2.59' -> '25.2.2025259'
$assemblyLongVerOld = ($assemblyPartsVerOld[0] + "." + $assemblyPartsVerOld[1] + ".20" + $assemblyPartsVerOld[0] + $assemblyPartsVerOld[1] + "." + $assemblyPartsVerOld[2]).Trim();
$assemblyLongVerNew = ($assemblyPartsVerNew[0] + "." + $assemblyPartsVerNew[1] + ".20" + $assemblyPartsVerNew[0] + $assemblyPartsVerNew[1] + "." + $assemblyPartsVerNew[2]).Trim();

# $repoLocation = 'C:\Work\wpf-samples'
$repoLocation = '.\..'
###############################

Write-Host "this script is replacing:"
Write-Host "-" $assemblyShortVerOld "with" $assemblyShortVerNew "version and"
Write-Host "-" $assemblyLongVerOld "with" $assemblyLongVerNew "version"

# Write-Host "replacing " $assemblyLongVerOld " with " $assemblyLongVerNew " version..."

$files = Get-ChildItem -Path $repoLocation -filter *.csproj -Recurse
Write-Host "updating" $files.Count "*.csproj files in" $repoLocation "folder:"

foreach ($file in $files) {
    $isFileUpdated = $false;
    $isMatchingLongVersion = Select-String -Path $file.FullName -Pattern $assemblyLongVerOld -SimpleMatch;
    if ($isMatchingLongVersion){
        $content = Get-Content -Path $file.FullName;
        $updated = $content.Replace($assemblyLongVerOld, $assemblyLongVerNew);
        Set-Content -Path $file.FullName -Value $updated; 
        $isFileUpdated = $true;
    }

    $isMatchingShortVersion = Select-String -Path $file.FullName -Pattern $assemblyShortVerOld -SimpleMatch;
    if ($isMatchingShortVersion){
        $content = Get-Content -Path $file.FullName;
        $updated = $content.Replace($assemblyShortVerOld, $assemblyShortVerNew);
        Set-Content -Path $file.FullName -Value $updated; 
        $isFileUpdated = $true;
    }

    if ($isFileUpdated) 
    {
        Write-Host $file.FullName "- file updated";
    }
    else 
    {
        # Write-Host $file.FullName "- file did not need updating"
    }
}

$files = Get-ChildItem -Path $repoLocation -filter packages.config -Recurse
Write-Host "updating" $files.Count "*.config files in" $repoLocation "folder:"

foreach ($file in $files) {
    $isMatchingShortVersion = Select-String -Path $file.FullName -Pattern $assemblyShortVerOld -SimpleMatch;
    if ($isMatchingShortVersion){
        $content = Get-Content -Path $file.FullName;
        $updated = $content.Replace($assemblyShortVerOld, $assemblyShortVerNew);
        Set-Content -Path $file.FullName -Value $updated; 
        Write-Host $file.FullName "- file updated";
    }
    else
    {
        # Write-Host $file.FullName "file did not need updating";
    }
}