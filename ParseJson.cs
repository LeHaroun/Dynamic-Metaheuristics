using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experimenter
{
    class ParseJson
    {

       

        public double Distance { get; set; }

        public double Time { get; set; }

        public double TimeTraffic { get; set; }
        public double Status { get; set; }

      


        public ParseJson(string Json)
        {
            Status = getStatus(Json);
            Distance = getDistance(Json);
            Time = getDuration(Json);
            TimeTraffic = getDurationOnTraffic(Json);

            /*Microsoft.Maps.MapControl.WPF.Location start = new Microsoft.Maps.MapControl.WPF.Location();
            Microsoft.Maps.MapControl.WPF.Location end = new Microsoft.Maps.MapControl.WPF.Location();

            start.Latitude = ActualStart(Json, "Latitude");
            start.Longitude = ActualStart(Json, "Longitude");
            end.Latitude = ActualEnd(Json, "Latitude");
            end.Longitude = ActualEnd(Json, "Longitude");

            From = start;
            To = end;

            Locations = RouteCoordinates(Json);*/


        }

        #region parsing functions
       

        private double getDistance(string json)
        {
            string search1 = "\"travelDistance\":";
            int found1 = json.LastIndexOf(search1);
            int found2 = json.LastIndexOf(",\"travelDuration\":");

            string temp = json.Substring(found1, found2 - found1);
            temp = temp.Remove(0, search1.Length);
            temp = temp.Replace(".", ",");

            double distance = Convert.ToDouble(temp);

            return distance;
            

        }

        private double getDuration(string json)
        {
            string search1 = ",\"travelDuration\":";
            string search2 = ",\"travelDurationTraffic\":";
            int found1 = json.LastIndexOf(search1);
            int found2 = json.LastIndexOf(search2);

            string temp = json.Substring(found1, found2 - found1);
            temp = temp.Remove(0, search1.Length);
            temp = temp.Replace(".", ",");

            double duration = Convert.ToDouble(temp);

            return duration;
           
        }

        private double getDurationOnTraffic(string json)
        {
            string search1 = ",\"travelDurationTraffic\":";
            string search2 = "}]}],\"statusCode";
            int found1 = json.LastIndexOf(search1);
            int found2 = json.LastIndexOf(search2);

            string temp = json.Substring(found1, found2 - found1);
            temp = temp.Remove(0, search1.Length);
            temp = temp.Replace(".", ",");

            double durationOnTraffic = Convert.ToDouble(temp);
            return durationOnTraffic;


        }
        private double getStatus(string json)
        {
            string search1 = "\"statusCode\":";
            string search2 = ",\"statusDescription\":\"";
            int found1 = json.LastIndexOf(search1);
            int found2 = json.LastIndexOf(search2);

            string temp = json.Substring(found1, found2 - found1);
            temp = temp.Remove(0, search1.Length);


            double status = Convert.ToDouble(temp);

            return status;


        }
       
        
       

        #endregion 
    }

 
}
