using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFramework.Models
{
    public class CreateBooking
    {
        public int bookingid { get; set; }
        public Booking booking { get; set; }
    }

    public class Booking
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int totalprice { get; set; }
        public bool depositpaid { get; set; }

        public BookingDates bookingdates = new BookingDates();
        public string additionalneeds { get; set; }
    }

    public class BookingDates
    { 
        public string checkin { get; set; }
        public string checkout { get; set; }
    }
}
