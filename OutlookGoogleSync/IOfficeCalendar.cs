using System.Collections.Generic;

namespace OutlookGoogleSync
{
    public interface IOfficeCalendar
    {
        List<OutlookAppointment> GetCalendarEntriesInRange();
        List<OutlookAppointment> GetCalendarEntries();
    }
}