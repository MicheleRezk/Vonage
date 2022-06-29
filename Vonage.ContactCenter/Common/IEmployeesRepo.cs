namespace Vonage.ContactCenter.Common
{
    public interface IEmployeesRepo
    {
        ICollection<IEmployee> GetAvailableContactEmployeesOrdered();
    }
}
