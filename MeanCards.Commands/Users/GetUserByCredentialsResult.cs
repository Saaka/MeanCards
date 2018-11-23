using System;
using System.Collections.Generic;
using System.Text;

namespace MeanCards.Commands.Users
{
    public class GetUserByCredentialsResult : BaseResult
    {
        public GetUserByCredentialsResult()
        {
        }

        public GetUserByCredentialsResult(string error) : base(error)
        {
        }
    }
}
