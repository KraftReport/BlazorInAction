namespace blazortrailsclient.Features.Home.Model
{
    public class TrailModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Image { get; set; } = "";
        public string Description { get; set; } = "";
        public string Location { get; set; } = "";
        public int TimeinMinutes { get; set; }
        public string TimeFormatted => $"{TimeinMinutes / 60}h{TimeinMinutes % 60}m";
        public int Length { get; set; }
        public IEnumerable<RouteInstruction> Routes { get; set; } = Array.Empty<RouteInstruction>();
    }


    public class RouteInstruction
    {
        public int State { get; set; }
        public string Description { get; set; }
    }
}
