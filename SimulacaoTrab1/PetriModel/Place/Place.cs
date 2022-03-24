namespace SimulacaoTrab1
{
    public class Place
    {
        public Place(int id, string label, int markCounter = 1)
        {
            this.MarkCounter = markCounter;
            this.ReservedMarks = 0;
            this.Id = id;
            this.Label = label;
            this.OutboundConnections = new List<Arc>();
        }

        public int MarkCounter { get; set; }
        public int Id { get; set; }
        public string Label { get; set; }
        public int ReservedMarks { get; set; }
        
        public List<Arc> OutboundConnections { get; set; }
    }
}
