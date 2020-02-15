﻿using System;

namespace ProConstructionsManagment.Core.Helpers
{
    public class GuidFactory : IGuidFactory
    {
        public Guid Create()
        {
            return Guid.NewGuid();
        }
    }
}