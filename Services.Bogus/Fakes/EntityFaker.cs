﻿using Bogus;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bogus.Fakes
{
    public abstract class EntityFaker<T> : Faker<T> where T : Entity
    {
        public EntityFaker(string locale) : base(locale)
        {
            RuleFor(x => x.Id, x => x.UniqueIndex + 1);
        }
    }
}
