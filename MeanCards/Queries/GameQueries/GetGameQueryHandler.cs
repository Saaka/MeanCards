using MeanCards.DAL.Interfaces.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeanCards.Queries.GameQueries
{
    public interface IGetGameQueryHandler
    {

    }

    public class GetGameQueryHandler : IGetGameQueryHandler
    {
        private readonly IQueryExecutor queryExecutor;

        public GetGameQueryHandler(IQueryExecutor queryExecutor)
        {
            this.queryExecutor = queryExecutor;
        }
    }
}
