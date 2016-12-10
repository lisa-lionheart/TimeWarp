using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using UnityEngine;

namespace TimeWarpMod
{
    public class Mod : IUserMod
    {
        public string Name
        {
            get { return i18n.current["mod_name"]; }
        }
        public string Description
        {
            get {
                return i18n.current["mod_description"]; 
            }
        }

    }
}
