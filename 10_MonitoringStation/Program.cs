namespace _10_MonitoringStation
{
    class Program
    {
        public static void Main()
        {
            Data data = new Data();

            Grid grid = new Grid(data);

            grid.Scan();

            grid.Results(1);

        }
    }
}
