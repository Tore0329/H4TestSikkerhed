namespace H4TestSikkerhedApp.Model
{
    public partial class TodoList
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Item { get; set; } = null!;

        public virtual Cpr User { get; set; } = null!;
    }
}
