echo "post build event"
RD /S /Q "../SocialNetwork.API/wwwroot"
xcopy "build" "../SocialNetwork.API/wwwroot" /e /i