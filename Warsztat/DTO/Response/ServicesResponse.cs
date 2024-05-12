using Warsztat.Data.Entity;

namespace Warsztat.DTO.Response
{
    public class ServicesResponse
    {
        public ServicesView Service { get; set; }
        public IEnumerable<Workers> Workers { get; set; }
    }
}
