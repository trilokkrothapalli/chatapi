namespace Chat.Data.Models.Interface
{
    public interface IEntity
    {
        int Id { get; set; }
        string GetEntityName();
    }
}
