using Core.Entities;

namespace Core.Specifications
{
    public class ClientWithFiltersForCountSpecification: BaseSpecification<Client>
    {
        public ClientWithFiltersForCountSpecification(ClientSpecParams clientParams)
        :base(x =>
                (string.IsNullOrEmpty(clientParams.Search) || x.Nom_Client.ToLower().Contains
                (clientParams.Search))  
            )
        {
        }
    }
}