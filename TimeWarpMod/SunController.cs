using ColossalFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TimeWarpMod
{
    class SunManager : MonoBehaviour, ISimulationManager
    {

        public double speed;


        SimulationManager sim = Singleton<SimulationManager>.instance;
        private double dayOffsetFrames;

        public void Awake() {
            dayOffsetFrames = sim.m_dayTimeOffsetFrames;
            SimulationManager.RegisterSimulationManager(this);
            speed = 1;
        }


        public void GetData(FastList<ColossalFramework.IO.IDataContainer> data)
        {
            
        }

        public string GetName()
        {
            return "Sun control";
        }

        public ThreadProfiler GetSimulationProfiler()
        {
            return null;
        }

        public void LateUpdateData(SimulationManager.UpdateMode mode)
        {
        }

        //Called after SimulationManager.SimulationStep(), overwrite what they were doing
        public void SimulationStep(int subStep)
        {
            if (!sim.SimulationPaused)
            {
                dayOffsetFrames = (dayOffsetFrames + speed - 1) % SimulationManager.DAYTIME_FRAMES;
            }

            sim.m_dayTimeOffsetFrames = (uint) dayOffsetFrames;
        }

        public void UpdateData(SimulationManager.UpdateMode mode)
        {
            
        }


        public float TimeOfDay
        {
            set
            {
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
