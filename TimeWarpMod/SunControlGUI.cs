
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        UISlider sunSize;
        UISlider sunIntensity;

        UILabel speed;
        UISlider speedControl;

        public SunManager sunControl;

        private static Fraction[] speeds = { 
            new Fraction(){num=0, den=1},
            new Fraction(){num=1, den=128},
            new Fraction(){num=1, den=64},
            new Fraction(){num=1, den=16},
            new Fraction(){num=1, den=8},
            new Fraction(){num=1, den=4},
            new Fraction(){num=1, den=2},
            new Fraction(){num=1, den=1},
            new Fraction(){num=2, den=1},
            new Fraction(){num=4, den=1},
            new Fraction(){num=8, den=1},
            new Fraction(){num=16, den=1},
            new Fraction(){num=32, den=1},
            new Fraction(){num=64, den=1},
            new Fraction(){num=128,den=1}
        };

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
            title.text = i18n.current["suncontrol_title"];
            title.textAlignment = UIHorizontalAlignment.Center;
            title.verticalAlignment = UIVerticalAlignment.Middle;
            title.textScale = 1.1f;
            title.autoSize = false;
            title.size = new Vector2(width - 20, 40);
                        

            
            //0, 1, 2, 4, 8, 16, 32, 64, 128 
            speed = AddUIComponent<UILabel>();
            speedControl = UIFactory.CreateSlider(this,0f, speeds.Length);
            speedControl.eventValueChanged += ValueChanged;
           
            lattitude = AddUIComponent<UILabel>();
            lattitudeControl = UIFactory.CreateSlider(this, -80f, 80f);
            lattitudeControl.eventValueChanged += ValueChanged;

            longitude = AddUIComponent<UILabel>();
            longitudeControl = UIFactory.CreateSlider(this, -180f, 180f);
            longitudeControl.eventValueChanged += ValueChanged;

            AddUIComponent<UILabel>().text = i18n.current["suncontrol_size"];
            sunSize = UIFactory.CreateSlider(this, 0.01f, 10.0f);         
            sunSize.eventValueChanged += ValueChanged;

            AddUIComponent<UILabel>().text = i18n.current["suncontrol_intensity"];
            sunIntensity = UIFactory.CreateSlider(this, 0f, 20f);
            sunIntensity.stepSize = 0.1f;
            sunIntensity.eventValueChanged += ValueChanged;

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

            sunControl.SaveData();
        }




        void Update()
        {
            relativePosition = new Vector3(20, 620, 0);

            if (DayNightProperties.instance != null) {

                lattitudeControl.value = DayNightProperties.instance.m_Latitude;
                lattitude.text = i18n.current["lattitude"] + Math.Floor(DayNightProperties.instance.m_Latitude) + "°";
                
                longitudeControl.value = DayNightProperties.instance.m_Longitude;
                longitude.text = i18n.current["longitude"] + Math.Floor(DayNightProperties.instance.m_Longitude) + "°";

                sunSize.value = DayNightProperties.instance.m_SunSize;
                sunIntensity.value = DayNightProperties.instance.m_SunIntensity;

                if (sunControl.DayNightEnabled)
                {
                    speed.text = i18n.current["speed"] + sunControl.speed.ToString();
                    speedControl.value = Array.IndexOf(speeds, sunControl.speed);
                }
                else
                {
                    speed.text = i18n.current["speed_disabled"];
                    speedControl.value = 0;
                }                
            }

            base.Update();
        }

    }
}
