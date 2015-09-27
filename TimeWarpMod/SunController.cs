using ColossalFramework;
using ColossalFramework.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TimeWarpMod
{
    class SunManager : MonoBehaviour, ISimulationManager
    {

        public uint speed;


        SimulationManager sim = Singleton<SimulationManager>.instance;
        private uint dayOffsetFrames;

        public void Awake()
        {
            dayOffsetFrames = sim.m_dayTimeOffsetFrames;
            SimulationManager.RegisterSimulationManager(this);
            speed = 1;

        }

    

        public string GetName()
        {
            return "Sun control";
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
                dayOffsetFrames = (dayOffsetFrames + speed - 1) % SimulationManager.DAYTIME_FRAMES;
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
            }
            get
            {
                return sim.m_currentDayTimeHour;
            }
        }




    }
}
