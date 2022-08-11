Remove-Item $env:USERPROFILE\.nuget\packages\identityserver\ -Recurse -ErrorAction SilentlyContinue 
Remove-Item $env:USERPROFILE\.nuget\packages\identityserver.storage\ -Recurse -ErrorAction SilentlyContinue 
Remove-Item $env:USERPROFILE\.nuget\packages\identityserver.entityframework\ -Recurse -ErrorAction SilentlyContinue 
Remove-Item $env:USERPROFILE\.nuget\packages\identityserver.entityframework.storage\ -Recurse -ErrorAction SilentlyContinue 
Remove-Item $env:USERPROFILE\.nuget\packages\identityserver.aspnetidentity\ -Recurse -ErrorAction SilentlyContinue 

Remove-Item $env:USERPROFILE\.nuget\packages\identitymodel\ -Recurse -ErrorAction SilentlyContinue 
Remove-Item $env:USERPROFILE\.nuget\packages\IdentityModel.AspNetCore.OAuth2Introspection\ -Recurse -ErrorAction SilentlyContinue 
Remove-Item $env:USERPROFILE\.nuget\packages\IdentityServer.AccessTokenValidation\ -Recurse -ErrorAction SilentlyContinue 