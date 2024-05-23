namespace H4TestSikkerhedApp.Model
{
    public partial class Cpr
    {
        public int Id { get; set; }

        public string User { get; set; } = null!;

        public string CprNr { get; set; } = null!;

        public virtual ICollection<TodoList> TodoLists { get; set; } = new List<TodoList>();
    }
}
