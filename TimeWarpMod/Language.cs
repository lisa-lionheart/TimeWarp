using System.Collections.Generic;
using ColossalFramework.Plugins;
using ColossalFramework.Globalization;

namespace TimeWarpMod
{
    public static class TimeWarpLang
    {
        private static string selectedLanguage;
        private static Dictionary<string, string> texts = new Dictionary<string, string>();

        public static string Text(string key)
        {
            string currentLanguage = LocaleManager.instance.language;

            if (selectedLanguage == null || selectedLanguage != currentLanguage)
            {
                selectedLanguage = currentLanguage;
                fillDictionaryWithText(selectedLanguage);
                //DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, "localeID: " + selectedLanguage);
            }

            return texts[key];
        }

        private static void fillDictionaryWithText(string localeID)
        {
            if (selectedLanguage == "nl")
            {
                texts["MOD_NAME"] = "Tijdsprong";
                texts["MOD_DESCRIPTION"] = "Rechtermuisknop op de gebiedenknop om het moment van de dag te veranderen";

		texts["TOGGLE_TOOLTIP"] = "Dag-/nachtinstellingen";
		texts["ZOOMBUTTON_TOOLTIP"] = "Gebieden \n Rechtermuisknop om het moment van de dag te veranderen";
		texts["SUNCONTROL"] = "Zonbediening";
		texts["SUNCONTROL_TITLE"] = "Dag-/nachtinstellingen";
		texts["SUNCONTROL_SIZE"] = "Zongrootte";
		texts["SUNCONTROL_INTENSITY"] = "Zonne-intensiteit";
		texts["LATTITUDE"] = "Breedtegraad: ";
		texts["LONGITUDE"] = "Lengtegraad: ";
		texts["SPEED_PAUZED"] = "Snelheid: Gepauzeerd";
		texts["SPEED_NORMAL"] = "Snelheid: Normaal";
		texts["SPEED"] = "Snelheid: ";
            }
            else if (selectedLanguage == "de")
            {
                texts["MOD_NAME"] = "Zeitsprung";
                texts["MOD_DESCRIPTION"] = "Rechtsklick auf dem Gebietenknopf, um die Tageszeit zu ändern";

                texts["TOGGLE_TOOLTIP"] = "Tag / Nacht-einstellungen";
		texts["ZOOMBUTTON_TOOLTIP"] = "Gebiete \n Rechtsklick um die Tageszeit zu ändern";
		texts["SUNCONTROL"] = "Sonnebedienung";
		texts["SUNCONTROL_TITLE"] = "Tag / Nacht-einstellungen";
		texts["SUNCONTROL_SIZE"] = "Sonnegröße";
		texts["SUNCONTROL_INTENSITY"] = "Sonnenintensität";
		texts["LATTITUDE"] = "Breitengrad: ";
		texts["LONGITUDE"] = "Längengrad: ";
		texts["SPEED_PAUZED"] = "Geschwindigkeit: Pausiert";
		texts["SPEED_NORMAL"] = "Geschwindigkeit: Normal";
		texts["SPEED"] = "Geschwindigkeit: ";

            }
            else if (selectedLanguage == "it")
            {
                texts["MOD_NAME"] = "Distorsione del tempo";
                texts["MOD_DESCRIPTION"] = "Tasto destro del mouse sul bottone aree per cambiare l'ora del giorno";

		texts["TOGGLE_TOOLTIP"] = "Impostazioni giorno / notte";
		texts["ZOOMBUTTON_TOOLTIP"] = "Aree \n Tasto destro del mouse per cambiare l'ora del giorno";
		texts["SUNCONTROL"] = "Controllo del sole";
		texts["SUNCONTROL_TITLE"] = "Impostazioni giorno / notte";
		texts["SUNCONTROL_SIZE"] = "Formato sole";
		texts["SUNCONTROL_INTENSITY"] = "Intensità solare";
		texts["LATTITUDE"] = "Lattitudine: ";
		texts["LONGITUDE"] = "Longitudine: ";
		texts["SPEED_PAUZED"] = "Velocità: In pausa";
		texts["SPEED_NORMAL"] = "Velocità: Normale";
		texts["SPEED"] = "Velocità: ";
            }
            else
            {
                texts["MOD_NAME"] = "Time Warp";
                texts["MOD_DESCRIPTION"] = "Right click on the Area Zoom button to set the time of day";

		texts["TOGGLE_TOOLTIP"] = "Day/Night Settings";
		texts["ZOOMBUTTON_TOOLTIP"] = "Areas \n Right Click to set time of day";
		texts["SUNCONTROL"] = "Sun control";
		texts["SUNCONTROL_TITLE"] = "Day/Night Settings";
		texts["SUNCONTROL_SIZE"] = "Sun Size";
		texts["SUNCONTROL_INTENSITY"] = "Sun Intensity";
		texts["LATTITUDE"] = "Lattitude: ";
		texts["LONGITUDE"] = "Longitude: ";
		texts["SPEED_PAUZED"] = "Speed: Paused";
		texts["SPEED_NORMAL"] = "Speed: Normal";
		texts["SPEED"] = "Speed: ";
            }
        }
    }
}
