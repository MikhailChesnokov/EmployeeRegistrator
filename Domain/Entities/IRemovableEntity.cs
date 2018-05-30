namespace Domain.Entities
{
    public interface IRemovableEntity : IEntity
    {
        bool IsDeleted();
    }
}