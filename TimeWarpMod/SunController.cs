using ColossalFramework;
using ColossalFramework.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TimeWarpMod
{

    [Serializable]
    public class Settings
    {
        public Fraction speed;
        public uint dayOffsetFrames;
        public float longitude;
        public float lattitude;
        public float sunSize;
        public float sunIntensity;
    }

    class SunManager : MonoBehaviour, ISimulationManager
    {

        public Fraction speed;
        private uint tick;


        SimulationManager sim = Singleton<SimulationManager>.instance;
        private uint dayOffsetFrames;

        public void Awake()
        {
            dayOffsetFrames = sim.m_dayTimeOffsetFrames;
            SimulationManager.RegisterSimulationManager(this);
            speed = new Fraction(){num=1,den=1};

            LoadData();
        }


        void LoadData()
        {
            byte[] data = sim.m_SerializableDataWrapper.LoadData("TimeWarp");
            if (data != null)   {
                try
                {
                    Settings settings = (Settings)(new BinaryFormatter()).Deserialize(new MemoryStream(data));

                    speed = settings.speed;
                    dayOffsetFrames = settings.dayOffsetFrames;
                    DayNightProperties.instance.m_Longitude = settings.longitude;
                    DayNightProperties.instance.m_Latitude = settings.lattitude;
                    DayNightProperties.instance.m_SunIntensity = settings.sunIntensity;
                    DayNightProperties.instance.m_SunSize = settings.sunSize;
                    
                }
                catch (Exception e)
                {
                    Debug.Log("Error loading timewarp settings: " + e);
                }
            }
        }


        public void SaveData()
        {
            try
            {
                Settings settings = new Settings();

                settings.speed = speed;
                settings.dayOffsetFrames = dayOffsetFrames;
                settings.longitude = DayNightProperties.instance.m_Longitude;
                settings.lattitude = DayNightProperties.instance.m_Latitude;
                settings.sunIntensity = DayNightProperties.instance.m_SunIntensity;
                settings.sunSize = DayNightProperties.instance.m_SunSize;

                MemoryStream stream = new MemoryStream();
                new BinaryFormatter().Serialize(stream, settings);

                sim.m_SerializableDataWrapper.SaveData("TimeWarp", stream.ToArray());
                Debug.Log("Time warp settings saved okay");
            }
            catch (Exception e)
            {
                Debug.Log("Error saving time warp settings: " + e);
            }
        }
        

        public bool DayNightEnabled
        {
            get
            {
                return sim.m_enableDayNight;
            }
        }



        //Called after SimulationManager.SimulationStep(), overwrite what they were doing
        public void SimulationStep(int subStep)
        {
            if (!DayNightEnabled)
                return;

            if (!sim.SimulationPaused && !sim.ForcedSimulationPaused)
            {
                //Do every nth frame for fractional speeds
                if (tick == 0)
                {
                    dayOffsetFrames = (dayOffsetFrames + (uint)speed.num - 1) % SimulationManager.DAYTIME_FRAMES;
                }
                else
                {
                    dayOffsetFrames = (dayOffsetFrames - 1) % SimulationManager.DAYTIME_FRAMES;
                }

                tick = (tick + 1) % speed.den;
            }

            sim.m_dayTimeOffsetFrames = dayOffsetFrames;
        }

        public ThreadProfiler GetSimulationProfiler()
        {
            return null;
        }

        public void UpdateData(SimulationManager.UpdateMode mode)
        {
            
        }
        public void LateUpdateData(SimulationManager.UpdateMode mode)
        {
        }

        public void GetData(FastList<ColossalFramework.IO.IDataContainer> data)
        {

        }

        public float TimeOfDay
        {
            set
            {

                if (!DayNightEnabled)
                    return;
                /*
                float hour = ((sim.m_referenceFrameIndex + sim.m_dayTimeOffsetFrames) + this.m_referenceTimer) * DAYTIME_FRAME_TO_HOUR
            
                 hour / DAYTIME_FRAME_TO_HOUR = (sim.m_referenceFrameIndex + sim.m_dayTimeOffsetFrames + this.m_referenceTimer)
                */

                int offset = (int)((value - sim.m_currentDayTimeHour) / SimulationManager.DAYTIME_FRAME_TO_HOUR);
                dayOffsetFrames = (uint)(((long)dayOffsetFrames + offset) % SimulationManager.DAYTIME_FRAMES);

                sim.m_currentDayTimeHour = value;

                SaveData();
            }
            get
            {
                return sim.m_currentDayTimeHour;
            }
        }


        public string GetName()
        {
            return "Sun Controller";
        }
    }
}
