using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ClientsWithSpecification : BaseSpecification<Client>
    {
        public ClientsWithSpecification(ClientSpecParams clientParams)
            : base(x =>
            (string.IsNullOrEmpty(clientParams.Search) || x.Nom_Client.ToLower().Contains
            (clientParams.Search)))
            
        {
                AddOrderBy(x => x.Nom_Client);
                ApplyPaging(clientParams.PageSize * (clientParams.PageIndex -1), clientParams.PageSize);

                if (!string.IsNullOrEmpty(clientParams.Sort))
                {
                    switch (clientParams.Sort)
                    {
                        
                        default:
                            AddOrderBy(n => n.Nom_Client);
                            break;
                    };
                }
                
        }
    
    }
}