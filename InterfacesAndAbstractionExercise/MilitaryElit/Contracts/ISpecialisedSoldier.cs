using MilitaryElit.Enumerations;

namespace MilitaryElit.Contracts
{
    public interface ISpecialisedSoldier : IPrivate
    {
        Corps Corps { get; }
    }
}
