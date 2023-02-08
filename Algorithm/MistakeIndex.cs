using System.Collections.Generic;

namespace MyTimeTable
{
    public class MistakeIndex
    {
        public int DayOfTheWeek { get; set; }
        public int HourPositionInDay { get; set; }
        public int RepeatedClassIndex { get; set; }
        public int OriginalClassIndex { get; set; }
        public int RepeatedHourGroupPosition { get; set; }
        public int OriginalHourGroupPosition { get; set; }
        public List<int> Busies { get; set; }

        public MistakeIndex(int dayOfTheWeek, int hourPositionInDay, int repeatedClassIndex, int originalClassIndex, int repeatedHourGroupPosition
            , int originalHourGroupPosition)
        {
            DayOfTheWeek = dayOfTheWeek;
            HourPositionInDay = hourPositionInDay;
            RepeatedClassIndex = repeatedClassIndex;
            RepeatedHourGroupPosition = repeatedHourGroupPosition;
            OriginalClassIndex = originalClassIndex;
            OriginalHourGroupPosition = originalHourGroupPosition;
            Busies = new List<int>();
        }
        public MistakeIndex(int dayOfTheWeek, int hourPositionInDay, int repeatedClassIndex, int originalClassIndex, int repeatedHourGroupPosition
                     , int originalHourGroupPosition, List<int> busies)
        {
            DayOfTheWeek = dayOfTheWeek;
            HourPositionInDay = hourPositionInDay;
            RepeatedClassIndex = repeatedClassIndex;
            RepeatedHourGroupPosition = repeatedHourGroupPosition;
            OriginalClassIndex = originalClassIndex;
            OriginalHourGroupPosition = originalHourGroupPosition;
            Busies = busies;
        }
    }
}
