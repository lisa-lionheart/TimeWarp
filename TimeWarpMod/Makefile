SOURCES=ModMain.cs  SunControlGUI.cs  SunController.cs  TimeSlider.cs  UIUtils.cs

TimeWarpMod.dll: ${SOURCES}
	mcs -out:TimeWarpMod.dll -target:library -r:ColossalManaged.dll -r:ICities.dll -r:Assembly-CSharp.dll -r:UnityEngine.dll -codepage:utf8 ${SOURCES}
