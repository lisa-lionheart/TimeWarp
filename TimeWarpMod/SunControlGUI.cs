
using System;
using System.Text;
using BuildingThemes.GUI;
using UnityEngine;
using ColossalFramework.UI;

namespace TimeWarpMod
{
    class SunControlGUI : UIPanel
    {

        
        UILabel lattitude;
        UISlider lattitudeControl;

        UILabel longitude;
        UISlider longitudeControl;

        UISlider sunSize, sunIntensity;

        UILabel speed;
        UISlider speedControl;

        public SunManager sunControl;

        private uint[] speeds = { 0, 1, 2, 4, 8, 16, 32, 64, 128 };

        public override void Awake()
        {
            size = new Vector2(200, 100);
            
            anchor = UIAnchorStyle.Bottom & UIAnchorStyle.Left;
            backgroundSprite = "ButtonMenu";

            autoLayoutPadding = new RectOffset(10, 10, 4, 4);
            autoLayout = true;
            autoFitChildrenVertically = true;
            autoLayoutDirection = LayoutDirection.Vertical;

            UILabel title = AddUIComponent<UILabel>();
            title.text = TimeWarpLang.Text("SUNCONTROL_TITLE");
            title.textAlignment = UIHorizontalAlignment.Center;
            title.verticalAlignment = UIVerticalAlignment.Middle;
            title.textScale = 1.1f;
            title.autoSize = false;
            title.size = new Vector2(width - 20, 40);
                        

            
            //0, 1, 2, 4, 8, 16, 32, 64, 128 
            speed = AddUIComponent<UILabel>();
            speedControl = UIFactory.CreateSlider(this,0f, 8f);
            speedControl.eventValueChanged += ValueChanged;
           
            lattitude = AddUIComponent<UILabel>();
            lattitudeControl = UIFactory.CreateSlider(this, -80f, 80f);
            lattitudeControl.eventValueChanged += ValueChanged;

            longitude = AddUIComponent<UILabel>();
            longitudeControl = UIFactory.CreateSlider(this, -180f, 180f);
            longitudeControl.eventValueChanged += ValueChanged;

            AddUIComponent<UILabel>().text = TimeWarpLang.Text("SUNCONTROL_SIZE");
            sunSize = UIFactory.CreateSlider(this, 0.01f, 10.0f);         
            sunSize.eventValueChanged += ValueChanged;

            AddUIComponent<UILabel>().text = TimeWarpLang.Text("SUNCONTROL_INTENSITY");
            sunIntensity = UIFactory.CreateSlider(this, 0, 8f);
            sunIntensity.stepSize = 0.1f;

            UILabel endPadding = AddUIComponent<UILabel>();
            endPadding.text = "    ";
            
        }



        void ValueChanged(UIComponent component, float value)
        {
            Debug.Log("New Value: " + value);

            if (DayNightProperties.instance == null) return;

            if(component == lattitudeControl) {
                DayNightProperties.instance.m_Latitude = value;    
            }

            if(component == longitudeControl) {
                DayNightProperties.instance.m_Longitude = value;    
            }

            if (component == sunSize)
            {
                DayNightProperties.instance.m_SunSize = value;
            }

            if (component == sunIntensity)
            {
                DayNightProperties.instance.m_SunIntensity = value;
            }

            if (component == speedControl)
            {
                sunControl.speed = speeds[(uint)value];
            }
        }




        public override void Update()
        {

            if (DayNightProperties.instance != null) {

                lattitudeControl.value = DayNightProperties.instance.m_Latitude;
                lattitude.text = TimeWarpLang.Text("LATTITUDE") + Math.Floor(DayNightProperties.instance.m_Latitude) + "°";
                
                longitudeControl.value = DayNightProperties.instance.m_Longitude;
                longitude.text = TimeWarpLang.Text("LONGITUDE") + Math.Floor(DayNightProperties.instance.m_Longitude) + "°";

                sunSize.value = DayNightProperties.instance.m_SunSize;
                sunIntensity.value = DayNightProperties.instance.m_SunIntensity;


                switch (sunControl.speed)
                {
                    case 0:
                        speed.text = TimeWarpLang.Text("SPEED_PAUZED");
                        break;
                    case 1:
                        speed.text = TimeWarpLang.Text("SPEED_NORMAL");
                        break;
                    default:
                        speed.text = TimeWarpLang.Text("SPEED") + sunControl.speed + "x";
                        break;
                }

                speedControl.value = Array.IndexOf(speeds, sunControl.speed);
            }

            base.Update();
        }

    }
}
