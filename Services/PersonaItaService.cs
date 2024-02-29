using WebAppWeek2.Interfaces;

namespace WebAppWeek2.Services
{
    public class PersonaItaService : IPersonaService<PersonaItaService>
    {
        public string AddPrefix(string telephoneNumber)
        {
            return "+39" + telephoneNumber;
        }
    }
}
