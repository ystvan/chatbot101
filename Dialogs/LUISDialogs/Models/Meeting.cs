using System;


namespace chatbot101.Dialogs.LUISDialogs.Models
{
    [Serializable]
    public class Meeting
    {
        public string Teacher { get; set; }
        public string DateTime { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }

    }
}