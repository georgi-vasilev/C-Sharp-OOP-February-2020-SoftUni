using MilitaryElit.Enumerations;
using System.Collections.Generic;

namespace MilitaryElit.Contracts
{
    public interface IMission
    {
        string CodeName { get; }

        State State { get; }

        void CompleteMission();
    }
}
