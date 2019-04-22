﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Xyzies.SSO.Identity.Data.Entity
{
    public class Department : BaseEntity<Guid>
    {
        public override Guid Id { get; set; }

        public string Name { get; set; }
    }
}
