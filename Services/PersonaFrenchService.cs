using WebAppWeek2.Interfaces;

namespace WebAppWeek2.Services
{
    public class PersonaFrenchService : IPersonaService<PersonaFrenchService>
    {
        public string AddPrefix(string telephoneNumber)
        {
            return "+33" + telephoneNumber; 
        }
    }
}
