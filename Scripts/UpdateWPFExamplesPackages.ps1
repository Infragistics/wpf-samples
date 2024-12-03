
$old1 = '24.1.39'
$new1 = '24.2.36'

$old2 = '24.1.20241.39'
$new2 = '24.2.20242.36'

$old3 = '24.1.39'
$new3 = '24.2.36'

$repoLoc = 'C:\Work\wpf-samples\Examples'
###############################

Write-Host "updating csproj files"
$files = Get-ChildItem -Path $repoLoc -filter *.csproj -Recurse
foreach ($file in $files) {
   
    $sel = Select-String -Path $file.FullName -Pattern $old1 -SimpleMatch;
    if ($sel){
        $content = Get-Content -Path $file.FullName;
        $updated = $content.Replace($old1,$new1);
        Set-Content -Path $file.FullName -Value $updated; 
        Write-Host $file.FullName 
    }
    else
    {
       Write-Host "file " + $file.FullName + " did not need updating"
    }
    $sel2 = Select-String -Path $file.FullName -Pattern $old2 -SimpleMatch;
    if ($sel){
        $content = Get-Content -Path $file.FullName;
        $updated = $content.Replace($old2,$new2);
        Set-Content -Path $file.FullName -Value $updated; 
        Write-Host $file.FullName 
    }
}

$repoLoc = 'C:\Work\wpf-samples\Examples'

Write-Host "updating packages files"
$files = Get-ChildItem -Path $repoLoc -filter packages.config -Recurse
foreach ($file in $files) {
   
    $sel = Select-String -Path $file.FullName -Pattern $old3 -SimpleMatch;
    if ($sel){
        $content = Get-Content -Path $file.FullName;
        $updated = $content.Replace($old3,$new3);
        Set-Content -Path $file.FullName -Value $updated; 
    }
    else
    {
       Write-Host "file " + $file.FullName + " did not need updating"
    }
   
}