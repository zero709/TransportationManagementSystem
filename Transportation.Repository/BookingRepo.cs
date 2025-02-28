﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.Data;
using Transportation.Entity;

namespace Transportation.Repository
{
    public static  class BookingRepo
    {
        public static bool Save(Booking b)
        {
            string sql = $"select * from booking where journey_date='{b.JourneyDate} ' and schedule_id='{b.ScheduleId}'";
            var dt = DataAccess.GetDataTable(sql);

            if (dt.Rows.Count > 0)
                return Update(b);
            else
            {
                sql = $"INSERT INTO [booking] VALUES('{b.JourneyDate}', '{b.Seats}', '{b.ScheduleId}', {b.AvailableSeatCount})";
                var row = DataAccess.ExecuteDmlQuery(sql);
                return row == 1;
            }

            }

        public static bool Update(Booking b)
        {
            string seats = "," + b.Seats;
         string sql = $"update booking set seats = concat(seats, '{seats}'),available_seat_count='{b.AvailableSeatCount}'  where journey_date = '{b.JourneyDate}' and schedule_id='{b.ScheduleId}'";
            var row = DataAccess.ExecuteDmlQuery(sql);
            return row == 1;
        }

        public static string GetBookedSeats(string scheduleId, string date)
        {
            string id = "";
            string sql = $"select seats from [booking] where journey_date = '{date}' and schedule_id = '{scheduleId}';";
            var dt = DataAccess.GetDataTable(sql);
            if (dt.Rows.Count > 0)
                id = dt.Rows[0][0].ToString();
            
            return id;
        }

        public static int GetAvailableSeatCount(string journeyDate,string scheduleId)
        {
            string sql = $"select available_seat_count from booking where journey_date = '{journeyDate}' and schedule_id = '{scheduleId}'";
            var dataTable = DataAccess.GetDataTable(sql);
            string seatCount;

            if (dataTable.Rows.Count > 0)
                seatCount = dataTable.Rows[0][0].ToString();
            else seatCount = "37" ;
            return int.Parse(seatCount);
        }
    }
}
