#!/bin/sh
mcs /out:IPSharp.exe /win32icon:res/Icon.ico src/*.cs /r:./Newtonsoft.Json.dll
