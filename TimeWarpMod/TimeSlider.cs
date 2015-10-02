using BuildingThemes.GUI;
using ColossalFramework;
using ColossalFramework.UI;
using System;
using System.Text;
using UnityEngine;

namespace TimeWarpMod
{
    class TimeSlider : UIPanel
    {
        UILabel timeOfDay;
        UISlider timeSlider;

        Color32 sunColor = new Color32(235, 255, 92, 255);
        Color32 moonColor = new Color32(24, 84, 255, 255);

        bool pauseUpdates;

        public SunManager sunControl;

        public override void Awake()
        {

            size = new Vector2(400, 100);

            anchor = UIAnchorStyle.Bottom & UIAnchorStyle.Left;
            backgroundSprite = "ButtonMenu";

            autoLayoutPadding = new RectOffset(10, 10, 4, 4);
            autoLayout = true;
            autoFitChildrenVertically = true;
            autoLayoutDirection = LayoutDirection.Vertical;
            


            timeOfDay = AddUIComponent<UILabel>();
            timeOfDay.textAlignment = UIHorizontalAlignment.Center;
            timeOfDay.size = new Vector2(width - 20, 20);
            timeOfDay.autoSize = false;

            timeSlider = UIFactory.CreateSlider(this, 0.0f, 24.0f);
            timeSlider.stepSize = 1f / 60.0f;
            timeSlider.eventValueChanged += ChangeTime;
            ((UIPanel)timeSlider.parent).backgroundSprite = "BudgetBarBackground";
            ((UISprite)timeSlider.thumbObject).spriteName = "InfoIconBasePressed";

            timeSlider.eventDragStart += timeSlider_eventDragStart;
            //timeSlider.eventDragEnd += timeSlider_eventDragEnd;

            eventMouseUp += timeSlider_eventDragEnd;

            UISprite pad = AddUIComponent<UISprite>();
            pad.autoSize = false;
            pad.size = new Vector2(10, 5);
            
        }

        void timeSlider_eventDragEnd(UIComponent component, UIMouseEventParameter eventParam)
        {
            Debug.Log("drag end");
            pauseUpdates = false;
        }

        void timeSlider_eventDragStart(UIComponent component, UIDragEventParameter eventParam)
        {
            Debug.Log("drag start");
            pauseUpdates = true;
        }

        private void ChangeTime(UIComponent c, float val) {
            sunControl.TimeOfDay = val;
        }

        public override void Update()
        {
            float tod = sunControl.TimeOfDay;

            int hour = (int)Math.Floor(tod);
            int minute = (int)Math.Floor((tod - hour) * 60.0f);

            timeOfDay.text = String.Format("{0,2:00}:{1,2:00}", hour, minute);

            if (!pauseUpdates)
            {
     
                timeSlider.value = sunControl.TimeOfDay;

            }

            float fade = Math.Abs(sunControl.TimeOfDay - 12.0f) / 12.0f;


            

            ((UISprite)timeSlider.thumbObject).color = Color32.Lerp(sunColor, moonColor, fade);
            
            base.Update();
        }
    }
}
