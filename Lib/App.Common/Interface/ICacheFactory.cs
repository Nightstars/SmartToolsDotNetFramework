﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Interface
{
    public interface ICacheFactory
    {
        public ICacheUtil GetInstance();
    }
}
