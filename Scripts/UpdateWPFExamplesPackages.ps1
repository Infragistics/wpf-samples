
$old1 = '24.1.20'
$new1 = '25.1.7'

$old2 = '24.1.20242.20'
$new2 = '25.1.20251.7'

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
   
    $sel = Select-String -Path $file.FullName -Pattern $old1 -SimpleMatch;
    if ($sel){
        $content = Get-Content -Path $file.FullName;
        $updated = $content.Replace($old1,$new1);
        Set-Content -Path $file.FullName -Value $updated; 
    }
    else
    {
       Write-Host "file " + $file.FullName + " did not need updating"
    }
   
}