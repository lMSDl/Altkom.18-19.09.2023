using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bogus.Fakes
{
    public class UserFaker : EntityFaker<User>
    {
        public UserFaker(string locale) : base(locale)
        {
            RuleFor(x => x.Username, x => x.Internet.UserName());
            RuleFor(x => x.Password, x => x.Internet.Password());
        }
    }
}
