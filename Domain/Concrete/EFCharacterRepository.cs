﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFCharacterRepository : ICharacterRepository
    {
        private EFDBContext context = new EFDBContext();
        public IQueryable<Character> Characters
        {
            get
            {
                return context.Characters;
            }
        }
    }
}
