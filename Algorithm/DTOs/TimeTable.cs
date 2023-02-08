namespace Model.DTOs
{
    public class TimeTable
    {
        public Day[] days { get; set; }
        public ClassDTO Class { get; set; }
        public TimeTable(Day[] days, ClassDTO clas)
        {
            this.days = days;
            this.Class = clas;
        }
    }
}
