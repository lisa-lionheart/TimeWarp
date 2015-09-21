using BuildingThemes.GUI;
using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TimeWarpMod
{
    public class ModMain : LoadingExtensionBase, IUserMod
    {
        SunControlGUI sunControlPanel;
        TimeSlider timeSlider;
        SunManager sunControl;
            
        public string Name
        {
            get { return "Time Warp"; }
        }
        public string Description
        {
            get {
                return "Right click on the Area Zoom button to set the time of day"; 
            }
        }


        public void CreateControlPanel()
        {
            
            Debug.Log("Creating panel");

            sunControlPanel = new GameObject("SunControlPanel").AddComponent<SunControlGUI>();
            sunControlPanel.gameObject.transform.localPosition = new Vector3(-1.75f, -0.1f, 0);
            sunControlPanel.sunControl = sunControl;

            timeSlider = new GameObject("TimeSlider").AddComponent<TimeSlider>();
            timeSlider.gameObject.transform.localPosition = new Vector3(-1.33f, -0.6f, 0);
            timeSlider.sunControl = sunControl;


            UIView.GetAView().AttachUIComponent(timeSlider.gameObject);
            UIView.GetAView().AttachUIComponent(sunControlPanel.gameObject);


            sunControlPanel.gameObject.SetActive(false);
            timeSlider.gameObject.SetActive(false);
        }

        public void AddGUIToggle()
        {

            UIMultiStateButton zoomButton = GameObject.Find("ZoomButton").GetComponent<UIMultiStateButton>();

            UIComponent bottomBar = zoomButton.parent;

            UIButton toggle = UIFactory.CreateButton(bottomBar);

            toggle.area = new Vector4(108, 24, 38, 38);
            toggle.playAudioEvents = true;

            toggle.normalBgSprite = "OptionBase";
            toggle.focusedBgSprite = "OptionBaseFocus";
            toggle.hoveredBgSprite = "OptionBaseHover";
            toggle.pressedBgSprite = "OptionBasePressed";

            toggle.tooltip = "Day/Night Settings";

            toggle.normalFgSprite = "InfoIconEntertainmentDisabled";
            toggle.scaleFactor = 0.75f;

            toggle.eventClicked += (UIComponent component, UIMouseEventParameter eventParam) =>
            {
                bool active = !sunControlPanel.gameObject.active;


                toggle.normalBgSprite = active ? "OptionBasePressed" : "OptionBase";

                sunControlPanel.gameObject.SetActive(active);
                timeSlider.gameObject.SetActive(active);
                
            };
        }


        public void HookZoomControls()
        {
            UIMultiStateButton zoomButton = GameObject.Find("ZoomButton").GetComponent<UIMultiStateButton>();

            zoomButton.tooltip = "Areas \n Right Click to set time of day";
            zoomButton.eventMouseMove += MouseMoved;
            zoomButton.eventMouseUp += MouseMoved;

            zoomButton.backgroundSprites[0].hovered = "";
            zoomButton.backgroundSprites[1].hovered = "";
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            if (GameObject.Find("ZoomButton") != null)
            {
                sunControl = new GameObject().AddComponent<SunManager>();

                CreateControlPanel();
                AddGUIToggle();
                HookZoomControls();
            }

        }

        void MouseMoved(UIComponent component, UIMouseEventParameter eventParam)
        {
            Vector2 pos;
            component.GetHitPosition(eventParam.ray, out pos);

            Vector2 center = new Vector2(30, 30);

            float angle = Vector2.Angle(new Vector3(0, 30), pos - center);

            if (pos.x > center.x)
                angle = 360.0f - angle;

            float time = (angle * 12.0f) / 180.0f;

            //Debug.Log("Mouse moved on zoom button");
            //Debug.Log("Angle =" + angle);
            //Debug.Log("Time = " + time);
            //Debug.Log("Button: " + eventParam.buttons);

            if (eventParam.buttons == UIMouseButton.Right)
                sunControl.TimeOfDay = time;

        }

    }
}
